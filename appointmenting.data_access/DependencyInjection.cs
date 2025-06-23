using appointmenting.DbContexts;
using appointmenting.Repositories.Annotations;
using appointmenting.Repositories.Base;
using appointmenting.RepositoryAbstractions.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Scrutor;
using System.Reflection;

namespace appointmenting.data_access;

public static class DependencyInjection
{

    private static ILogger? _logger;
    private static IConfiguration? _configuration;

    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        var scope = services.BuildServiceProvider().CreateScope();
        var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
        _logger = loggerFactory.CreateLogger(nameof(DependencyInjection));
        _configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        _logger?.LogInformation("▶ Add Data Access");

        services.AddDbContext().AddRepositories();

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        var connstring = _configuration?.GetConnectionString("Apmt");
        if (string.IsNullOrEmpty(connstring))
            throw new InvalidDataException("Missing configuration for database");

        _logger?.LogInformation("\t ✓ Add Database Context");

        services.AddDbContext<ApmtDbContext>(options => options.UseSqlServer(connstring)
            .EnableSensitiveDataLogging(_logger!.IsEnabled(LogLevel.Debug))
            .EnableDetailedErrors(_logger!.IsEnabled(LogLevel.Debug)));

        return services;
    }
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        _logger?.LogInformation("\t ✓ Add Repositories");

        services.TryAddScoped<IRepositoryManager, RepositoryManager>();
        services.TryAddScoped<IRepositoryContext, RepositoryContext>();

        services.Scan(selector => selector.FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.WithAttribute<RepositoryAttribute>(attribute => attribute.Lifetime.Equals(Lifetime.Singleton)))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithSingletonLifetime()

            .AddClasses(classes => classes.WithAttribute<RepositoryAttribute>(attribute => attribute.Lifetime.Equals(Lifetime.Scoped)))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime()

            .AddClasses(classes => classes.WithAttribute<RepositoryAttribute>(attribute => attribute.Lifetime.Equals(Lifetime.Transient)))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithTransientLifetime());

        return services;
    }
}
