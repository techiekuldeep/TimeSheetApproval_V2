namespace TimeSheetApproval.Application.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
        string UserName { get; }
        string UserFullName { get; }
    }
}
