using InnoSoft.InventorySystem.Application.Features.Categories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Products.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public decimal PricePerPiece { get; set; }
        public double Quantity { get; set; }
        public double AlertThresholdQuantity { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public CategoryDto Category { get; set; }
    }
}
