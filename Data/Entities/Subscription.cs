namespace ConversorMonedas.Data.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int MaxConversions { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
