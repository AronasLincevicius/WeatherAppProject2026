
using System.Text.Json;
using WeatherAppProject.Models;

namespace WeatherAppProject.Services;

public class WeatherService
{
    private readonly HttpClient _client;

    public WeatherService(HttpClient client)
    {
        _client = client;
    }

    public async Task<WeatherData?> GetWeatherAsync(double latitude, double longitude)
    {
        string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,apparent_temperature,relative_humidity_2m,weather_code,is_day,precipitation,wind_direction_10m,wind_speed_10m,cloud_cover,visibility,uv_index";

        HttpResponseMessage response = await _client.GetAsync(url);

        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();

        WeatherData? weatherData = JsonSerializer.Deserialize<WeatherData>(json);

        return weatherData;
    }
}
