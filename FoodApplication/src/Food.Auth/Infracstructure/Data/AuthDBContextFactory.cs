using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infracstructure.Data
{
    public class AuthDBContextFactory : IDesignTimeDbContextFactory<AuthDBContext>
    {
        public AuthDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AuthDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Database"));
            return new AuthDBContext(optionsBuilder.Options);
        }
    }
}
