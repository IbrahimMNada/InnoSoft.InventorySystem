using ConsultationPlatformService.Extensions;
using InnoSoft.InventorySystem.Extensions;
using InnoSoft.InventorySystem.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.ScanAndRegisterDependencies();

builder.Services.AddScoped<DbContextDependencies>();
builder.Services.AddDbContext<DbContext, MainDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Application")));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



void ConfigureJsonOptions(JsonOptions options)
{
    options.JsonSerializerOptions.Converters.AddRange(AppJsonSerializerExtensions.DefaultConverters);
    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
}

