using AfriLearnBackend.Constants;
using AfriLearnBackend.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AfriLearnBackend
{
    public class IdentityDbInitializer
    {
        public static async Task SeedData(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        public static async Task SeedUsers(UserManager<AppUser> userManager)
        {
            foreach (var  appUser in AdminUsers.Admins)
            {
                await CreateUserAsync(userManager, appUser, appUser.PasswordHash);
            }
        }

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            await CreateRoleAsync(roleManager, Roles.Admin);
            await CreateRoleAsync(roleManager, Roles.Partner);
            await CreateRoleAsync(roleManager, Roles.Employee);
            await CreateRoleAsync(roleManager, Roles.Manager);
            await CreateRoleAsync(roleManager, Roles.Student);
            await CreateRoleAsync(roleManager, Roles.Tutor);
        }

        private static async Task CreateRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                IdentityRole role = new IdentityRole
                {
                    Name = roleName
                };
                await roleManager.CreateAsync(role);
            }
        }

        private static async Task CreateUserAsync(UserManager<AppUser> userManager, AppUser user, string password)
        {
            if (await userManager.FindByEmailAsync(user.Email) == null)
            {
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, user.Role);
                }
            }
        }
    }
}
