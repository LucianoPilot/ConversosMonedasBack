namespace ConversorMonedas.Models
{
    public class CurrencyConversionResponseDto
    {
        public string FromCode { get; set; } = string.Empty;
        public string ToCode { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal Result { get; set; }
    }
}
