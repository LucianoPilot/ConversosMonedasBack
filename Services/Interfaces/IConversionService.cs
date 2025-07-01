using ConversorMonedas.Models;

namespace ConversorMonedas.Services.Interfaces
{
    public interface IConversionService
    {
        Task<CurrencyConversionResponseDto?> ConvertAsync(int userId, string fromCode, string toCode, decimal amount);
    }

}
