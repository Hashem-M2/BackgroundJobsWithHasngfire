using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundJobsWithHasngfire.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            // excute at the same time 

            // BackgroundJob.Enqueue(() => SendMeassage("Hashem Talk"));

            // excute after specific time 

            //Console.WriteLine(DateTime.Now);
            //BackgroundJob.Schedule(() => SendMeassage("Hashem Talk"), TimeSpan.FromMinutes(1));

            //excute frecuently 

            RecurringJob.AddOrUpdate("HashemTalk", () => SendMeassage("Hashem Talk"), Cron.Minutely);

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {

                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void SendMeassage (string Email)
        {
            Console.WriteLine($"Email Sent {DateTime.Now}");
        }
    }
}
