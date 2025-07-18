using InnoSoft.InventorySystem.Api.Core.ActionFilters;
using InnoSoft.InventorySystem.Application.Common;
using InnoSoft.InventorySystem.Application.Features.Products.Commands;
using InnoSoft.InventorySystem.Application.Features.Products.Dtos;
using InnoSoft.InventorySystem.Application.Features.Products.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InnoSoft.InventorySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IProductReadService _productReadService;
        public ProductsController(IMediator mediator, IProductReadService productReadService)
        {
            _mediator = mediator;
            _productReadService = productReadService;
        }

        [HttpPost]
        [Route("")]
        [RequirePermission(Permissions.CreateProduct)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateProductCommand createProductCommand)
        {
            return (await _mediator.Send(createProductCommand));
        }

        [HttpPut]
        [Route("")]
        [RequirePermission(Permissions.UpdateProduct)]
        public async Task<ActionResult<bool>> Update([FromBody] UpdateProductCommand updateProductCommand)
        {
            return Ok(await _mediator.Send(updateProductCommand));
        }

        [HttpGet]
        [Route("")]
        [RequirePermission(Permissions.ViewProduct)]
        public async Task<ActionResult<PagedResult<ProductDto>>> GetList(
            [FromQuery] Guid? categoryId = null,
            [FromQuery] double? maxQuantity = null,
            [FromQuery] PagedQuery query = default)
        {
            return Ok(await _productReadService.GetProducts(categoryId, maxQuantity, query));
        }

        [HttpDelete]
        [Route("{id}")]
        [RequirePermission(Permissions.DeleteProduct)]
        public async Task<ActionResult<bool>> DeleteItem(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteProductCommand() { Id = id }));
        }

        [HttpGet]
        [Route("administration/{id}")]
        [RequirePermission(Permissions.ViewProduct)]
        public async Task<ActionResult<ProductAdministrationDto>> GetProductById(Guid id)
        {
            return Ok(await _productReadService.GetProductById(id));
        }

        [HttpGet]
        [Route("administration")]
        [RequirePermission(Permissions.ViewProduct)]
        public async Task<ActionResult<PagedResult<ProductAdministrationDto>>> GetAllProducts(
            [FromQuery] Guid? categoryId = null,
            [FromQuery] double? maxQuantity = null,
            [FromQuery] PagedQuery query = default)
        {
            return Ok(await _productReadService.GetAllProducts(categoryId, maxQuantity, query));
        }
    }
}
