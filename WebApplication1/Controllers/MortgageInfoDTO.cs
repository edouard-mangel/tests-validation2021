namespace MortgageGeneratorWeb.Controllers
{
    public class MortgageInfoDTO
    {
        public decimal BorrowedAmount { get; set; }
        public int DurationMonths { get; set; }
        public decimal RatePercent { get; set; }
    }
}