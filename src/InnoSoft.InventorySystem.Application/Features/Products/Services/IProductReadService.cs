using InnoSoft.InventorySystem.Application.Common;
using InnoSoft.InventorySystem.Application.Features.Products.Dtos;
using System;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Products.Services
{
    public interface IProductReadService
    {
        Task<ProductAdministrationDto> GetProductById(Guid id);
        Task<PagedResult<ProductDto>> GetProducts(Guid? categoryId = null, double? maxQuantity = null, PagedQuery query = default);
        Task<PagedResult<ProductAdministrationDto>> GetAllProducts(Guid? categoryId = null, double? maxQuantity = null, PagedQuery query = default);
        Task<List<CategoryProductCountDto>> GetProductCountByCategoryAsync();
        Task<List<ProductDto>> GetProductsUnderLimit();
    }
}
