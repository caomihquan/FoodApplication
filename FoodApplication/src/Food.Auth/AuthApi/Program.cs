using Application;
using AuthApi;
using Domain.Entity;
using Infracstructure.Data;
using Infracstructure.Extenstions;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddApiServices(builder.Configuration);
builder.Services.AddControllers();


var app = builder.Build();
app.MapControllers();
app.UseApiServices();
if (app.Environment.IsDevelopment())
{
    await DatabaseExtentions.SeedDatabaseAsync(app.Services);
}
app.Run();




