
using System.Diagnostics;
using WeatherAppProject.Models;
using WeatherAppProject.Services;

namespace WeatherAppProject;

public partial class MainPage : ContentPage
{
    private readonly WeatherService _weatherService;

    public MainPage()
    {
        InitializeComponent();

        //create a weatherservice object to be able to communicate with api and retreive data
        _weatherService = new WeatherService(new HttpClient());
    }


    private async void CountyEnteredReceived(object sender, EventArgs e)
    {

        //making a string variable that is equal to the county the user picks
        string? county = CountyPicker.SelectedItem?.ToString();

        //local longitude and latitude to pass into api get request
        double latitude = 0;
        double longitude = 0;

        //simple check to see if county is null and getting latitude and longitude coordinates from CountyData.cs from dictionary
        if (county != null && CountyData.Counties.TryGetValue(county, out var coordinates))
        {
            latitude = coordinates.Latitude;
            longitude = coordinates.Longitude;
        }
        else
        {
            Debug.WriteLine("County not found in dictionary!");
            return;
        }

        //debugging
        Debug.WriteLine($"Selected county: {county}");
        Debug.WriteLine($"Latitude: {latitude}");
        Debug.WriteLine($"Longitude: {longitude}");

            // receiving the weatherdata template object from the api
            WeatherData? weather = await _weatherService.GetWeatherAsync(latitude, longitude);

            //more debugging
            Debug.WriteLine($"Weather code: {weather?.Current?.WeatherCode}");
            Debug.WriteLine($"Cloud coverage: {weather?.Current?.CloudCoverage}%");
            Debug.WriteLine($"Temperature: {weather?.Current?.Temperature}°C");

            if (weather?.Current == null)
                return;

            int weatherCode = weather.Current.WeatherCode;

            Debug.WriteLine(weatherCode);

            if (WeatherCondition.WeatherCodes.TryGetValue(weatherCode, out string? weatherCondition))
            {
                weatherConditionLbl.Text = weatherCondition;
            }
            else
            {
                weatherConditionLbl.Text = "Unknown weather condition";
            }

            // extracting individual values and assigning them to the variables
            double temperature = weather.Current.Temperature;
            int humidity = weather.Current.Humidity;
            double windSpeed = weather.Current.WindSpeed;
            double uvIndex = weather.Current.UvIndex;

            // display the values to user
            Debug.WriteLine("Starting label updates");

            temperatureLbl.Text = temperature.ToString();
            locationLbl.Text = county;
            humidityLbl.Text = $"{humidity}%";
            windSpeedLbl.Text = windSpeed.ToString();
            uvIndexLbl.Text = uvIndex.ToString();

            Debug.WriteLine("Finished label updates");
        }
    }

