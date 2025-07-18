using AutoMapper;
using AutoMapper.EquivalencyExpression;
using FluentValidation;
using FluentValidation.AspNetCore;
using InnoSoft.InventorySystem.Api.Core.SwaggerOperationFilters;
using InnoSoft.InventorySystem.Application.Features.Categories;
using InnoSoft.InventorySystem.Application.Features.Categories.Validators;
using InnoSoft.InventorySystem.Application.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using System.Globalization;

namespace InnoSoft.InventorySystem.Api.Core
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddSrilog(this IServiceCollection services, string policySchemeName)
        {
            Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .Enrich.WithEnvironmentName()
                    .Enrich.WithEnvironmentUserName()
                    .Enrich.WithMachineName()
                    //  .Enrich.WithClientAgent()
                    .Enrich.WithClientIp()
                    .Enrich.WithExceptionDetails()
                    .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder())
                    .WriteTo.Console()
                    .WriteTo.Debug()
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
                    .Destructure.ToMaximumDepth(1)
                    .CreateLogger();

            services.AddLogging(builder => builder.AddSerilog(dispose: true));
            var loggerFactory = new LoggerFactory().AddSerilog(Log.Logger);
            services.AddSingleton(loggerFactory.CreateLogger(policySchemeName));
            Serilog.Debugging.SelfLog.Enable(msg => System.Diagnostics.Debug.WriteLine("Serilog: " + msg));

            return services;
        }



        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { cfg.AddCollectionMappers(); }, typeof(CategoriesMappingProfile));
            return services;
        }

        public static IServiceCollection ConfigureValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>();
            return services;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<LanguageFilter>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });


                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
            return services;
        }

        public static IServiceCollection AddAppLocalization(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                new CultureInfo("en"),
                new CultureInfo("ar")
            };

                options.DefaultRequestCulture = new RequestCulture("ar");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                // Accept language from header or query
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                new QueryStringRequestCultureProvider(),
                new AppAcceptLanguageHeaderRequestCultureProvider()
                };

            });
            return services;
        }
    }
}
