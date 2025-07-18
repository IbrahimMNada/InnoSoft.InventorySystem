using AutoMapper;
using InnoSoft.InventorySystem.Application.Common;
using InnoSoft.InventorySystem.Application.Features.Categories.Dtos;
using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private IQueryable<Category> BaseQuery()
        {
            return _repository.Queryable()
                .Include(x => x.Translations)
                .AsNoTracking();
        }

        public async Task<CategoryAdministrationDto> GetCategoryById(Guid id)
        {
            var category = await BaseQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<CategoryAdministrationDto>(category);
        }

        public async Task<PagedResult<CategoryAdministrationDto>> GetAllCategories(PagedQuery query)
        {
            var baseQuery = BaseQuery();
            var totalCount = await baseQuery.CountAsync();
            var items = await baseQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();
            return new PagedResult<CategoryAdministrationDto>
            {
                Items = _mapper.Map<IEnumerable<CategoryAdministrationDto>>(items),
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
        }

        public async Task<PagedResult<CategoryDto>> GetCategories(PagedQuery query)
        {
            var baseQuery = BaseQuery();
            var totalCount = await baseQuery.CountAsync();
            var items = await baseQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();
            return new PagedResult<CategoryDto>
            {
                Items = _mapper.Map<IEnumerable<CategoryDto>>(items),
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
        }
    }
}
