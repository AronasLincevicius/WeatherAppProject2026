
using System.Collections.ObjectModel;
using System.Diagnostics;
using WeatherAppProject.Models;
using WeatherAppProject.Services;

namespace WeatherAppProject;

public partial class MainPage : ContentPage
{

    //creating instances of objects
    private readonly WeatherService _weatherService;
    private readonly HourlyForecastService _hourlyForecastService = new HourlyForecastService();

    // observableCollection allows me to dynamically add or remove hourly forecast
    // objects and automatically notify the ui without manually mapping each
    // forecast variable to individual labels
    public ObservableCollection<HourlyWeatherForecast> HourlyForecast { get; set; } = new();

    public MainPage()
    {
        InitializeComponent();

        //creating instance of weatherservice and make an instance of httpclient
        //im doing it this way so that i dont make a new instance of httpclient everytime i call getweatherasync()
        _weatherService = new WeatherService(new HttpClient());

        BindingContext = this;
    }

    //event handler for button
    private async void CountyEnteredReceived(object sender, EventArgs e)
    {
        //getting county from picker based on user input
        string? county = CountyPicker.SelectedItem?.ToString();

        //local variables to pass into api request
        double latitude = 0;
        double longitude = 0;

        //checking if county is valid and stops it from trying to index for a county if it hasnt been entered
        if (county != null && CountyData.Counties.TryGetValue(county, out var coordinates))
        {
            //getting coordinates from countydata.cs
            latitude = coordinates.Latitude;
            longitude = coordinates.Longitude;
        }
        else
        {
            //debug line to tell me if theres an error (almost never going to happen because its a picker with predetermined values)
            Debug.WriteLine("county not found in dictionary!");
            return;
        }

        //more debugging because i like just having it there to know whats going on in the backend
        Debug.WriteLine($"selected county: {county}");
        Debug.WriteLine($"latitude: {latitude}");
        Debug.WriteLine($"longitude: {longitude}");

        // retrieve current and hourly weather from the API
        WeatherData? weather = await _weatherService.GetWeatherAsync(latitude, longitude);

        //if api didnt return anything then exist method
        if (weather == null) { 
            Debug.WriteLine("api returned null");
            return;
        }

        // process the hourly forecast
        var forecast = _hourlyForecastService.GetHourlyForecast(weather);

        // clear previous county's forecast
        HourlyForecast.Clear();

        // add the new hourly forecast
        foreach (var hour in forecast)
        {
            HourlyForecast.Add(hour);
        }

        //debugging 
        Debug.WriteLine($"Hourly forecast entries: {HourlyForecast.Count}");

        // Check current weather data
        if (weather.Current == null)
            return;

        //more debugging 
        Debug.WriteLine($"Weather code: {weather.Current.WeatherCode}");
        Debug.WriteLine($"Cloud coverage: {weather.Current.CloudCoverage}%");
        Debug.WriteLine($"Temperature: {weather.Current.Temperature}°C");

        //local variable to hold wmo code retreived fom api under the variable WeatherCode
        int weatherCode = weather.Current.WeatherCode;

        //indexing weatherconditions in WeatherCondition.cs and changing the corresponding label to the condition provided
        if (WeatherCondition.WeatherCodes.TryGetValue(weatherCode, out string? weatherCondition))
        {
            weatherConditionLbl.Text = weatherCondition;
        }
        else
        {
            weatherConditionLbl.Text = "Unknown weather condition";
        }

        // Extract current weather values
        double temperature = weather.Current.Temperature;
        int humidity = weather.Current.Humidity;
        double windSpeed = weather.Current.WindSpeed;
        double uvIndex = weather.Current.UvIndex;

        // Update current weather labels
        temperatureLbl.Text = temperature.ToString();
        locationLbl.Text = county;

        Debug.WriteLine("Hourly forecast and current weather updated");
    }
}
