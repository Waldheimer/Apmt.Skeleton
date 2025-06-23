using appointmenting.Features.User.Commands;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace appointmenting.application;

public static class DependencyInjection
{

    private static ILogger? _logger;
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var scope = services.BuildServiceProvider().CreateScope();
        var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
        _logger = loggerFactory.CreateLogger(nameof(DependencyInjection));

        _logger?.LogInformation("▶ Adding Application");
        services.AddMediatr(_logger!)
            .AddFluentValidator(_logger!);
            //.AddAutoMapper()
            //.AddSecurityFeatures()
            //.AddServices();

        return services;
    }

    private static IServiceCollection AddMediatr(this IServiceCollection services, ILogger logger)
    {

        logger.LogInformation("\t ✓ Adding MediatR");
        services.AddMediatR(o =>
        {
            o.Lifetime = ServiceLifetime.Scoped;
            o.RegisterServicesFromAssemblyContaining<UserRegisterCommandHandler>();
        });
        return services;
    }
    private static IServiceCollection AddFluentValidator(this IServiceCollection services, ILogger logger)
    {
        logger?.LogInformation("\t ✓ Add FluentValidation");
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        ValidatorOptions.Global.LanguageManager.Enabled = false;

        return services;
    }
}
