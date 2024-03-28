using Identity.Application.Abstractions;
using Identity.Application.Mappings;
using Identity.Domain.Models;
using Identity.Infrastructure.Authentication;
using Identity.Infrastructure.Data;
using Identity.WEB.ExceptionHandlers;
using Identity.WEB.OptionsSetup;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlite(connection));

builder.Services.AddIdentity<User, IdentityRole>(opt =>
                {
                    opt.User.RequireUniqueEmail = true;
                }).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(UserMappingProfile)));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Identity.Application.Features.Identity.Commands.LoginUser.LoginUserHandler)));
// builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

builder.Services.AddExceptionHandler<IdentityExceptionHandler>();
builder.Services.AddExceptionHandler<UnauthorizedExceptionHandler>();

builder.Services.AddProblemDetails();

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

app.Run();