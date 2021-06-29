using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Interfaces;

namespace TimeSheetApproval.Application.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly IAuthenticatedUserService _currentUserService;
        private readonly IAccountService _identityService;

        public RequestLogger(ILogger<TRequest> logger, IAuthenticatedUserService currentUserService, IAccountService identityService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId;
            //var userName = await _identityService.GetUserNameAsync(userId);
            var userName = userId;

            _logger.LogInformation("-----------------------------------------------------");
            _logger.LogInformation("TimeSheetApproval.API Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);
            _logger.LogInformation("-----------------------------------------------------");
        }
    }
}
