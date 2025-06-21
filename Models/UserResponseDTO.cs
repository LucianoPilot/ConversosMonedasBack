namespace ConversorMonedas.DTOs
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "user";
        public string? SubscriptionName { get; set; }
    }
}
