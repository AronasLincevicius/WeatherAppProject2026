
using System.Text.Json.Serialization;

namespace WeatherAppProject.Models;

public class WeatherData
{
    [JsonPropertyName("current")]
    public CurrentWeather? Current { get; set; }

    [JsonPropertyName("hourly")]
    public HourlyWeather? Hourly { get; set; }
}

public class CurrentWeather
{

    [JsonPropertyName("temperature_2m")]
    public double Temperature { get; set; }

    [JsonPropertyName("apparent_temperature")]
    public double AparentTemperature { get; set; }

    [JsonPropertyName("relative_humidity_2m")]
    public int Humidity { get; set; }

    [JsonPropertyName("weather_code")]
    public int WeatherCode { get; set; }

    [JsonPropertyName("is_day")]
    public int IsDay { get; set; }

    [JsonPropertyName("precipitation")]
    public double Precipitation { get; set; }

    [JsonPropertyName("wind_direction_10m")]
    public int WindDirection { get; set; }

    [JsonPropertyName("wind_speed_10m")]
    public double WindSpeed { get; set; }

    [JsonPropertyName("cloud_cover")]
    public int CloudCoverage { get; set; }

    [JsonPropertyName("visibility")]
    public double VisibilityDistance { get; set; }

    [JsonPropertyName("uv_index")]
    public double UvIndex { get; set; }
}
