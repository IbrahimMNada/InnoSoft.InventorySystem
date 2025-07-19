using AutoMapper;
using InnoSoft.InventorySystem.Application.Features.Products.Commands;
using InnoSoft.InventorySystem.Application.Features.Products.Dtos;
using InnoSoft.InventorySystem.Application.SignalRHubs;
using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Entities.Categories;
using InnoSoft.InventorySystem.Core.Entities.Products;
using InnoSoft.InventorySystem.Core.Exceptions;
using InnoSoft.InventorySystem.Infrastructure;
using InnoSoft.InventorySystem.Persistence;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Products.Handlers
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _repository;
        private readonly IHubContext<ProductsHub> _hubContext;
        private readonly ICurrentUser _currentUser;

        public UpdateProductCommandHandler(IRepository<Product> repository, IMapper mapper, IHubContext<ProductsHub> hubContext, ICurrentUser currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _hubContext = hubContext;
            _currentUser = currentUser;
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
            await _hubContext.Clients.All.SendAsync("ProductUpdated", new { request = request, initator = _currentUser.UserId }, cancellationToken);
            return true;
        }
    }
}
