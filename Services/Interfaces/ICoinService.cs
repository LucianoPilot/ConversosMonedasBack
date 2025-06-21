using ConversorMonedas.Data.Entities;
using ConversorMonedas.Models;

namespace ConversorMonedas.Services.Interfaces
{
    public interface ICoinService
    {
        Task<IEnumerable<Coin>> GetAllAsync();
        Task<Coin?> GetByIdAsync(int id);
        Task<Coin> CreateAsync(CoinDto dto);
        Task<bool> UpdateAsync(int id, CoinDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
