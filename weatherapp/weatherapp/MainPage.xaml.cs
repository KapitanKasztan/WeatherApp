using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace weatherapp
{
    public partial class MainPage : ContentPage
    {
        public string Api_Key { get; set; }
        public string chose_city_name { get; set; }

        public received_Rootobject ItemsSource { set; get; }
        public MainPage()
        {
            InitializeComponent();
            Api_Key = login_page.API_KEYreal;

        }



        private async void search_city_button_ClickedAsync(object sender, EventArgs e)
        {
            result.IsVisible = false;
            string city = search_city.Text;
            string search_uri = $"http://api.weatherapi.com/v1/search.json?key={Api_Key}&q={city}";
            using (var client = new HttpClient())
            {
   
                var result = await client.GetStringAsync(search_uri);


                List<search_template> city_list = JsonConvert.DeserializeObject<List<search_template>>(result);
                if (city_list.Count > 0)
                {
                    searched_city_list.IsVisible = true;
                    searched_city_list.ItemsSource = city_list;

                }

                
                

            }
            
        }

        private async void City_choseAsync(object sender, ItemTappedEventArgs e)
        {
            search_template city_data = (search_template)e.Item;

            result.IsVisible = true;
            chose_city_name = e.ToString();
            searched_city_list.IsVisible=false;
            
            string uri = $"http://api.weatherapi.com/v1/current.json?key={Api_Key}&q={city_data.name}";

            using (var client = new HttpClient())
            {


                var result = await client.GetStringAsync(uri);

                received_Rootobject weather_data = JsonConvert.DeserializeObject<received_Rootobject>(result);

                ItemsSource = weather_data;

                
                name.Text = ItemsSource.location.name;
                if(ItemsSource.location.region != null && ItemsSource.location.region != "")
                {
                    region.IsVisible = true;
                    regionlb.IsVisible = true;
                    region.Text = ItemsSource.location.region;
                    regionlb.Text = "Region:";

                }
                else
                {
                    region.Text = "";
                    regionlb.Text = "";
                    region.IsVisible = false;
                    regionlb.IsVisible = false;

                }
                countrylb.Text = "Country:";
                country.Text = ItemsSource.location.country;
                temp_clb.Text = "Temperature:";
                temp_c.Text = ItemsSource.current.temp_c.ToString();
                pressurelb.Text = "Presure:";
                pressure.Text = ItemsSource.current.pressure_mb.ToString();
                humiditylb.Text = "Humidity:";
                humidity.Text = ItemsSource.current.humidity.ToString();
                windlb.Text = "Wind:";
                wind.Text = ItemsSource.current.wind_kph.ToString();
                textlb.Text = "Weather:";
                text.Text = ItemsSource.current.condition.text;
                weather_icon.Source = "https:" + ItemsSource.current.condition.icon;


            }


        }


    }
}
