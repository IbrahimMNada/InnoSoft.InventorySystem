using AutoMapper;
using InnoSoft.InventorySystem.Application.Common;
using InnoSoft.InventorySystem.Application.Features.Products.Dtos;
using InnoSoft.InventorySystem.Core.Abstractions;
using InnoSoft.InventorySystem.Core.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Products.Services
{
    public class ProductReadService : IProductReadService
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductReadService(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private IQueryable<Product> BaseQuery()
        {
            return _repository.Queryable()
                .Include(x => x.Translations)
                .Include(x => x.Category).ThenInclude(x => x.Translations)
                .OrderByDescending(x => x.Id)
                .AsSplitQuery()
                .AsNoTracking();
        }

        public async Task<ProductAdministrationDto> GetProductById(Guid id)
        {
            var product = await BaseQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<ProductAdministrationDto>(product);
        }

        public async Task<PagedResult<ProductAdministrationDto>> GetAllProducts(Guid? categoryId = null, double? maxQuantity = null, PagedQuery query = default)
        {
            var baseQuery = BaseQuery();
            if (categoryId.HasValue)
                baseQuery = baseQuery.Where(x => x.CategoryId == categoryId.Value);
            if (maxQuantity.HasValue)
                baseQuery = baseQuery.Where(x => x.Quantity < maxQuantity.Value);
            var totalCount = await baseQuery.CountAsync();
            var items = await baseQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();
            return new PagedResult<ProductAdministrationDto>
            {
                Items = _mapper.Map<IEnumerable<ProductAdministrationDto>>(items),
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
        }

        public async Task<PagedResult<ProductDto>> GetProducts(Guid? categoryId = null, double? maxQuantity = null, PagedQuery query = default)
        {
            var baseQuery = BaseQuery();
            if (categoryId.HasValue)
                baseQuery = baseQuery.Where(x => x.CategoryId == categoryId.Value);
            if (maxQuantity.HasValue)
                baseQuery = baseQuery.Where(x => x.Quantity < maxQuantity.Value);
            var totalCount = await baseQuery.CountAsync();
            var items = await baseQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();
            return new PagedResult<ProductDto>
            {
                Items = _mapper.Map<IEnumerable<ProductDto>>(items),
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
        }
    }
}
