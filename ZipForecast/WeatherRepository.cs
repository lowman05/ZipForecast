using Newtonsoft.Json.Linq;
using ZipForecast.Models;

namespace ZipForecast
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly IConfiguration _configuration;
        

        public WeatherRepository(IConfiguration configuration)
        {
            _configuration = configuration;
          
        }

        public Weather GetWeather(string zip)
        {
            //var apiKeyObj = File.ReadAllText("appsettings.json");
            //var apiKey = JObject.Parse(apiKeyObj).GetValue("apiKey").ToString();
            //Console.Write("Enter Your Zip Code:");
            try
            {
                var apiKey = _configuration["apiKey"];

                string weatherURL = $"https://api.openweathermap.org/data/2.5/weather?zip={zip}&appid={apiKey}&units=imperial";
                var client = new HttpClient();
                var weatherResponse = client.GetStringAsync(weatherURL).Result;
                var weatherData = JObject.Parse(weatherResponse);

                var name = weatherData["name"].ToString();

                var description = weatherData["weather"][0]["description"];

                var icon = weatherData["weather"][0]["icon"].ToString();

                var iconUrl = $"http://openweathermap.org/img/w/{icon}.png";

                var temp = (double)weatherData["main"]["temp"];
                var roundedTemp = Math.Round(temp);

                var feelsLike = (double)weatherData["main"]["feels_like"];
                var roundedFeelsLike = Math.Round(feelsLike);

                var high = weatherData["main"]["temp_max"];
                var roundedHigh = Math.Round((double)high);

                var low = weatherData["main"]["temp_min"];
                var roundedLow = Math.Round((double)low);

                var sunriseUnixTime = (long)weatherData["sys"]["sunrise"];
                DateTimeOffset sunriseDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(sunriseUnixTime);
                DateTime sunriseTime = sunriseDateTimeOffset.LocalDateTime;
                string sunrise = sunriseTime.ToString("h:mm tt");

                var sunsetUnixTime = (long)weatherData["sys"]["sunset"];
                DateTimeOffset sunsetDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(sunsetUnixTime);
                DateTime sunsetTime = sunsetDateTimeOffset.LocalDateTime;
                string sunset = sunsetTime.ToString("h:mm tt");

                var weatherViewModel = new Weather
                {
                    Description = description.ToString(),
                    Temperature = roundedTemp,
                    FeelsLike = roundedFeelsLike,
                    High = roundedHigh,
                    Low = roundedLow,
                    Sunrise = sunrise,
                    Sunset = sunset,
                    IconURL = iconUrl,
                    Name = name.ToString(),
                };

                return weatherViewModel;
            }
            catch (Exception ex)
            {
                return new Weather
                {
                    ErrorMessage = "Invalid Entry: " /*+ ex.Message*/,
                    HasError = true
                };
                
            }
        }
    }
}
