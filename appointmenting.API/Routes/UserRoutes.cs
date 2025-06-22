using appointmenting.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace appointmenting.Routes;

public class UserRoutes : IApiEndpoint
{
    public RouteGroupBuilder ConfigureRoutes(RouteGroupBuilder builder)
    {
        //builder.MapGet("users/{id:guid}", GetUserByIdAsync);


        return builder;
    }

    //public async Task<ApiResult> GetUserByIdAsync([FromRoute] Guid id)
    //{

    //}
}
