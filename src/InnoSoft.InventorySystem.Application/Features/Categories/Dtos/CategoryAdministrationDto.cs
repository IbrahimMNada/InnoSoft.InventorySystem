using System;
using System.Collections.Generic;

namespace InnoSoft.InventorySystem.Application.Features.Categories.Dtos
{
    public class CategoryAdministrationDto
    {
        public Guid Id { get; set; }
        public ICollection<CategoryTranslationDto> Translations { get; set; }
    }
}