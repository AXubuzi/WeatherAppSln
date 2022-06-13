using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
             await GetWeathersData();
           // BindingContext = new WeatherDataModel();
        }

        private async Task GetWeathersData()
        {
            var data = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (data != PermissionStatus.Granted)
            {
                var newdata = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }
            var location = await Geolocation.GetLocationAsync();
            var latitude = location.Latitude;
            var longitude = location.Longitude;
            //location.Latitude =

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&units=metric&appid=b1c85832a158bf3634a2c3334c93f6a4";
            var response = await client.GetStringAsync(url);

            var weathersData = JsonConvert.DeserializeObject<WeatherData>(response);

            BindingContext = weathersData;
        }
    }
}








