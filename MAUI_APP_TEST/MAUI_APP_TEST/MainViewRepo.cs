using MAUI_APP_TEST.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_APP_TEST
{
    public class MainViewRepo
    {
        public static List<MainViewModel> MainItems = new()
        {
        new("seppe", 122),
        new("yeet", 122),
        new("woaw", 122),
        };
    }
}
