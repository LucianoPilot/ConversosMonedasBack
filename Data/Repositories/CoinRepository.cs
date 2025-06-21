using ConversorMonedas.Data.Entities;
using ConversorMonedas.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConversorMonedas.Data.Repositories
{
    public class CoinRepository : ICoinRepository
    {
        private readonly ApplicationDbContext _context;

        public CoinRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Coin>> GetAllAsync() =>
            await _context.Coins.ToListAsync();

        public async Task<Coin?> GetByIdAsync(int id) =>
            await _context.Coins.FindAsync(id);

        public async Task<Coin?> GetByCodeAsync(string code) =>
            await _context.Coins.FirstOrDefaultAsync(c => c.Code == code);

        public async Task AddAsync(Coin coin) =>
            await _context.Coins.AddAsync(coin);

        public async Task<bool> ExistsByCodeAsync(string code) =>
            await _context.Coins.AnyAsync(c => c.Code == code);

        public async Task DeleteAsync(Coin coin)
        {
            _context.Coins.Remove(coin);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
    

}
