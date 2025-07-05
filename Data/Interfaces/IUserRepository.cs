using ConversorMonedas.Data.Entities;

namespace ConversorMonedas.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task DeleteAsync(User user);
        Task SaveChangesAsync();
        Task<int?> GetConversionCountAsync(int userId);

        Task<User?> GetUserByUsernameAndPassword(string name, string password);
    }
}
