using Microsoft.AspNetCore.Mvc;

namespace DataProtectionOnServer.Controllers
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
        private readonly IHostEnvironment _hostEnvironment;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {

            string secretKey = "This sentence is very important and secret";
            DataProtector dataProtector = new DataProtector(_hostEnvironment.ContentRootPath);

            var length = dataProtector.EncryptData(secretKey);

            var decrypted = dataProtector.DecryptData(length);


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                Value = decrypted
            })
            .ToArray();
        }


    }
}