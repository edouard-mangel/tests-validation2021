using Domain;
using Microsoft.AspNetCore.Mvc;

namespace MortgageGeneratorWeb.Controllers
{
    internal static class MortgageExtensions
    {
        public static JsonResult ToJson(this Mortgage m)
        {
            return new JsonResult(m);
        }
    }
}
