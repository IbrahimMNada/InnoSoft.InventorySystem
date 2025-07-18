using AutoMapper;
using InnoSoft.InventorySystem.Application.Features.Products.Commands;
using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Entities.Categories;
using InnoSoft.InventorySystem.Core.Entities.Products;
using InnoSoft.InventorySystem.Core.Exceptions;
using InnoSoft.InventorySystem.Infrastructure;
using InnoSoft.InventorySystem.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Products.Handlers
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, bool>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(Product), request.Id);
            }
            _mapper.Map(request, entity);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
