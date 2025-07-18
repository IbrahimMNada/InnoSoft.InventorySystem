using InnoSoft.InventorySystem.Application.Common;
using InnoSoft.InventorySystem.Application.Features.Categories.Dtos;
using System;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Categories.Services
{
    public interface ICategoryReadService
    {
        Task<PagedResult<CategoryDto>> GetCategories(PagedQuery query);
        Task<CategoryAdministrationDto> GetCategoryById(Guid id);
        Task<PagedResult<CategoryAdministrationDto>> GetAllCategories(PagedQuery query);
    }
}