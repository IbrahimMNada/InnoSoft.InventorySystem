using InnoSoft.InventorySystem.Application.Features.Products.Dtos;
using InnoSoft.InventorySystem.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Products.Commands
{
    public class UpdateProductCommand : Command<bool>
    {
        public Guid Id { get; set; }
        public required IEnumerable<ProductTranslationDto> Translations { get; set; }
        public decimal PricePerPiece { get; set; }
        public double Quantity { get; set; }
        public double AlertThresholdQuantity { get; set; }
        public Guid CategoryId { get; set; }
    }
}
