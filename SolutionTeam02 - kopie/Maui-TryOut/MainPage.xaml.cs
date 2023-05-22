using ClassLibTeam02.Business.Entities;
using Dropbox.Api;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Maui.Converters;

namespace Maui_TryOut
{
    public partial class MainPage : ContentPage
    {
        RestService rest = new();
        ObservableCollection<House> houses;
        MediaElement med;
        MediaSourceConverter converter = new MediaSourceConverter();
        House currentHouse;
        public MainPage()
        {
            InitializeComponent();
            med = MainMed;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            houses = rest.GetHouses();
            LstHouses.ItemsSource = houses;
        }
        private void PlaySound(string audiopath)
        {
            med.Source = (MediaSource)converter.ConvertFromString($"embed://{audiopath}");
            med.Play();
        }

        private void LstHouses_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (currentHouse == e.Item && med.CurrentState == CommunityToolkit.Maui.Core.Primitives.MediaElementState.Playing)
                med.Pause();
            else if (currentHouse == e.Item && med.CurrentState == CommunityToolkit.Maui.Core.Primitives.MediaElementState.Paused)
                med.Play();
            else
            {
                currentHouse = (House)e.Item;
                PlaySound(currentHouse.AudioFilePath);
            }
        }

        private void MainMed_MediaFailed(object sender, CommunityToolkit.Maui.Core.Primitives.MediaFailedEventArgs e)
        {
            _ = 5;
        }
    }
}