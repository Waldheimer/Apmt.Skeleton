using Microsoft.AspNetCore.Routing;

namespace appointmenting.AspNetCore;

public interface IApiEndpoint
{
    RouteGroupBuilder ConfigureRoutes(RouteGroupBuilder builder);
}
