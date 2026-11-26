using System.Text.Json.Serialization;

namespace WeatherAppProject.Models;

public class HourlyWeather
{
    [JsonPropertyName("time")]
    public List<string>? Time { get; set; }

    [JsonPropertyName("temperature_2m")]
    public List<double>? Temperature { get; set; }

    [JsonPropertyName("weather_code")]
    public List<int>? WeatherCode { get; set; }

    [JsonPropertyName("precipitation_probability")]
    public List<int>? PrecipitationProbability { get; set; }
}