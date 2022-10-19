using Microsoft.AspNetCore.Mvc;
using Blazorise.Shared;
using System.Text.Json;

namespace Blazorise.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //MUST be static to persist
        private static List<WeatherForecast> Forecasts { get; set; } = new List<WeatherForecast>();
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;

            if(Forecasts.Count == 0)
            {
                Forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToList();
            }
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Forecasts;
        }

        [HttpPut]
        public void Put(Tuple<Int32, WeatherForecast> putStuff)
        {
            Forecasts.RemoveAt(putStuff.Item1);
            Forecasts.Insert(putStuff.Item1, putStuff.Item2);
        }

        [HttpPost]
        public void Post(WeatherForecast forecast)
        {
            Forecasts.Add(forecast);
        }

        [HttpDelete]
        public void Delete(String item)
        {
            Forecasts.RemoveAt(Convert.ToInt32(item));
        }
    }
}