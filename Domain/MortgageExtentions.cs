namespace Domain
{
    public static class MortgageExtentions
    {
        public static Mortgage WithDuration(this Mortgage mortgage, int duration)
        {
            return new Mortgage(mortgage.BorrowedAmount, duration, mortgage.InterestRateInPercent);
        }

        public static Mortgage WithAmount(this Mortgage mortgage, Amount amount)
        {
            return new Mortgage(amount, mortgage.Duration, mortgage.InterestRateInPercent);
        }

        public static Mortgage SetInterestRate(this Mortgage mortgage, decimal rate)
        {
            return new Mortgage(mortgage.BorrowedAmount, mortgage.Duration, rate);
        }
    }
}
