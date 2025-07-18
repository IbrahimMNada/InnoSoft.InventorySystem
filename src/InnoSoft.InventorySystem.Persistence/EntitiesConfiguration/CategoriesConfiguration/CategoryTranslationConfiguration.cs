using InnoSoft.InventorySystem.Core.Entities.Categories;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Persistence.EntitiesConfiguration.CategoriesConfiguration
{
    internal class CategoryTranslationConfiguration : TranslationConfiguration<CategoryTranslation>
    {
        public override void Configure(EntityTypeBuilder<CategoryTranslation> builder)
        {
            base.Configure(builder);
        }

        public override void ConfigureTranslationRoot(EntityTypeBuilder<CategoryTranslation> builder)
        {
            builder.HasOne<Category>().WithMany(x => x.Translations).HasForeignKey(x => x.TranslationRootId);
        }
    }
}
