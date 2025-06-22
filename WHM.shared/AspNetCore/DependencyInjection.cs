using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace appointmenting.AspNetCore;

public static class DependencyInjection
{
    private static ILogger? _logger;
    private static IConfiguration? _configuration;
    public static IServiceCollection AddApiEndpoints(this IServiceCollection services)
    {
        var scope = services.BuildServiceProvider().CreateScope();
        var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
        _logger = loggerFactory.CreateLogger(nameof(DependencyInjection));
        _configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        ArgumentNullException.ThrowIfNull(services, nameof(services));

        var serviceDescriptors = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(s => !s.IsAbstract && !s.IsInterface && s.IsAssignableTo(typeof(IApiEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IApiEndpoint), type));
        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }
    public static RouteGroupBuilder UseApiEndpoints(this WebApplication app)
    {
        _logger = app.Logger;
        _logger?.LogInformation("▶ Using API Endpoints");
        ArgumentNullException.ThrowIfNull(app, nameof(app));
        try
        {
            var endpoints = app.Services.GetRequiredService<IEnumerable<IApiEndpoint>>();

            var baseGroup = app.MapGroup("api/v1");
            foreach (var item in endpoints)
                item.ConfigureRoutes(baseGroup);
            return baseGroup;
        }
        catch (InvalidOperationException) { throw new InvalidOperationException("Dependency Injection is not intended for API endpoints"); }
        catch { throw; }
    }
}
