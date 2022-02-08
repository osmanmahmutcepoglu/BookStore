using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")] 

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


        [HttpGet] 
        public IEnumerable<WeatherForecast> Get() 
        { 
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast 
            {
                Date = DateTime.Now.AddDays(index), 
                TemperatureC = rng.Next(-20, 55), 
                Summary = Summaries[rng.Next(Summaries.Length)] 
            })
            .ToArray(); 
        }
        
        [HttpGet("tempC/{tempC}")] 
        public ActionResult<WeatherForecast> GetByTempC([FromQuery] int tempC)
        {
            return new WeatherForecast
            {
                Date = DateTime.Now.AddDays(tempC),
                Summary = Summaries[2],
                TemperatureC = tempC
            };
        }

        [HttpGet("{id}")] 
        public ActionResult<WeatherForecast> GetById(string id)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray()[0];
        }
    }
}