using TimeSheetApproval.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Enums;

namespace TimeSheetApproval.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Approver.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Creator.ToString()));
        }
    }
}
