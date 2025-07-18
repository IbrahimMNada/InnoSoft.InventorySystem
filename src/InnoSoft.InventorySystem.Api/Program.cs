using ConsultationPlatformService.Extensions;
using InnoSoft.InventorySystem.Api.Core;
using InnoSoft.InventorySystem.Application.Features.Categories.Commands;
using InnoSoft.InventorySystem.Extensions;
using InnoSoft.InventorySystem.Persistence;
using InnoSoft.InventorySystem.Persistence.DataSeeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(ConfigureMvc).AddJsonOptions(ConfigureJsonOptions);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.ScanAndRegisterDependencies();
builder.Services.AddSrilog("InnoSoft.InventorySystem.Api");
builder.Services.ConfigureValidators();
builder.Services.ConfigureAutoMapper();
builder.Services.AddAppLocalization();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCategoryCommand).Assembly));
builder.Services.AddScoped<DbContextDependencies>();
builder.Services.AddDbContext<DbContext, MainDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Application")));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Assembly.GetEntryAssembly().GetName().Name} v1"));
}

//app.UseHttpsRedirection();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.UseExceptionHandler("/error");
app.UseAuthorization();
app.UseRouting();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();
    dbContext.Database.Migrate();
}


await LanguageSeeder.SeedAsync(app.Services);
await CategorySeeder.SeedAsync(app.Services);

app.Run();



void ConfigureJsonOptions(JsonOptions options)
{
    options.JsonSerializerOptions.Converters.AddRange(AppJsonSerializerExtensions.DefaultConverters);
    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
}

void ConfigureMvc(MvcOptions options)
{
    //  options.Filters.Add<AuthorizeActionFilter>();
    //   options.Filters.Add(new AuthorizeFilter(_policySchemeName));

    //options.FluentValidationModelValidatorProvider.Clear();
    //options.ModelValidatorProviders.Clear();
}