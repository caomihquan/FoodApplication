using Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Infracstructure.Data;
public class AuthDBContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public AuthDBContext(DbContextOptions<AuthDBContext> options)
        : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Add custom configurations here if needed
    }
}
