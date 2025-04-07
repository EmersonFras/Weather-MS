using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherDataService.Data;
using WeatherDataService.Models;

namespace WeatherDataService.Controllers;

[Route("[controller]")]
[ApiController]
public class WeatherDataController : ControllerBase
{
    private readonly WeatherContext _context;

    public WeatherDataController(WeatherContext context)
    {
        _context = context;
    }

    /*
        Endpoint to get current temperature.
        Rarely used, but useful for testing.
        May be called by clients at startup to get the current temperature.
    */
    //GET: /weatherdata
    [HttpGet]
    public async Task<ActionResult<WeatherData>> GetWeatherData()
    {
        try
        {
            var weatherData = await _context.WeatherData.FindAsync(1);

            if (weatherData == null)
            {
                return NotFound();
            }

            return weatherData;
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);

        }
    } 
}
