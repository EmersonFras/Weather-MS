namespace WeatherDataService.Models
{
    /*
        Model split into to classes to match the JSON response from the weather API.
        The WeatherData class is the main class that contains the current weather data.
        The Current class contains the current temperature and weather code.
        The weather code is used to determine the weather condition.
    */
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
