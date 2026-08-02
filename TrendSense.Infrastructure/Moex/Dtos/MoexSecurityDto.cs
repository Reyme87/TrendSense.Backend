namespace TrendSense.Infrastructure.Moex.Dtos
{
    public class MoexSecurityDto
    {
        public string SecId { get; set; } = null!;
        public string BoardId { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public string Isin { get; set; } = null!;
        public string CurrencyId { get; set; } = null!;
    }
}
