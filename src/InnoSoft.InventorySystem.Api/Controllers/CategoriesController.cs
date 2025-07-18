using InnoSoft.InventorySystem.Api.Core.ActionFilters;
using InnoSoft.InventorySystem.Application.Common;
using InnoSoft.InventorySystem.Application.Features.Categories.Commands;
using InnoSoft.InventorySystem.Application.Features.Categories.Dtos;
using InnoSoft.InventorySystem.Application.Features.Categories.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoSoft.InventorySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICategoryReadService _categoryReadService;

        public CategoriesController(IMediator mediator, ICategoryReadService categoryReadService)
        {
            _mediator = mediator;
            _categoryReadService = categoryReadService;
        }

        [HttpPost]
        [Route("")]
        [RequirePermission(Permissions.CreateCategory)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            return (await _mediator.Send(createCategoryCommand));
        }

        [HttpPut]
        [Route("")]
        [RequirePermission(Permissions.UpdateCategory)]
        public async Task<ActionResult<bool>> Update([FromBody] UpdateCategoryCommand createCategoryCommand)
        {
            return Ok(await _mediator.Send(createCategoryCommand));
        }

        [HttpGet]
        [Route("")]
        [RequirePermission(Permissions.ViewCategory)]
        public async Task<ActionResult<PagedResult<CategoryDto>>> GetList([FromQuery] PagedQuery query)
        {
            return Ok(await _categoryReadService.GetCategories(query));
        }

        [HttpDelete]
        [Route("{id}")]
        [RequirePermission(Permissions.DeleteCategory)]
        public async Task<ActionResult<bool>> DeleteItem(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteCategoryCommand() { Id = id }));
        }

        [HttpGet]
        [Route("administration/{id}")]
        [RequirePermission(Permissions.ViewCategory)]
        public async Task<ActionResult<CategoryAdministrationDto>> GetCategoryById(Guid id)
        {
            return Ok(await _categoryReadService.GetCategoryById(id));
        }

        [HttpGet]
        [Route("administration")]
        [RequirePermission(Permissions.ViewCategory)]
        public async Task<ActionResult<PagedResult<CategoryAdministrationDto>>> GetAllCategories([FromQuery] PagedQuery query)
        {
            return Ok(await _categoryReadService.GetAllCategories(query));
        }
    }
}
