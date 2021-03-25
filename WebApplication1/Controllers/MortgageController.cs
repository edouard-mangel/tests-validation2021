using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MortgageGeneratorWeb.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MortgageController : ControllerBase
    {
        private readonly MortgageGenerator mortgageGenerator;

        private readonly ILogger<MortgageController> _logger;

        public MortgageController(ILogger<MortgageController> logger, MortgageGenerator mortgageGenerator)
        {
            _logger = logger;
            this.mortgageGenerator = mortgageGenerator;
        }

        [HttpPost]
        public async Task<JsonResult> Compute([FromBody] MortgageInfoDTO mortgageInfoDTO)
        {
            _logger.LogInformation($"Received ");
            return new JsonResult((await mortgageGenerator.GenerateMortgage(mortgageInfoDTO.BorrowedAmount,
                                                                            mortgageInfoDTO.DurationMonths,
                                                                            mortgageInfoDTO.RatePercent)).Terms.Select(t => new
                                                                            {
                                                                                Interest = (decimal)t.Interest,
                                                                                TotalAmount = (decimal)t.TotalAmount,
                                                                                AmortizedCapital = (decimal)t.AmortizedCapital,
                                                                                RemainingCapital = (decimal)t.RemainingCapital
                                                                            }));
        }
    }
}
