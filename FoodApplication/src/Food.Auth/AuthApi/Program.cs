using Application;
using Infrastructure;
using AuthApi;
using Infracstructure.Extenstions;
using Microsoft.AspNetCore.Identity;
using Domain.Entity;
using Infracstructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);
builder.Services.AddControllers();


var app = builder.Build();
app.MapControllers();
app.UseApiServices();
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AuthDBContext>();
    context.Database.MigrateAsync().GetAwaiter().GetResult();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
    await DatabaseExtentions.SeedRolesAsync(roleManager);
}
app.Run();




