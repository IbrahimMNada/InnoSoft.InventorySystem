using InnoSoft.InventorySystem.Core.Entities.Categories;
using InnoSoft.InventorySystem.Core.Entities.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Persistence.EntitiesConfiguration.CategoriesConfiguration
{
    internal class ProductTranslationConfiguration : TranslationConfiguration<ProductTranslation>
    {
        public override void Configure(EntityTypeBuilder<ProductTranslation> builder)
        {
            base.Configure(builder);
        }

        public override void ConfigureTranslationRoot(EntityTypeBuilder<ProductTranslation> builder)
        {
            builder.HasOne<Product>().WithMany(x => x.Translations).HasForeignKey(x => x.TranslationRootId);
        }
    }
}
