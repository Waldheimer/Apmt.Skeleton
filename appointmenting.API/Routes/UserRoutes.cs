using appointmenting.AspNetCore;
using appointmenting.domain.Entities;
using appointmenting.Dtos.User;
using appointmenting.Features.User.Commands;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace appointmenting.Routes;

public class UserRoutes : IApiEndpoint
{
    public RouteGroupBuilder ConfigureRoutes(RouteGroupBuilder builder)
    {
        //  POST
        builder.MapPost("users/register", RegisterUserAsync);

        //  GET
        builder.MapGet("users/{id:guid}", GetUserByIdAsync);

        //  PUT

        //  DELETE

        return builder;
    }

    #region POST
    public async Task<IResult> RegisterUserAsync([FromBody] UserRegisterDto user, ISender sender, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new UserRegisterCommand(user), cancellationToken);
        if (result.Success) return TypedResults.Ok(result);
        else return TypedResults.BadRequest(result);

    }
    #endregion
    #region GET
    public async Task<IResult> GetUserByIdAsync([FromRoute] Guid id, ISender sender, CancellationToken cancellationToken = default)
    {
        ApiResult<UserDto> response = null!;

        response = new ApiResult<UserDto> { Success = true };
        await Task.Yield();
        return TypedResults.Ok(id);
    }
    #endregion
}
