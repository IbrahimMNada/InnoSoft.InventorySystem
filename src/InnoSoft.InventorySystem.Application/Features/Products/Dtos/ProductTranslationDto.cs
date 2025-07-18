using InnoSoft.InventorySystem.Application.Localization;
using System;

namespace InnoSoft.InventorySystem.Application.Features.Products.Dtos
{
    public class ProductTranslationDto : IWrriteTranslationDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string Language { get; set; }
    }
}
