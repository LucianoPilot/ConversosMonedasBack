using ConversorMonedas.Data.Entities;
using ConversorMonedas.DTOs;
using ConversorMonedas.Models;
using ConversorMonedas.Data.Repositories;
using ConversorMonedas.Repositories.Interfaces;
using ConversorMonedas.Services.Interfaces;

namespace ConversorMonedas.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            return users.Select(u => u.ToDto()).ToList();
        }

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user?.ToDto();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return false;

            await _repository.DeleteAsync(user);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeSubscription(int userId, int subscriptionId)
        {
            var user = await _repository.GetByIdAsync(userId);
            if (user == null) return false;

            user.SubscriptionId = subscriptionId;
            user.ConversionCount = 0;
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<UserResponseDto> CreateAsync(UserCreateDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                SubscriptionId = dto.SubscriptionId,
                Rol = dto.Role,
                ConversionCount = dto.ConversionCount
            };

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();

            return user.ToDto();
        }

        public async Task<User?> ValidateUser(AuthDTO auth)
        {
            return await _repository.GetUserByUsernameAndPassword(auth.Name, auth.Password);
        }

    }
}
