using InnoSoft.InventorySystem.Application.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Categories.Dtos
{

    public class CategoryTranslationDto : IWrriteTranslationDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string Language { get; set; }
    }
}
