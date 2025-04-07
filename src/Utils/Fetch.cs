using Newtonsoft.Json;
using WeatherDataService.Data;
using WeatherDataService.Models;

namespace WeatherDataService.Utils
{
    public class Fetch
    {
        private readonly WeatherContext _context;

        public Fetch(WeatherContext context)
        {
            _context = context;
        }

        private readonly string URL = "https://api.open-meteo.com/v1/forecast?latitude=40&longitude=-83&current=temperature_2m&current=weather_code&temperature_unit=fahrenheit";
        
        /*
         * Fetches weather data from the Open Meteo API.
         * The data is stored in the SQLite database.
         * The data is fetched using the Fetch class.
         * The data is emitted using the Emitter class.
         */
        public async Task FetchWeatherData()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(URL);

                // Retry on fail
                while (!response.IsSuccessStatusCode)
                {
                    response = await client.GetAsync(URL);

                    // Retry every 5 minutes if the response is not successful

                    if (!response.IsSuccessStatusCode) Task.Delay(TimeSpan.FromMinutes(5)).Wait();
                }

                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response into the WeatherData object
                WeatherData? weatherData = JsonConvert.DeserializeObject<WeatherData>(responseBody);
                

                if (weatherData != null)
                {
                    // Only one record in the database, so we can use Id = 1
                    weatherData.Id = 1;
                    var existingData = await _context.WeatherData.FindAsync(1);

                    if ( existingData != null )
                    {
                        existingData.current.temperature_2m = weatherData.current.temperature_2m;
                        existingData.current.weather_code = weatherData.current.weather_code;
                        existingData.LastUpdated = DateTime.Now;

                        _context.WeatherData.Update(existingData);
                    } else
                    {
                        await _context.WeatherData.AddAsync(weatherData);
                    }

                    await _context.SaveChangesAsync();
                }

            }
        }
        
    }
}

