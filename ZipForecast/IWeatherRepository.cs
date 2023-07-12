using ZipForecast.Models;

namespace ZipForecast
{
    public interface IWeatherRepository
    {
        public Weather GetWeather(string zip);
    }
}
