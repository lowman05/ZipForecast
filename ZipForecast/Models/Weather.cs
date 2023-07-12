namespace ZipForecast.Models
{
    public class Weather
    {
        public string Description { get; set; }
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }

        public string IconURL { get; set; }

       public string ErrorMessage { get; set; }
        public bool HasError { get; set; }
        public string Name { get; set; }
    }
}
