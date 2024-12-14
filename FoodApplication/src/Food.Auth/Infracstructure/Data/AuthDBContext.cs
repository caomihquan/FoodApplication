using Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
namespace Infracstructure.Data;
public class AuthDBContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public AuthDBContext(DbContextOptions<AuthDBContext> options)
        : base(options) { }
}


