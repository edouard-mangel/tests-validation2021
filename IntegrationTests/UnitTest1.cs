using MortgageGeneratorWeb.Controllers;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Xunit;

namespace IntegrationTests
{
    public class IntegrationTestsClass1
    {
        [Fact]
        public async System.Threading.Tasks.Task Test1Async()
        {
            var client = new HttpClient();
            MortgageInfoDTO dto = new MortgageInfoDTO() { BorrowedAmount = 100000, DurationMonths = 120, RatePercent = 1.2m };
            var stringContent = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:5001/mortgage/compute", stringContent);
            
            Assert.Equal((int)200, (int)response.StatusCode);
        }
    }

    public static class HttpExtensions
    {


    }
}
