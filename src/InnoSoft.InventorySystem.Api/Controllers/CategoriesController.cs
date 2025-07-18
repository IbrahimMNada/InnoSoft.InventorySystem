using InnoSoft.InventorySystem.Application.Common;
using InnoSoft.InventorySystem.Application.Features.Categories.Commands;
using InnoSoft.InventorySystem.Application.Features.Categories.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoSoft.InventorySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
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
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            return Ok(await _mediator.Send(createCategoryCommand));
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand createCategoryCommand)
        {
            return Ok(await _mediator.Send(createCategoryCommand));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetList([FromQuery] PagedQuery query)
        {
            return Ok(await _categoryReadService.GetCategories(query));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteCategoryCommand() { Id = id }));
        }

        [HttpGet]
        [Route("administration/{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            return Ok(await _categoryReadService.GetCategoryById(id));
        }

        [HttpGet]
        [Route("administration")]
        public async Task<IActionResult> GetAllCategories([FromQuery] PagedQuery query)
        {
            return Ok(await _categoryReadService.GetAllCategories(query));
        }
    }
}
