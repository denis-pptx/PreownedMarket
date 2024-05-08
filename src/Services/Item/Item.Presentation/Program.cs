using Item.DataAccess.Data;
using Item.DataAccess.Data.Initializers;
using Item.BusinessLogic.Grpc;
using Item.DataAccess;
using Item.BusinessLogic;
using Item.Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDataAccessServices(builder.Configuration);

builder.Services.AddBusinessLogicServices();

builder.Services.AddPresentationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations<ApplicationDbContext>();

    await app.SeedData();
}

app.MapGrpcService<ItemService>();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();