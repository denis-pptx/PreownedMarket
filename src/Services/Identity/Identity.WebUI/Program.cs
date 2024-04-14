using Identity.Application.Mappings;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Data.Seed;
using Identity.WebUI.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("MySQL");
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 3, 0))));

//var connection = builder.Configuration.GetConnectionString("SQLite");
//builder.Services.AddDbContext<ApplicationDbContext>(
//    options => options.UseSqlite(connection));

builder.Host.UseLogging();

builder.Services.AddApplication();

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(UserMappingProfile)));

builder.Services.AddProblemDetails();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.AddMiddleware();

await app.SeedDataAsync();

app.Run();