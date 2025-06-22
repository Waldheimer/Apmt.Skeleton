using appointmenting.API.Extensions;
using appointmenting.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace appointmenting;

public static class DependencyInjection
{
    private static ILogger? _logger;
    //private static IConfiguration? _configuration;

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        _logger = services.CurrentLogger(nameof(DependencyInjection));
        
        _logger.LogInformation("▶ Adding CORS");
        services.AddCors(configure => configure.AddPolicy("AllowAny", policy =>
        {
            var allowedHeaders = new List<string> { "Origin", "X-Requested-With", "Accept",
                                                    "Content-Type", "Access-Control-Allow-Headers",
                                                    "Access-Control-Allow-Origin", "Access-Control-Request-Method" };

            var allowedMethods = new List<string> { "HEAD", "GET", "POST", "PUT", "DELETE", "PATCH", "OPTIONS" };

            policy.SetIsOriginAllowed(origin => true);
            policy.WithHeaders([.. allowedHeaders]);
            policy.WithMethods([.. allowedMethods]);
            policy.AllowCredentials();
            policy.SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
        }));
        
        //_logger.LogInformation("✓ Adding Api Endpoints");
        _logger.LogInformation("▶ Adding API Endpoints");
        services.AddApiEndpoints();

        return services;
    }
}
