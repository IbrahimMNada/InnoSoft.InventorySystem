using AutoMapper;
using InnoSoft.InventorySystem.Application.Features.Products.Commands;
using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Entities.Products;
using InnoSoft.InventorySystem.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Products.Handlers
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Product>(request);
            await _repository.Add(entity);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
