using ConversorMonedas.Data.Entities;
using ConversorMonedas.DTOs;
using ConversorMonedas.Models;

namespace ConversorMonedas.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateAsync(UserCreateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ChangeSubscription(int userId, int subscriptionId);
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto?> GetByIdAsync(int id);

        Task<User?> ValidateUser(AuthDTO auth);


    }
}
