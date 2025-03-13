namespace WeatherDataService.Models
{
    public class WeatherData
    {
        public int Id { get; set; } = 1;
        public Current? current { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }

    public class Current
    {
        public double temperature_2m { get; set; }
        public int weather_code { get; set; }
    }

}
