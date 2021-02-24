using System;
using System.Collections.Generic;

namespace Domain
{
    public class Mortgage 
    {
        public int Id { get; set; }

        internal Mortgage(decimal amount, int duration, decimal rate)
        {
            this.BorrowedAmount = amount;
            this.Duration = duration;
            this.InterestRateInPercent = rate;
            this.Terms = new AmortizationTable(new List<Term>());
        }

        public Amount ComputeMensuality()
        {
            return this.BorrowedAmount * this.PeriodicRate /
                (1 - (decimal)Math.Pow(1 + (double)this.PeriodicRate, -this.Duration));
        }

        private decimal PeriodicRate => this.InterestRateInPercent / 100 / 12;

        public void GenerateAmortizationTable()
        {
            decimal remainingCapital = GenerateTerms();
            GenerateLastTerm(remainingCapital);
        }

        private decimal GenerateTerms()
        {
            decimal mensuality = this.ComputeMensuality();
            decimal remainingCapital = BorrowedAmount;
            for (int i = 0; i < this.Duration - 1; i++)
            {
                Amount interest = remainingCapital * PeriodicRate;
                Amount amortizedCapital = mensuality - interest;
                remainingCapital -= amortizedCapital;
                this.Terms.AddTerm(new Term(mensuality, interest, amortizedCapital, remainingCapital));
            };
            return remainingCapital;
        }

        private void GenerateLastTerm(decimal remainingCapital)
        {
            decimal interestLastPeriod = remainingCapital * PeriodicRate;
            this.Terms.AddTerm(new Term(remainingCapital + interestLastPeriod, interestLastPeriod, remainingCapital, remainingCapital));
        }

        public void SetDuration(int duration)
        {
            Duration = duration;
        }

        public Amount BorrowedAmount { get; }
        public int Duration { get; private set; }
        public AmortizationTable Terms { get; private set; } 
        public decimal InterestRateInPercent { get; private set; }

    }
}