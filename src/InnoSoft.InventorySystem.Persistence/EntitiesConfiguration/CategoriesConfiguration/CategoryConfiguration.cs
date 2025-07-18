using InnoSoft.InventorySystem.Core.Entities.Categories;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Persistence.EntitiesConfiguration.CategoriesConfiguration
{
    internal class CategoryConfiguration : EntityMapping<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
        }
    }
}
