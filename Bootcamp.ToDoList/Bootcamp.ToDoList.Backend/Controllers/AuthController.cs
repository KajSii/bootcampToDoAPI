
using Bootcamp.ToDoList.Backend.Entities.Domain;
using Bootcamp.ToDoList.Backend.Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.eShop.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(
            [FromBody, Bind] UserDto request,
            CancellationToken ct)
        {
            try {
                var user = new User()
                {
                    UserName = request.Username,
                };
                var userId = await _authService.Register(user, request.Password);

                return Ok(userId);
            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(
            [FromBody, Bind] UserDto request,
            CancellationToken ct)
        {
            try {
                var jwtToken = await _authService.Login(request.Username, request.Password);
                return Ok(jwtToken);
            } 
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }
    }
}