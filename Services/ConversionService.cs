using ConversorMonedas.Data.Interfaces;
using ConversorMonedas.Models;
using ConversorMonedas.Repositories.Interfaces;
using ConversorMonedas.Services.Interfaces;

namespace ConversorMonedas.Services
{
    public class ConversionService : IConversionService
    {
        private readonly ICoinRepository _coinRepository;
        private readonly IUserRepository _userRepository;

        public ConversionService(ICoinRepository coinRepository, IUserRepository userRepository)
        {
            _coinRepository = coinRepository;
            _userRepository = userRepository;
        }

        public async Task<CurrencyConversionResponseDto?> ConvertAsync(CurrencyConversionRequestDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null || user.Subscription == null)
                return null;

            var subscription = user.Subscription;
            var maxConversions = subscription.Type.ToLower() switch
            {
                "free" => 10,
                "trial" => 100,
                "pro" => int.MaxValue,
                _ => 0
            };

            // ❌ Límite alcanzado
            if (user.ConversionCount >= maxConversions)
                return null;

            var fromCoin = await _coinRepository.GetByCodeAsync(dto.FromCode);
            var toCoin = await _coinRepository.GetByCodeAsync(dto.ToCode);
            if (fromCoin == null || toCoin == null)
                return null;

            decimal result = dto.Amount * fromCoin.IC / toCoin.IC;

            // ✅ Incrementar contador y guardar
            user.ConversionCount++;
            await _userRepository.SaveChangesAsync();

            return new CurrencyConversionResponseDto
            {
                FromCode = dto.FromCode,
                ToCode = dto.ToCode,
                Amount = dto.Amount,
                Result = Math.Round(result, 2)
            };
        }
    }
}
