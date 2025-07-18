using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Core.Abstractions
{
    public class TranslationBase : ITranslation
    {
        public int TranslationRootId { get; set; }
        public int LanguageId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
