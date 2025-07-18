using InnoSoft.InventorySystem.Core.Exceptions;
using InnoSoft.InventorySystem.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace InnoSoft.InventorySystem.Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ProblemDetailsFactory _problemDetailsFactory;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public ErrorController(ILogger logger, ProblemDetailsFactory problemDetailsFactory, IStringLocalizer<SharedResource> stringLocalizer)
        {
            _logger = logger;
            _problemDetailsFactory = problemDetailsFactory;
            _stringLocalizer = stringLocalizer;
        }

        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            _logger.LogError(context.Error, context.Error.ToString());

            if (context.Error is AggregateException)
            {
                var aggregateException = context.Error as AggregateException;
                var problem = _problemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, detail: "Aggregate Exception");

                foreach (var exception in aggregateException.InnerExceptions)
                {
                    if (exception is DomainException)
                    {

                    }

                }
                return new ObjectResult(problem) { StatusCode = StatusCodes.Status400BadRequest };
            }

            if (context.Error is EntityNotFoundException)
            {
                var validationException = context.Error as EntityNotFoundException;
                var problem = _problemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, detail: _stringLocalizer[validationException.Message]);

                return new ObjectResult(problem) { StatusCode = StatusCodes.Status400BadRequest };
            }

            if (context.Error is DomainException)
            {
                var domainException = context.Error as DomainException;
                var problem = _problemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, detail: _stringLocalizer[domainException.Message, domainException.ExtraInput]);

                return new ObjectResult(problem) { StatusCode = StatusCodes.Status400BadRequest };
            }

            else if (context.Error is ValidationException)
            {
                var validationException = context.Error as ValidationException;
                var problem = _problemDetailsFactory.CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, detail: _stringLocalizer[validationException.Message]);
                return new ObjectResult(problem) { StatusCode = StatusCodes.Status400BadRequest };
            }

            return Problem(context.Error.ToString(), statusCode: StatusCodes.Status500InternalServerError);
        }

    }
}
