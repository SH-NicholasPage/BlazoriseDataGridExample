using Microsoft.AspNetCore.Mvc;
using SQLiteAndBlazorise.Shared;

namespace SQLiteAndBlazorise.Server.Controllers
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

        [HttpPost]
        public void Post(WeatherForecast forecast)
        {
            Forecasts.Add(forecast);
        }
    }
}