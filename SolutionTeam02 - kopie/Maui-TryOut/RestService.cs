using ClassLibTeam02.Business.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Maui_TryOut
{
    public class RestService
    {
        //MAUI-06
        private static bool IsAndroid() => DeviceInfo.Current.Platform == DevicePlatform.Android;
        string REST_Url = IsAndroid() ?
            "http://10.0.2.2:5148/api/House" :
            "http://localhost:5148/api/House";

        //MAUI-05
        private HttpClient _client = new HttpClient();
        private ObservableCollection<House> HouseItems { get; set; }
        private bool hasLoaded = false;
        public ObservableCollection<House> GetHouses()
        {
            if (!hasLoaded)
            {
                //MAUI-07
                HttpResponseMessage t = _client.GetAsync(REST_Url).Result;
                //MAUI-08
                HouseItems = JsonSerializer.Deserialize<ObservableCollection<House>>(t.Content.ReadAsStream());
                hasLoaded = true;
            }
            return HouseItems;
        }

    }
}
