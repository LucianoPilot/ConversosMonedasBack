using ConversorMonedas.Data;
using ConversorMonedas.Data.Entities;
using ConversorMonedas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConversorMonedas.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ConversorMonedas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoinsController : ControllerBase
    {
        private readonly ICoinService _service;

        public CoinsController(ICoinService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var coins = await _service.GetAllAsync();
            return Ok(coins);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Coin>> GetCoin(int id)
        {
            var coin = await _service.GetByIdAsync(id);
            if (coin == null)
                return NotFound();

            return Ok(coin);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateCoin(CoinDto dto)
        {
            var coin = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetCoin), new { id = coin.Id }, coin);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateCoin(int id, CoinDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCoin(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

    }
}
