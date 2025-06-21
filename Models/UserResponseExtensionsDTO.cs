using ConversorMonedas.Data.Entities;
using ConversorMonedas.DTOs;

namespace ConversorMonedas.Models
{
    public static class UserResponseExtensionsDTO
    {
        public static UserResponseDto ToDto(this User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Rol,
                SubscriptionName = user.Subscription?.Type
            };
        }
    }
}
