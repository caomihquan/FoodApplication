using Application;
using Infrastructure;
using AuthApi;
using Infracstructure.Extenstions;
using Microsoft.AspNetCore.Identity;
using Domain.Entity;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);
builder.Services.AddControllers();


var app = builder.Build();
app.MapControllers();
app.UseApiServices();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
    await Seeder.SeedRolesAsync(roleManager);
}
app.Run();




