using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Persistence
{
    internal static class ModelBuilderExtensions
    {
        public static void RegisterAllEntities<BaseModel>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes()).Where(c => c.IsClass && !c.IsAbstract && c.IsPublic &&
              typeof(BaseModel).IsAssignableFrom(c));
            foreach (Type type in types)
                modelBuilder.Entity(type);
        }
        public static EntityTypeBuilder<T> ToPluralizedTableName<T>(this EntityTypeBuilder<T> builder) where T : class
        {
            builder.ToTable(typeof(T).Name.Pluralize());
            return builder;
        }
        public static EntityTypeBuilder<T> AddAuditingProperties<T>(this EntityTypeBuilder<T> builder) where T : class
        {
            builder.Property<Guid?>("CreatedBy");
            builder.Property<DateTime>("CreatedAt");
            builder.Property<Guid?>("LastUpdatedBy");
            builder.Property<DateTime?>("LastUpdatedAt");
            // builder.HasOne<User>().WithMany().HasForeignKey("CreatedBy");
            return builder;
        }
    }
}
