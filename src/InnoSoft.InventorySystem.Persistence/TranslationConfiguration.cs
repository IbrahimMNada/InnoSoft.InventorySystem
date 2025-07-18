using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Persistence
{
    public abstract class TranslationConfiguration<T> : IEntityTypeConfiguration<T>
        where T : TranslationBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            ConfigureTranslationRoot(builder);
            builder.ToPluralizedTableName();
            builder.HasOne<Language>().WithMany().HasForeignKey(x => x.LanguageId);
            builder.HasKey(x => new { x.TranslationRootId, x.LanguageId });
        }
        public abstract void ConfigureTranslationRoot(EntityTypeBuilder<T> builder);


    }
}
