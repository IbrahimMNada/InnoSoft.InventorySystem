using AutoMapper;
using InnoSoft.InventorySystem.Application.Features.Categories.Commands;
using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Entities.Categories;
using InnoSoft.InventorySystem.Core.Exceptions;
using InnoSoft.InventorySystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Categories.Handlers
{
    public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, bool>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = _repository.GetById(request.Id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(Category), request.Id);
            }
            _mapper.Map(request, entity);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
