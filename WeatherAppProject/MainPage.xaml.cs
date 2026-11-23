using WeatherAppProject.Services;
using WeatherAppProject.Models;

namespace WeatherAppProject;

public partial class MainPage : ContentPage
{
    private WeatherService _weatherService = new WeatherService();

    public MainPage()
    {
        InitializeComponent();
    }

    private async void WeatherQueryCompleted(object sender, EventArgs e)
    {
        string targetLocation = WeatherQuery.Text;

        WeatherData? weather = await _weatherService.GetWeather(targetLocation);


    }
}