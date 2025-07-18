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
    public abstract class IdentifiedObjectMapping<T> : IEntityTypeConfiguration<T> where T : IdentifiedObject
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn();
        }

    }
}
