using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ResumeSmart.Api.Configs;
using ResumeSmart.Api.DB;
using ResumeSmart.Api.Models;
using ResumeSmart.Api.Services;
using ResumeSmart.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMongoDatabase>(_ =>
{
    var client = new MongoClient(builder.Configuration.GetConnectionString("MongoUri"));
    return client.GetDatabase(DbConstents.DatabaseName);
});

builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    var dbContext = app.Services.GetService<MongoDbContext>();
    dbContext?.EnsureIndexes();
}

app.MapControllers();

app.MapGet("/users", async (IUserService service) =>
{
    var users = await service.GetAllUsers();
    return Results.Ok(users);
});

app.MapPost("/users", async ([FromBody] CreateUserRequest request, IUserService service) =>
{
    try
    {
        await service.CreateUser(request);
        return Results.Created($"/users", null);
    }
    catch (Exception e)
    {   
        return Results.InternalServerError(e.Message);
    }
});

app.Run();