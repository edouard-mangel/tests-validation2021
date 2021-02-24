namespace Domain
{
    public class MortgageGenerator
    {
        public IMortgageRepository mortgageRepository;

        public MortgageGenerator(IMortgageRepository mortgageRepository)
        {
            this.mortgageRepository = mortgageRepository;
        }

        public Mortgage GenerateMortgage(Amount borrowedAmount, int durationMonths, decimal interestsPercent)
        {
            Mortgage newMortgage = new Mortgage(borrowedAmount, durationMonths, interestsPercent);
            newMortgage.GenerateAmortizationTable();
            
            this.mortgageRepository.Save(newMortgage);

            return newMortgage;
        }
    }
}
