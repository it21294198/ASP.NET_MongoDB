using System;
using ASP.NET_MongoDB.Services;

var builder = WebApplication.CreateBuilder(args);

// Set base path and load environment variables from .env file in development
if (builder.Environment.IsDevelopment())
{
    try
    {
        DotNetEnv.Env.Load();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading environment variables: {ex.Message}");
        throw; // Rethrow the exception
    }
}

builder.Services.AddScoped<UserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add configuration sources, including environment variables
builder.Configuration
    .AddEnvironmentVariables()  // Adds environment variables to the configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddCommandLine(args);

var app = builder.Build();

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

