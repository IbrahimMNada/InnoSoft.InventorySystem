using InnoSoft.InventorySystem.Application.Authentication;
using InnoSoft.InventorySystem.Core.Abstractions;
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
        private readonly ICurrentUser _currentUser;

        public AuthController(AuthenticationService authenticationService, ICurrentUser currentUser)
        {
            _authenticationService = authenticationService;
            _currentUser = currentUser;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<AuthenticationResult> Login([FromBody] AuthenticationRequest authenticationRequest)
        {
            var result = _authenticationService.Authenticate(authenticationRequest.Username, authenticationRequest.Password);
            return Ok(result);
        }

        [HttpGet("current-user")]
        public ActionResult<ICurrentUser> CurrentUser()
        {
            return Ok(_currentUser);
        }
    }
}
