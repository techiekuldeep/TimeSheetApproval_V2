using TimeSheetApproval.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TimeSheetApproval.WebApi.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
            UserName = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            UserFullName = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public string UserId { get; }
        public string UserName { get; }
        public string UserFullName { get; }
    }
}
