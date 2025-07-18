using InnoSoft.InventorySystem.Application.Features.Categories.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Application.Features.Categories.Services
{
    public interface ICategoryReadService
    {
        Task<IEnumerable<CategoryDto>> GetCategories();
        Task<CategoryAdministrationDto> GetCategoryById(Guid id);
        Task<IEnumerable<CategoryAdministrationDto>> GetAllCategories();
    }
}