using SQLitePCL;
using WeatherDataService.Data;
using WeatherDataService.Utils;

namespace WeatherDataService.Services
{
    public class WeatherFetcher : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Emitter _emitter;

        public WeatherFetcher(IServiceScopeFactory scopeFactory, Emitter emitter)
        {
            _scopeFactory = scopeFactory;
            _emitter = emitter;
        }

        /*
            Background service to fetch weather data from the API every 10 minutes.
            The data is stored in the SQLite database.
            The data is fetched using the Fetch class.
            The data is emitted using the Emitter class.
        */
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var fetch = scope.ServiceProvider.GetRequiredService<Fetch>();
                    var context = scope.ServiceProvider.GetRequiredService<WeatherContext>();

                    // Fetch weather data
                    await fetch.FetchWeatherData();
                    var weatherData = await context.WeatherData.FindAsync(1);
                    if (weatherData != null && weatherData.current != null) 
                    {
                        string weatherCode = weatherData.current.weather_code.ToString();
                        string temperature = weatherData.current.temperature_2m.ToString();

                        // Seperate topics for temperature and weather code
                        // Emit the data to the event bus
                        await _emitter.EmitAsync(weatherCode, "update.weathercode");
                        await _emitter.EmitAsync(temperature, "update.temperature");

                        Console.WriteLine($"Updates, Temperature: {temperature}, Weather Code: {weatherCode}");
                    }

                }

                // Wait for 10 minutes before next fetch
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }

        }
    }
}
