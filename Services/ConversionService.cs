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

        public async Task<CurrencyConversionResponseDto?> ConvertAsync(int userId, string fromCode, string toCode, decimal amount)
        {
            var user = await _userRepository.GetByIdAsync(userId);
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

            if (user.ConversionCount >= maxConversions)
                return null;

            var fromCoin = await _coinRepository.GetByCodeAsync(fromCode);
            var toCoin = await _coinRepository.GetByCodeAsync(toCode);
            if (fromCoin == null || toCoin == null)
                return null;

            decimal result = amount * fromCoin.IC / toCoin.IC;

            user.ConversionCount++;
            await _userRepository.SaveChangesAsync();

            return new CurrencyConversionResponseDto
            {
                FromCode = fromCode,
                ToCode = toCode,
                Amount = amount,
                Result = Math.Round(result, 2)
            };
        }
    }
}
