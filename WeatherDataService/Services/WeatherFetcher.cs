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

                        await _emitter.EmitAsync(weatherCode, "update.weatercode");
                        await _emitter.EmitAsync(temperature, "update.temperature");
                    }

                }

                // Wait for 10 minutes before next fetch
                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
            }

        }
    }
}
