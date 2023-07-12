using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using ZipForecast.Models;

namespace ZipForecast.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherRepository repo;

        public WeatherController(IWeatherRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index(string zip)
        {
            var weather = repo.GetWeather(zip);

            if (weather.HasError)
            {
                ModelState.AddModelError(string.Empty, weather.ErrorMessage);
                return View(weather);
            }

            return View(weather);
        }


        











    }
}
