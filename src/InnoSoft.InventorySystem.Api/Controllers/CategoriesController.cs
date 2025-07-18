using InnoSoft.InventorySystem.Application.Features.Categories.Commands;
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
        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            return Ok(await _mediator.Send(createCategoryCommand));
        }
        //[HttpPut]
        //[Route("")]
        //public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand updateCategoryCommand)
        //{
        //    return Ok(await _mediator.Send(updateCategoryCommand));
        //}
    }
}
