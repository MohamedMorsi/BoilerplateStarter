using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        //using Nlog
        private readonly ILoggerManager _nlog;
        //using defult logger
        private readonly ILogger<WeatherForecastController> _logger;

        private IRepositoryWrapper _repository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ILoggerManager nlog,
            IRepositoryWrapper repository)
        {
            _logger = logger;
            _nlog = nlog;
            _repository = repository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
           //logging
            _nlog.LogInfo("Here is info message from the controller.");
            _nlog.LogDebug("Here is debug message from the controller.");
            _nlog.LogWarn("Here is warn message from the controller.");
            _nlog.LogError("Here is error message from the controller.");


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet(Name = "GetAccounts")]
        public IEnumerable<string> GetAccounts()
        {
            var domesticAccounts = _repository.Account.FindByCondition(x => x.AccountType.Equals("Domestic"));
            var owners = _repository.Owner.FindAll();
            return new string[] { "value1", "value2" };
        }
    }
}