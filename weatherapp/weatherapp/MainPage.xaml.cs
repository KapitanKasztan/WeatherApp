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
            Api_Key = "43305e15ed494be7918123207211610";


        }



        private async void search_city_button_ClickedAsync(object sender, EventArgs e)
        {
            
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

        private async void City_choseAsync(object sender, EventArgs e)
        {
            var button = (Button)sender;
            chose_city_name = button.Text;
            searched_city_list.IsVisible=false;
            
            string uri = $"http://api.weatherapi.com/v1/current.json?key={Api_Key}&q={chose_city_name}";

            using (var client = new HttpClient())
            {

                var result = await client.GetStringAsync(uri);

                received_Rootobject weather_data = JsonConvert.DeserializeObject<received_Rootobject>(result);

                ItemsSource = weather_data;

                name.Text = ItemsSource.location.name;
                region.Text = ItemsSource.location.region;
                country.Text = ItemsSource.location.country;
                temp_c.Text = ItemsSource.current.temp_c.ToString();
                text.Text = ItemsSource.current.condition.text;
                Console.WriteLine(ItemsSource.current.condition.text);

            }


        }

    }
}
