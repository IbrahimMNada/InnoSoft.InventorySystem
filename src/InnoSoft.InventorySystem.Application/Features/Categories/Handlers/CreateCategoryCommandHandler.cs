using InnoSoft.InventorySystem.Application.Features.Categories.Commands;
using InnoSoft.InventorySystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Categories.Handlers
{
    public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, Guid>
    {
        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {


            return Guid.NewGuid();
        }
    }
}
