using MediatR;
using Microsoft.Extensions.Logging;
using TimeSheetApproval.Application.Interfaces;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TimeSheetApproval.Application.Behaviours
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly IAuthenticatedUserService _currentUserService;
        private readonly IAccountService _identityService;

        public RequestPerformanceBehaviour(
            ILogger<TRequest> logger,
            IAuthenticatedUserService currentUserService,
            IAccountService identityService)
        {
            _timer = new Stopwatch();

            _logger = logger;
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 100)
            {
                var requestName = typeof(TRequest).Name;
                var userId = _currentUserService.UserId;
                //var userName = await _identityService.GetUserNameAsync(userId);
                var userName = userId;

                _logger.LogWarning("TimeSheetApproval.API Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
                    requestName, elapsedMilliseconds, userId, userName, request);
            }

            return response;
        }
    }
}
