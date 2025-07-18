using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Entities;
using InnoSoft.InventorySystem.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Persistence
{
    public class MainDbContext : DbContext, IUnitOfWork
    {
        private readonly DbContextDependencies _deps;

        public MainDbContext(DbContextOptions options, DbContextDependencies deps) : base(options)
        {
            _deps = deps;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var targetedAssembly = typeof(Language).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(targetedAssembly);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public Task BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
        {
            return Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        }

        public Task Commit(CancellationToken cancellationToken = default)
        {
            if (Database.CurrentTransaction != null)
                return Database.CommitTransactionAsync(cancellationToken);
            return Task.CompletedTask;
        }

        public Task ExecuteSql(string sql, CancellationToken cancellationToken = default)
        {
            return Database.ExecuteSqlRawAsync(sql, cancellationToken);
        }

        public Task Rollback(CancellationToken cancellationToken = default)
        {
            if (Database.CurrentTransaction != null)
                return Database.RollbackTransactionAsync(cancellationToken);
            return Task.CompletedTask;
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            SetSystemProperties();
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }




        private void SetSystemProperties()
        {
            ChangeTracker.Entries().Where(x => x.Entity is IAuditable).ForEach(entry =>
            {
                var entity = entry.Entity as Entity;
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedBy").CurrentValue = _deps?.CurrentUser?.Id ?? Guid.Empty;
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("LastUpdatedBy").CurrentValue = _deps?.CurrentUser?.Id ?? Guid.Empty;
                    entry.Property("LastUpdatedAt").CurrentValue = DateTime.Now;
                }
            });

        }

    }
}
