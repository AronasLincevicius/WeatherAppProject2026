using WeatherAppProject.Models;

namespace WeatherAppProject.Services;

public class HourlyForecastService
{
    public List<HourlyWeatherForecast> GetHourlyForecast(WeatherData weather)
    {
        //creating a list to store the hourly forecast received by the api
        var forecast = new List<HourlyWeatherForecast>();


        var hourly = weather.Hourly;

        if (hourly?.Time == null ||
            hourly.Temperature == null ||
            hourly.WeatherCode == null ||
            hourly.PrecipitationProbability == null)
        {
            return forecast;
        }

        int count = new[]
        {
            hourly.Time.Count,
            hourly.Temperature.Count,
            hourly.WeatherCode.Count,
            hourly.PrecipitationProbability.Count
        }.Min();

        DateTime currentHour = DateTime.Now.Date.AddHours(DateTime.Now.Hour);

        int startIndex = hourly.Time.FindIndex(
            time => DateTime.Parse(time) >= currentHour
        );

        if (startIndex == -1)
            return forecast;

        for (int i = startIndex; i < Math.Min(startIndex + 24, count); i++)
        {
            DateTime time = DateTime.Parse(hourly.Time[i]);

            forecast.Add(new HourlyWeatherForecast
            {
                Time = i == startIndex ? "Now" : time.ToString("h tt"),
                Temperature = $"{Math.Round(hourly.Temperature[i])}°",
                Icon = GetWeatherIcon(hourly.WeatherCode[i]),
                Precipitation = $"{hourly.PrecipitationProbability[i]}%"
            });
        }

        return forecast;
    }
    //returns an emoji for whatever wmo code reveived by the api
    private string GetWeatherIcon(int code)
    {
        return code switch
        {
            0 => "☀️",
            1 => "🌤️",
            2 => "⛅",
            3 => "☁️",
            45 or 48 => "🌫️",
            >= 51 and <= 67 => "🌧️",
            >= 71 and <= 77 => "❄️",
            >= 80 and <= 82 => "🌦️",
            >= 85 and <= 86 => "🌨️",
            >= 95 => "⛈️",
            _ => "🌡️"
        };
    }
}