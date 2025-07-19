using ConsultationPlatformService.Extensions;
using InnoSoft.InventorySystem.Api.Core;
using InnoSoft.InventorySystem.Api.Core.BackgroundJobs;
using InnoSoft.InventorySystem.Application;
using InnoSoft.InventorySystem.Application.Authentication;
using InnoSoft.InventorySystem.Application.Features.Categories.Commands;
using InnoSoft.InventorySystem.Application.SignalRHubs;
using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Extensions;
using InnoSoft.InventorySystem.Localization;
using InnoSoft.InventorySystem.Persistence;
using InnoSoft.InventorySystem.Persistence.DataSeeds;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            $"{context.Connection.RemoteIpAddress}:{context.Request.Path}",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 50,
                Window = TimeSpan.FromSeconds(10),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                //QueueLimit = 2
            }));

    options.RejectionStatusCode = 429;
    options.OnRejected = async (context, token) =>
    {
        var localizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<SharedResource>>();
        context.HttpContext.Response.ContentType = "application/json";
        await context.HttpContext.Response.WriteAsync("{\"error\":\"" + localizer["RateLimitExceeded"] + "\"}", token);
    };
});

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

builder.Services.AddHostedService<NotifyLowStockOfProductsHostedService>();

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
        ClockSkew = TimeSpan.Zero,
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;

            if (!string.IsNullOrEmpty(accessToken) &&
                path.StartsWithSegments("/ProductsHub"))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        }
    };
});
builder.Services.AddAuthorization();

builder.Services.AddSingleton<TokenService>();
builder.Services.AddSingleton<AuthenticationService>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

var allowedOrigins = builder.Configuration["Cors:AllowedOrigins"]
    ?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
    ?? Array.Empty<string>();

builder.Services.AddSignalR();
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

app.UseExceptionHandler("/error");
app.UseRateLimiter();
//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ProductsHub>("/ProductsHub");

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