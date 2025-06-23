using MediatR;
using Microsoft.Extensions.Logging;

namespace appointmenting.Features.User.Commands
{
    //Todo: add parameter(s)
    //Todo: replace 'object' with an existing return value
    public record UserLoginCommandCommand() : IRequest<object>;

    //Todo: replace 'object' with an existing return value
    public sealed class UserLoginCommandCommandHandler : IRequestHandler<UserLoginCommandCommand, object>
    {
        private readonly ILogger<UserLoginCommandCommandHandler> _logger;
        //Todo: add DI here
        public UserLoginCommandCommandHandler(ILogger<UserLoginCommandCommandHandler> logger)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            _logger = logger;
            //always check against null for all DI
            //ArgumentNullException.ThrowIfNull(..., nameof(...));
        }

        //Todo: replace 'object' with an existing return value
        public async Task<object> Handle(UserLoginCommandCommand request, CancellationToken cancellationToken)
        {
            //Todo: add logic here
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
    }
}
