using ConversorMonedas.Models;
using ConversorMonedas.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPut("{id}/subscription/{subscriptionId}")]
        public async Task<IActionResult> ChangeSubscription(ChangeSubscriptionDto dto)
        {
            var result = await _service.ChangeSubscription(dto.UserId, dto.SubscriptionId);
            if (!result)
                return BadRequest("Usuario no encontrado");

            return Ok("Suscripción actualizada y contador reiniciado");
        }

        [HttpPost]

        public async Task<IActionResult> CreateUser(UserCreateDto dto)
        {
            var user = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

    }
}
