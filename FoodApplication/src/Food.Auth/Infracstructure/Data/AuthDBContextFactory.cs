using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infracstructure.Data
{
    public class AuthDBContextFactory : IDesignTimeDbContextFactory<AuthDBContext>
    {
        public AuthDBContext CreateDbContext(string[] args)
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Get the connection string
            var connectionString = configuration.GetConnectionString("Database");

            // Configure DbContext options
            var optionsBuilder = new DbContextOptionsBuilder<AuthDBContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new AuthDBContext(optionsBuilder.Options);
        }
    }
}
