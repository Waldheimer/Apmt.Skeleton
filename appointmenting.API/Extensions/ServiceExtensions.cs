using Microsoft.Extensions.Configuration;

namespace appointmenting.API.Extensions;

public static class ServiceExtensions
{
    public static ILogger<T> CurrentLogger<T>(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        ILogger<T> _logger = null!;

        var scope = services.BuildServiceProvider().CreateScope();
        var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
        _logger = loggerFactory.CreateLogger<T>();

        return _logger;
    }
    public static ILogger CurrentLogger(this IServiceCollection services, string name)
    {
        ArgumentNullException.ThrowIfNull(services, nameof (services));

        var scope = services.BuildServiceProvider().CreateScope();
        var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
        return loggerFactory.CreateLogger(name);
    }
    public static IConfiguration CurrentConfiguration(this ServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        IConfiguration _configuration = null!;

        var scope = services.BuildServiceProvider().CreateScope();
        _configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        return _configuration;
    }
}
