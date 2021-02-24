using Domain;
using Infrastructure;
using System;
using System.Linq;
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
        public void ShouldSaveGeneratedMortgages()
        {
            // Act
            Mortgage mortgage = DefaultMortgage();

            // Assert
            Assert.Equal(mortgage, mortgageRepository.ById(mortgage.Id));
        }

        [Fact]
        public void ShouldRefundTotalAmount()
        {
            Amount borrowedAmount = 10000;
            int duration = 120;
            decimal interestsRate = 1m;

            // Act
            Mortgage mortgage = GenerateMortgage(borrowedAmount, duration, interestsRate);
            

            // Assert
            Amount totalRefundedAmount = mortgage.Terms.Sum(t => t.AmortizedCapital);
            Assert.Equal((decimal)borrowedAmount, (decimal)totalRefundedAmount);
        }

        private Mortgage GenerateMortgage(Amount borrowedAmount, int duration, decimal interestsRate)
        {
            return generator.GenerateMortgage(borrowedAmount, duration, interestsRate);
        }

        private Mortgage DefaultMortgage()
        {
            return GenerateMortgage(10000, 120, 1m);
        }

        [Fact]
        public void LastTermShouldRefundAllCapital()
        {
            Amount borrowedAmount = 10000;
            // Arrange
            Mortgage mortgage = GenerateMortgage(borrowedAmount, 120, 1m);

            // Act
            mortgage.GenerateAmortizationTable();

            // Assert
            Term lastTerm = mortgage.Terms.Last();
            Term termBeforeLast = mortgage.Terms.ElementAt(mortgage.Terms.Count-2);

            Assert.Equal(termBeforeLast.RemainingCapital, lastTerm.AmortizedCapital);
        }

        [Fact]
        public void ShouldHaveTermsAmountCorrespondingToInterestsPlusAmortizedCapital()
        {
            // Arrange
            Mortgage mortgage = DefaultMortgage();

            // Act
            mortgage.GenerateAmortizationTable();

            // Assert
            Assert.True(mortgage.Terms.Take(mortgage.Terms.Count -1).All(t => t.TotalAmount == t.Interest + t.AmortizedCapital));
        }

        [Fact]
        public void ShouldHaveTermsAmountCorrespondingToMensuality()
        {
            // arrange
            Mortgage mortgage = DefaultMortgage();

            // act
            decimal mensuality = mortgage.ComputeMensuality();

            // Assert
            Assert.True(mortgage.Terms.Take(mortgage.Terms.Count -1).All(t => t.TotalAmount == mensuality));
        }

        [Theory]
        [InlineData(10000, 120, 87.6)]
        [InlineData(10000, 240, 45.99)]
        [InlineData(20000, 120, 175.21)]
        [InlineData(100000, 120, 876.04)]
        public void ShouldHaveMensualityBasedOnInterestsAndDuration(double amount, int duration, double expectedMensuality)
        {
            // Arrange
            Mortgage mortgage = DefaultMortgage()
                .WithAmount(new Amount((decimal)amount))
                .WithDuration(duration);

            // Act 
            decimal mensuality = mortgage.ComputeMensuality();

            // Assert
            Assert.Equal((decimal)expectedMensuality, mensuality);
        }

        [Fact]
        public void ShouldContainAsManyTermsAsDuration()
        {
            // Arrange 
            int duration = 12;
            
            // Act 
            Mortgage mortgage = GenerateMortgage(100000, duration, 1m);

            // Assert 
            Assert.Equal(duration, mortgage.Terms.Count());
        }
                
    }
}
