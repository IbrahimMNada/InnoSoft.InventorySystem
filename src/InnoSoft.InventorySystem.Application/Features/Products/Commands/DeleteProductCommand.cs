using InnoSoft.InventorySystem.Infrastructure.Commands;


namespace InnoSoft.InventorySystem.Application.Features.Products.Commands
{
    public class DeleteProductCommand : Command<bool>
    {
        public Guid Id { get; set; }
    }
}
