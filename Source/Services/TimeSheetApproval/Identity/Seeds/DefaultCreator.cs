using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Enums;
using TimeSheetApproval.Identity.Models;

namespace TimeSheetApproval.Identity.Seeds
{
    public static class DefaultCreator
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "Creator",
                Email = "creator@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Pass@123");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Creator.ToString());
                }

            }
        }
    }
}
