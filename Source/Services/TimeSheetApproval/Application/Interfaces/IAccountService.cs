using TimeSheetApproval.Application.DTOs.Account;
using TimeSheetApproval.Application.Wrappers;
using System.Threading.Tasks;

namespace TimeSheetApproval.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Response<string>> ConfirmEmailAsync(string userId, string code);
        Task ForgotPassword(ForgotPasswordRequest model, string origin);
        Task<Response<string>> ResetPassword(ResetPasswordRequest model);
        Task<string> GetUserNameAsync(string userId);
        Task<Response<string>> CreateNewRoleAsync(string roleName);
        Task<Response<string>> MapRoleToUserAsync(string userName, string roleName);
        Task<Response<string>> RemoveRoleFromUserAsync(string userName, string roleName);
    }
}
