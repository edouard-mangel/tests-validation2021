using Domain;
using NSubstitute;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class MortgageMockingTest
    {

        public IMortgageRepository mortgageRepository;
        public MortgageGenerator generator;

        public MortgageMockingTest()
        {
            this.mortgageRepository = Substitute.For<IMortgageRepository>();
            this.generator = new MortgageGenerator(this.mortgageRepository);
        }


        [Fact]
        public void ShouldSaveGeneratedMortgages()
        {
            // Act
            Mortgage mortgage = this.generator.GenerateMortgage(10000, 120, 1m);

            // Assert     
            Assert.True(this.mortgageRepository.ReceivedCalls().Any());
        }

    }
}
