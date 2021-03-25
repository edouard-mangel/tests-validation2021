using Domain;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class MortgageTest
    {
        public IMortgageRepository mortgageRepository;
        public MortgageGenerator generator;

        public MortgageTest()
        {
            mortgageRepository = new InMemoryMortgageRepository();
            generator = new MortgageGenerator(mortgageRepository);
        }

        [Fact]
        public async Task ShouldSaveGeneratedMortgagesAsync()
        {
            // Act
            Mortgage mortgage = await DefaultMortgageAsync();

            // Assert
            Assert.Equal(mortgage, await mortgageRepository.ById(mortgage.Id));
        }

        [Fact]
        public async Task ShouldRefundTotalAmountAsync()
        {
            Amount borrowedAmount = 10000;
            int duration = 120;
            decimal interestsRate = 1m;

            // Act
            Mortgage mortgage = await GenerateMortgageAsync(borrowedAmount, duration, interestsRate);


            // Assert
            Amount totalRefundedAmount = mortgage.Terms.Sum(t => t.AmortizedCapital);
            Assert.Equal((decimal)borrowedAmount, (decimal)totalRefundedAmount);
        }

        private async Task<Mortgage> GenerateMortgageAsync(Amount borrowedAmount, int duration, decimal interestsRate)
        {
            return await generator.GenerateMortgage(borrowedAmount, duration, interestsRate);
        }

        private async Task<Mortgage> DefaultMortgageAsync()
        {
            return await GenerateMortgageAsync(10000, 120, 1m);
        }

        [Fact]
        public async Task LastTermShouldRefundAllCapitalAsync()
        {
            Amount borrowedAmount = 10000;
            // Arrange
            Mortgage mortgage = await GenerateMortgageAsync(borrowedAmount, 120, 1m);

            // Act
            mortgage.GenerateAmortizationTable();

            // Assert
            Term lastTerm = mortgage.Terms.Last();
            Term termBeforeLast = mortgage.Terms.ElementAt(mortgage.Terms.Count - 2);

            Assert.Equal(termBeforeLast.RemainingCapital, lastTerm.AmortizedCapital);
        }

        [Fact]
        public async Task ShouldHaveTermsAmountCorrespondingToInterestsPlusAmortizedCapitalAsync()
        {
            // Arrange
            Mortgage mortgage = await DefaultMortgageAsync();

            // Act
            mortgage.GenerateAmortizationTable();

            // Assert
            Assert.True(mortgage.Terms.Take(mortgage.Terms.Count - 1).All(t => t.TotalAmount == t.Interest + t.AmortizedCapital));
        }

        [Fact]
        public async Task ShouldHaveTermsAmountCorrespondingToMensualityAsync()
        {
            // arrange
            Mortgage mortgage = await DefaultMortgageAsync();

            // act
            decimal mensuality = mortgage.ComputeMensuality();

            // Assert
            Assert.True(mortgage.Terms.Take(mortgage.Terms.Count - 1).All(t => t.TotalAmount == mensuality));
        }

        [Theory]
        [InlineData(10000, 120, 87.6)]
        [InlineData(10000, 240, 45.99)]
        [InlineData(20000, 120, 175.21)]
        [InlineData(100000, 120, 876.04)]
        public async Task ShouldHaveMensualityBasedOnInterestsAndDurationAsync(double amount, int duration, double expectedMensuality)
        {
            // Arrange
            Mortgage mortgage = (await DefaultMortgageAsync())
                .WithAmount(new Amount((decimal)amount))
                .WithDuration(duration);

            // Act 
            decimal mensuality = mortgage.ComputeMensuality();

            // Assert
            Assert.Equal((decimal)expectedMensuality, mensuality);
        }

        [Fact]
        public async Task ShouldContainAsManyTermsAsDurationAsync()
        {
            // Arrange 
            int duration = 12;

            // Act 
            Mortgage mortgage = await GenerateMortgageAsync(100000, duration, 1m);

            // Assert 
            Assert.Equal(duration, mortgage.Terms.Count());
        }

    }
}
