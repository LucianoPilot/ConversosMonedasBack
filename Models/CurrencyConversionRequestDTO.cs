namespace ConversorMonedas.Models
{
    public class CurrencyConversionRequestDto
    {
        public string FromCode { get; set; } = string.Empty;
        public string ToCode { get; set; } = string.Empty;
        public decimal Amount { get; set; }

        public int UserId { get; set; }
    }
}
