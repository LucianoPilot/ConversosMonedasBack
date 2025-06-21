using ConversorMonedas.Models;

namespace ConversorMonedas.Services.Interfaces
{
    public interface IConversionService
    {
        Task<CurrencyConversionResponseDto?> ConvertAsync(CurrencyConversionRequestDto dto);

    }
}
