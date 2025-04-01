using Microsoft.EntityFrameworkCore;
using WeatherDataService.Data;
using WeatherDataService.Services;
using WeatherDataService.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<WeatherContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddSingleton<Emitter>();
builder.Services.AddScoped<Fetch>();
builder.Services.AddHostedService<WeatherFetcher>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WeatherContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
