using ConversorMonedas.Data.Entities;
using ConversorMonedas.Data.Interfaces;
using ConversorMonedas.Models;
using ConversorMonedas.Services.Interfaces;

namespace ConversorMonedas.Services
{
    public class CoinService : ICoinService
    {
        private readonly ICoinRepository _repository;

        public CoinService(ICoinRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Coin>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Coin?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Coin> CreateAsync(CoinDto dto)
        {
            dto.Code = dto.Code.ToUpper();

            if (dto.IC <= 0)
                throw new ArgumentException("El índice de convertibilidad debe ser mayor que 0.");

            if (await _repository.ExistsByCodeAsync(dto.Code))
                throw new InvalidOperationException("Ya existe una moneda con ese código.");

            var coin = new Coin
            {
                Code = dto.Code,
                Legend = dto.Legend,
                Symbol = dto.Symbol,
                IC = dto.IC
            };

            await _repository.AddAsync(coin);
            await _repository.SaveChangesAsync();

            return coin;
        }

        public async Task<bool> UpdateAsync(int id, CoinDto dto)
        {
            var coin = await _repository.GetByIdAsync(id);
            if (coin == null)
                return false;

            coin.Code = dto.Code.ToUpper();
            coin.Legend = dto.Legend;
            coin.Symbol = dto.Symbol;
            coin.IC = dto.IC;

            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var coin = await _repository.GetByIdAsync(id);
            if (coin == null)
                return false;

            await _repository.DeleteAsync(coin);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
