using DevOne.Security.Cryptography.BCrypt;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using ResumeSmart.Api.Configs;
using ResumeSmart.Api.DB;
using ResumeSmart.Api.Services;
using ResumeSmart.Api.Services.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => 
    loggerConfig.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddSingleton<IMongoDatabase>(_ =>
{
    var client = new MongoClient(builder.Configuration.GetConnectionString("MongoUri"));
    return client.GetDatabase(DbConstents.DatabaseName);
});

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    var dbContext = app.Services.GetService<MongoDbContext>();
    dbContext?.EnsureIndexesAsync();
}

app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();