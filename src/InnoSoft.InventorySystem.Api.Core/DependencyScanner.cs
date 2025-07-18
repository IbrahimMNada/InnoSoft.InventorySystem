using AutoMapper;
using ConsultationPlatformService.Application.Localization;
using FluentValidation;
using InnoSoft.InventorySystem.Application;
using InnoSoft.InventorySystem.Application.Localization;
using InnoSoft.InventorySystem.Core;
using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Infrastructure;
using InnoSoft.InventorySystem.Infrastructure.Commands;
using InnoSoft.InventorySystem.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Query;
using System.Reflection;

namespace ConsultationPlatformService.Extensions
{
    public static class DependencyScanner
    {
        public const string RootNamespace = "InnoSoft.";

        private static IEnumerable<Assembly> _projectAssembles = null;
        public static IEnumerable<Assembly> GetProjectAssemblies(string rootNamespace = "InnoSoft.")
        {
            if (_projectAssembles == null)
                _projectAssembles = AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.IsDynamic && x.FullName.Contains(rootNamespace)).ToList();

            return _projectAssembles;
        }

        public static IServiceCollection ScanAndRegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<ICorrelationIdAccessor, CorrelationIdAccessor>();
            services.AddScoped<ILanguageService, LanguageService>();

            services.AddScoped<DbContextDependencies>();
            services.AddHttpContextAccessor();
            var assemblies = MakeSureAllAssembliesAreLoaded();
            services.Scan(scan =>
            {
                scan.FromAssemblies(assemblies)
                    .AddClasses(classes => classes.Where(x => !IsExcludedType(x)))
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime();
            });
            return services;
        }

        private static bool IsExcludedType(Type type)
        {
            return
                type.IsAssignableTo(typeof(Exception)) ||
                type.IsAssignableTo(typeof(ISqlExpressionFactory)) ||
                type.IsAssignableTo(typeof(ICommand)) ||
                type.IsAssignableTo(typeof(Command<>)) ||
                type.IsAssignableTo(typeof(AbstractValidator<>)) ||
                type.IsAssignableTo(typeof(IHostedService)) ||
                type.IsAssignableTo(typeof(BackgroundService)) ||
                type.IsAssignableTo(typeof(Attribute)) ||
                type.IsAssignableTo(typeof(IAuthorizationRequirement)) ||
                type.IsAssignableTo(typeof(Profile)) ||
                type.IsAssignableTo(typeof(ILanguageService)) ||
                type.IsAssignableTo(typeof(IUnitOfWork)) ||
                type.IsAssignableTo(typeof(ICommandHandler<,>)) ||
                type.GetCustomAttribute<DependencyScannerIgnoreAttribute>() != null;

            //type.IsAssignableTo(typeof(IRefitClient)) ||
            //type.Namespace == typeof(CorrelationIdMiddleware).Namespace ||
            //type.Namespace == typeof(UserValidationMiddelware).Namespace ||
            //type == typeof(QueryExecuterOptions) ||
            //type.IsRecordType() ||
            //type.IsAssignableTo(typeof(SecurityEnumsProvider)) ||
            //type.IsAssignableTo(typeof(WorkflowEnumsProvider)) ||
            //type.IsAssignableTo(typeof(ICommandHandler<,>)) ||
            //type.Assembly == typeof(ObjectUtils).Assembly ||

        }

        private static IEnumerable<Assembly> MakeSureAllAssembliesAreLoaded()
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.IsDynamic && x.FullName.StartsWith(RootNamespace)).ToList();
            var location = Directory.GetParent(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath).FullName;
            foreach (var assemblyFile in Directory.EnumerateFiles(location, $"{RootNamespace}*.dll"))
            {
                if (!loadedAssemblies.Any(x => new Uri(x.Location).LocalPath.Equals(assemblyFile, StringComparison.OrdinalIgnoreCase)))
                    loadedAssemblies.Add(Assembly.LoadFrom(assemblyFile));
            }
            return loadedAssemblies;
        }
    }
}
