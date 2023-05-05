using ClassLibTeam02.Business.Entities;
using Dropbox.Api;
using System.Collections.ObjectModel;

namespace Maui_TryOut
{
    public partial class MainPage : ContentPage
    {
        RestService rest = new();
        ObservableCollection<House> houses;
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            houses = rest.GetHouses();
            LstHouses.ItemsSource = houses;
        }
        private void ChangePhoto()
        {

        }

    }
}