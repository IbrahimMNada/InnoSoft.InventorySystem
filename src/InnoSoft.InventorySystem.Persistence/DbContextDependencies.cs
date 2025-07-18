using InnoSoft.InventorySystem.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace InnoSoft.InventorySystem.Persistence
{
    public class DbContextDependencies
    {
        public DbContextDependencies(ICurrentUser currentUser, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ICorrelationIdAccessor correlationIdAccessor)
        {
            CurrentUser = currentUser;
            HttpContextAccessor = httpContextAccessor;
            Configuration = configuration;
            CorrelationIdAccessor = correlationIdAccessor;
        }

        public ICurrentUser CurrentUser { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }
        public IConfiguration Configuration { get; }
        public ICorrelationIdAccessor CorrelationIdAccessor { get; }
    }
}
