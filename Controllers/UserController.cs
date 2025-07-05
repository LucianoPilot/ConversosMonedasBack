using ConversorMonedas.Models;
using ConversorMonedas.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConversorMonedas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [Authorize]
        [HttpPut("subscription/{subscriptionId}")]
        public async Task<IActionResult> ChangeSubscription(int subscriptionId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _service.ChangeSubscription(userId, subscriptionId);

            if (!result)
                return BadRequest("Usuario no encontrado");

            return Ok(new { message = "Suscripción actualizada y contador reiniciado" });
        }

        [HttpGet("me/conversion-count")]
        [Authorize]
        public async Task<IActionResult> GetConversionCount()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);
            var count = await _service.GetConversionCountAsync(userId);

            if (count is null)
                return NotFound("Usuario no encontrado");

            return Ok(count);
        }


        [HttpPost]

        public async Task<IActionResult> CreateUser(UserCreateDto dto)
        {
            var user = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

    }
}
