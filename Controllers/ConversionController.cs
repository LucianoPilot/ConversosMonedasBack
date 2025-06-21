using ConversorMonedas.Models;
using ConversorMonedas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConversorMonedas.Controllers
{
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
                var result = await _service.ConvertAsync(dto);
                if (result == null)
                    return BadRequest("Error en la conversión o usuario inválido");

                return Ok(result);
            }
        }
    }
