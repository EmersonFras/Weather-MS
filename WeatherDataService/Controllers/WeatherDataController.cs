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

    //GET: /weatherdata
    [HttpGet]
    public async Task<ActionResult<WeatherData>> GetWeatherData()
    {
        try
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return await _context.WeatherData.FindAsync(1);
#pragma warning restore CS8604 // Possible null reference argument.
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error: " + ex.Message);

        }
    } 
}
