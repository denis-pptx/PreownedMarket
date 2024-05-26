using ApiGateway.Options.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json");

builder.Services.AddOcelot(builder.Configuration);

builder.Services
    .ConfigureOptions<JwtOptionsSetup>()
    .ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

var app = builder.Build();

await app.UseOcelot();

app.UseAuthentication();

app.Run();