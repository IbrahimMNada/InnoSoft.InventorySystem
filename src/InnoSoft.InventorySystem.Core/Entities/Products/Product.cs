using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Entities.Categories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Core.Entities.Products
{
    public class Product : Entity, IAuditable, ITranslationRootOf<ProductTranslation>
    {
        public ICollection<ProductTranslation> Translations { get; set; }
        public decimal PricePerPiece { get; set; }
        public double Quantity { get; set; }
        public double AlertThresholdQuantity { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime? LastLowStockWarningDate { get; set; }
    }
}
