using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Core.Abstractions
{
    public class TranslationBase : ITranslation
    {
        public Guid TranslationRootId { get; set; }
        public Guid LanguageId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
