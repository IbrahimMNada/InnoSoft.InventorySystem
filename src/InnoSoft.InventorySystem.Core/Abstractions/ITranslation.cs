using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Core.Abstractions
{
    public interface ITranslation
    {
        Guid TranslationRootId { get; set; }
        Guid LanguageId { get; set; }
    }
}
