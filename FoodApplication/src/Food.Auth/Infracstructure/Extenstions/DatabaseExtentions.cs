using Domain.Entity;
using Infracstructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infracstructure.Extenstions
{
    public static class DatabaseExtentions
    {
        
        private static async Task SeedRolesAsync(RoleManager<AppRole> roleManager)
        {
            string[] roles = { "Admin", "User", "Manager" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new AppRole() { Name = role, Description = role });
                }
            }
        }

        public static async Task SeedDatabaseAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            try
            {
                var context = serviceProvider.GetRequiredService<AuthDBContext>();
                await context.Database.MigrateAsync();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
                await DatabaseExtentions.SeedRolesAsync(roleManager);
            }
            catch (Exception ex)
            {
                // Log the error and handle it as needed
                Console.WriteLine($"An error occurred during database seeding: {ex.Message}");
            }
        }
    }
}
