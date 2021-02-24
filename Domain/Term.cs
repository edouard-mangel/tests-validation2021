namespace Domain
{
    public struct Term
    {
        public readonly Amount Interest;
        public readonly Amount TotalAmount;
        public readonly Amount AmortizedCapital;
        public readonly Amount RemainingCapital;

        public Term(Amount totalAmount, Amount interest, Amount amortizedCapital, Amount remainingCapital)
        {
            this.Interest = interest;
            this.TotalAmount = totalAmount;
            this.AmortizedCapital = amortizedCapital;
            this.RemainingCapital = remainingCapital;
        }
    }
}