namespace BusinessInsightsAPI.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using BusinessInsightsAPI.Models;

    public class FizzBuzzController : ApiController
    {
        //// GET api/FizzBuzz/5
        public List<string> Get(int id)
        {
            List<string> fizzBuzzSeries = new List<string>();
            fizzBuzzSeries = FizzBuzz.DoFizzBuzzSeries(id);

            return fizzBuzzSeries;
        }
    }
}