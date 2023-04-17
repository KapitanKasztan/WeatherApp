using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace weatherapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class login_page : ContentPage
    {
        public static string API_KEYreal { set; get; }
        public login_page()
        {
            InitializeComponent();
        }

        private async void sendapiAsync(object sender, EventArgs e)
        {

            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                error_label.IsVisible = true;
                error_label.Text = "API key not valid";
            }
            else
            {
                API_KEYreal = api_entry.Text.ToString();
                string search_uri = $"http://api.weatherapi.com/v1/search.json?key={api_entry.Text}&q=London";
                using (var client = new HttpClient())
                {
                    try
                    {
                        var result = await client.GetStringAsync(search_uri);
                        error_label.IsVisible = false;
                        await Navigation.PushAsync(new MainPage());
                    }
                    catch (System.Net.Http.HttpRequestException)
                    {
                        error_label.IsVisible = true;
                        error_label.Text = "API key not valid";
                    }



                }
            }

            


        }

 
    }
}