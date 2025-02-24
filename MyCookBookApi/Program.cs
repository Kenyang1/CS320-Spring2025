using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

using MyCookBookApi.Models;
using MyCookBookApi.Services;
using MyCookBookApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add CORS policy to allow all origins, methods, and headers
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Register Services and Repositories
builder.Services.AddSingleton<IRecipeRepository, MockRecipeRepository>(); // Singleton keeps our in-memory data alive during the app's lifetime.
builder.Services.AddScoped<IRecipeService, RecipeService>(); // Scoped: a new service instance per request.

// Add controllers and ensure enums are properly serialized as strings
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Optionally, register other services like Endpoints API Explorer if needed
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline to use CORS
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
