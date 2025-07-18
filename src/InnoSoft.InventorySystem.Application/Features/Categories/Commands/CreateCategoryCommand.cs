using InnoSoft.InventorySystem.Application.Features.Categories.Dtos;
using InnoSoft.InventorySystem.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Categories.Commands
{
    public class CreateCategoryCommand : Command<Guid>
    {
        public required IEnumerable<CategoryTranslationDto> Translations { get; set; }
    }

}
