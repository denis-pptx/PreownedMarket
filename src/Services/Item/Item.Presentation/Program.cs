using FluentValidation;
using Item.BusinessLogic.Mappings;
using Item.BusinessLogic.Models.Validators;
using Item.DataAccess.Data;
using Item.DataAccess.Data.Initializers;
using Item.Presentation.ExceptionHandlers;
using Item.Presentation.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => 
x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

var connection = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlite(connection));

builder.Services.AddValidatorsFromAssemblyContaining<CategoryValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddApplication();

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(CategoryProfile)));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();


var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await DataInitializer.Seed(app.Services);

app.Run();