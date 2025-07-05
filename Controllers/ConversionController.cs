using ConversorMonedas.Models;
using ConversorMonedas.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConversorMonedas.Controllers
{

        [Authorize]
        [ApiController]
        [Route("api/[controller]")]
        public class ConversionController : ControllerBase
        {
            private readonly IConversionService _service;

            public ConversionController(IConversionService service)
            {
                _service = service;
            }

            [HttpPost]
        public async Task<IActionResult> Convert([FromBody] CurrencyConversionRequestDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var result = await _service.ConvertAsync(userId, dto.FromCode, dto.ToCode, dto.Amount);
            if (result == null)
                return BadRequest("Conversión no válida o límite alcanzado");

            return Ok(result);
        }
    }
    }
