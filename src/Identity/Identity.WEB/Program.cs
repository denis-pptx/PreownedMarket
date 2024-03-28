using Identity.Application.Mappings;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Data.Seed;
using Identity.WEB.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlite(connection));

builder.Services.AddIdentity();

builder.Services.AddDI();

builder.Services.ConfigureAuthentication();

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(UserMappingProfile)));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(LoginUserHandler)));

builder.Services.AddExcepitonHandlers();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await DataInitializer.Seed(app.Services);

app.Run();