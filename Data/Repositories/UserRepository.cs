using ConversorMonedas.Data;
using ConversorMonedas.Data.Entities;
using ConversorMonedas.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConversorMonedas.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.Include(u => u.Subscription).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.Include(u => u.Subscription)
                                       .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.Include(u => u.Subscription)
                                       .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByUsernameAndPassword(string name, string password)
        {
            return await _context.Users
                .Include(u => u.Subscription) // si necesitás el rol o suscripción
                .FirstOrDefaultAsync(u => u.Name == name && u.Password == password);
        }

        public User? Authenticate(string username, string password)
        {

            User? userToAuthenticate = _context.Users.FirstOrDefault(u => u.Name == username && u.Password == password);
            return userToAuthenticate;

        }
    }
}
