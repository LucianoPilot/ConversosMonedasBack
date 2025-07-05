namespace ConversorMonedas.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public int? SubscriptionId { get; set; }   // FK
        public Subscription? Subscription { get; set; } = null!;
        public string Rol { get; set; } = "user";

        public int ConversionCount { get; set; } = 0;
    }

}
