using InnoSoft.InventorySystem.Application.Features.Categories.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Products.Dtos
{
    public class CategoryProductCountDto
    {
        public Guid CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public int ProductCount { get; set; }
    }
}
