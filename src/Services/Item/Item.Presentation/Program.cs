using FluentValidation;
using Item.BusinessLogic.Mappings;
using Item.BusinessLogic.Models.Validators;
using Item.BusinessLogic.Services.Implementations;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Data;
using Item.DataAccess.Repositories.Implementations;
using Item.DataAccess.Repositories.Interfaces;
using Item.Presentation.ExceptionHandlers;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
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

builder.Services.AddValidatorsFromAssemblyContaining<CategoryValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IRegionService, RegionService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(CategoryProfile)));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
