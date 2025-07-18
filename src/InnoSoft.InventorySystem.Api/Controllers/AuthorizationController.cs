using InnoSoft.InventorySystem.Application.Authentication;
using InnoSoft.InventorySystem.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoSoft.InventorySystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;


        public AuthController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] AuthenticationRequest authenticationRequest)
        {
            var result = _authenticationService.Authenticate(authenticationRequest.Username, authenticationRequest.Password);
            return Ok(result);
        }
    }
}
