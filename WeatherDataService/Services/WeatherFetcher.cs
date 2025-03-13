using SQLitePCL;
using WeatherDataService.Utils;

namespace WeatherDataService.Services
{
    public class WeatherFetcher : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public WeatherFetcher(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var fetch = scope.ServiceProvider.GetRequiredService<Fetch>();

                    // Fetch weather data
                    await fetch.FetchWeatherData();
                }

                // Wait for 10 minutes before next fetch
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }

        }
    }
}
