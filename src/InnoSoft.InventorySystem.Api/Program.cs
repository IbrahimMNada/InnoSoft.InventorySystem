using ConsultationPlatformService.Extensions;
using InnoSoft.InventorySystem.Api.Core;
using InnoSoft.InventorySystem.Application;
using InnoSoft.InventorySystem.Application.Authentication;
using InnoSoft.InventorySystem.Application.Features.Categories.Commands;
using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Extensions;
using InnoSoft.InventorySystem.Persistence;
using InnoSoft.InventorySystem.Persistence.DataSeeds;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(ConfigureMvc).AddJsonOptions(ConfigureJsonOptions);


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


var jwtKey = builder.Configuration["JwtSettings:Key"];
var jwtIssuer = builder.Configuration["JwtSettings:Issuer"];
var jwtAudience = builder.Configuration["JwtSettings:Audience"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtIssuer,
        ValidateAudience = true,
        ValidAudience = jwtAudience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization();

builder.Services.AddSingleton<TokenService>();
builder.Services.AddSingleton<AuthenticationService>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

var allowedOrigins = builder.Configuration["Cors:AllowedOrigins"]
    ?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
    ?? Array.Empty<string>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins(allowedOrigins)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials(); // if needed
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Assembly.GetEntryAssembly().GetName().Name} v1"));
}

//app.UseHttpsRedirection();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.UseCors("CorsPolicy");
app.MapControllers();
app.UseExceptionHandler("/error");


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();
    dbContext.Database.Migrate();
}


await LanguageSeeder.SeedAsync(app.Services);
await CategorySeeder.SeedAsync(app.Services);
await ProductSeeder.SeedAsync(app.Services);
app.Run();



void ConfigureJsonOptions(JsonOptions options)
{
    options.JsonSerializerOptions.Converters.AddRange(AppJsonSerializerExtensions.DefaultConverters);
    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
}

void ConfigureMvc(MvcOptions options)
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
}