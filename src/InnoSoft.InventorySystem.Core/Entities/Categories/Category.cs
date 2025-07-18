using InnoSoft.InventorySystem.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Core.Entities.Categories
{
    public class Category : Entity, IAuditable, ITranslationRootOf<CategoryTranslation>
    {
        public ICollection<CategoryTranslation> Translations { get; set; }
    }
}
