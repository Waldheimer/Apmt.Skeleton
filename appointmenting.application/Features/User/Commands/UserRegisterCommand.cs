using appointmenting.AspNetCore;
using appointmenting.Dtos.User;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace appointmenting.Features.User.Commands;

public record UserRegisterCommand(UserRegisterDto input) : IRequest<ApiResult<string>>;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, ApiResult<string>>
{
    private readonly ILogger<UserRegisterCommandHandler> _logger;
    private readonly IValidator<UserRegisterDto> _validator;
    //private readonly IRepositoryManager _repository;
    public UserRegisterCommandHandler(ILogger<UserRegisterCommandHandler> logger, IValidator<UserRegisterDto> validator)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(validator, nameof(validator));
        _logger = logger;
        _validator = validator;
    }

    public async Task<ApiResult<string>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        ApiResult<string> response = null!;
        ValidationResult vres = await _validator.ValidateAsync(request.input);
        if (vres.IsValid)
        {
            response = new ApiResult<string> { Success = true, Message = "User successfully registered" };
            _logger.LogInformation("\t ✓ User successfully registered");
            return response;
        }
        else
        {
            var messages = new List<string>();
            vres.Errors.ForEach(x => messages.Add(x.ErrorMessage));
            response = new ApiResult<string> { Success = false, Message = JsonConvert.SerializeObject(messages), ErrorCode = "InputValidationError" };
            _logger.LogInformation("\t x User registration failed");
            return response;
        }
    }
}
