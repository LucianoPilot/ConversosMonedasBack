namespace ConversorMonedas.Models
{
    public class UserCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int SubscriptionId { get; set; }
        public string Role { get; set; } = "user";
        public int ConversionCount { get; set; }
    }
}
