using AutoMapper;
using InnoSoft.InventorySystem.Application.Features.Categories.Dtos;
using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Categories.Services
{
    public class CategoryReadService : ICategoryReadService
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryReadService(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryAdministrationDto> GetCategoryById(Guid id)
        {
            var category = await _repository.GetById(id);
            return _mapper.Map<CategoryAdministrationDto>(category);
        }

        public async Task<IEnumerable<CategoryAdministrationDto>> GetAllCategories()
        {
            var categories = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryAdministrationDto>>(categories);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            var categories = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

    }
}
