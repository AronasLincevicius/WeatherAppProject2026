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

        // receiving the weatherdata template object from the api
        WeatherData? weather = await _weatherService.GetWeatherAsync(latitude, longitude);

        // check that the data exists
        if (weather?.Current == null)
            return;

        // extracting individual values and assigning them to the variables
        double temperature = weather.Current.Temperature;
        int humidity = weather.Current.Humidity;
        double windSpeed = weather.Current.WindSpeed;

        // display the valuse to user
        temperatureLbl.Text = temperature.ToString();
    }

}