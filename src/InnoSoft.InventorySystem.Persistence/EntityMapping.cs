using InnoSoft.InventorySystem.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Persistence
{
    public class EntityMapping<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToPluralizedTableName()
                   .HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.HasQueryFilter(p => !p.IsDeleted);
            if (typeof(T).GetInterface(nameof(IAuditable)) != null)
                builder.AddAuditingProperties();
        }
    }
}
