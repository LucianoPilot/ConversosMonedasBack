using ConversorMonedas.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConversorMonedas.Data.Interfaces
{
    public interface ICoinRepository
    {
        Task<IEnumerable<Coin>> GetAllAsync();
        Task<Coin?> GetByIdAsync(int id);
        Task<Coin?> GetByCodeAsync(string code);
        Task AddAsync(Coin coin);
        Task<bool> ExistsByCodeAsync(string code);
        Task DeleteAsync(Coin coin);
        Task SaveChangesAsync();
    }
}
