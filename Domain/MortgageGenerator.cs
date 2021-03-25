using System.Threading.Tasks;

namespace Domain
{
    public class MortgageGenerator
    {
        public IMortgageRepository mortgageRepository;

        public MortgageGenerator(IMortgageRepository mortgageRepository)
        {
            this.mortgageRepository = mortgageRepository;
        }

        public async Task<Mortgage> GenerateMortgage(Amount borrowedAmount, int durationMonths, decimal interestsPercent)
        {
            Mortgage newMortgage = new Mortgage(borrowedAmount, durationMonths, interestsPercent);
            newMortgage.GenerateAmortizationTable();
            
            await this.mortgageRepository.Save(newMortgage);

            return newMortgage;
        }
    }
}
