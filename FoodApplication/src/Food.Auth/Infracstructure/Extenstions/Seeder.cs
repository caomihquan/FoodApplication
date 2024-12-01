using Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace Infracstructure.Extenstions
{
    public static class Seeder
    {
        public static async Task SeedRolesAsync(RoleManager<AppRole> roleManager)
        {
            string[] roles = { "Admin", "User", "Manager" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new AppRole() { Name = role,Description = role });
                }
            }
        }
    }
}
