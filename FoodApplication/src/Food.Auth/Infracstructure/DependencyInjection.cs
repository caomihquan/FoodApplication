using Domain.Entity;
using Infracstructure.Data;
using Infracstructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthDBContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("Database")));
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }
}
