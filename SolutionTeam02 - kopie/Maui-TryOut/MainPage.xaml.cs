using ClassLibTeam02.Business.Entities;
using Dropbox.Api;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Maui.Converters;

namespace Maui_TryOut
{
    public partial class MainPage : ContentPage
    {
        #region buttonTexts
        private string enlargeImage => "Enlarge image";
        private string closeImage => "Close Image";
            #endregion

        readonly RestService rest = new();
        ObservableCollection<House> houses;
        MediaElement med;
        readonly MediaSourceConverter converter = new MediaSourceConverter();
        readonly ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
        House currentHouse;
        public MainPage()
        {
            InitializeComponent();
            med = MainMed;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Button sdr = (Button)sender;
            if (sdr.Text == "Load Content")
            {
                houses = rest.GetHouses();
                LstHouses.ItemsSource = houses;
                sdr.Text = enlargeImage;
            }
            else if (LstHouses.SelectedItem != null && sdr.Text == enlargeImage)
            {
                BigImage.Source = (ImageSource)imageSourceConverter.ConvertFromString(((House)LstHouses.SelectedItem).ImageFilePath);
                BigImage.ZIndex = 5;
                sdr.Text = closeImage;
            }
            else if (LstHouses.SelectedItem != null && sdr.Text == closeImage)
            {
                BigImage.Source = null;
                BigImage.ZIndex = 0;
                sdr.Text = enlargeImage;
            }
            else
                DisplayAlert("hmmm", "Something went wrong here, we're not sure why \n" +
                    "but we can figure it out! please contact us at pxl@support.com", "Close");
        }
        private void PlaySound(string audiopath)
        {
            //MAUI-10
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
            DisplayAlert("woops", "Looks like something went " +
                "wrong with playing an audio file. please contact pxl@supportteam.com.", 
                "That's fine!");
        }
    }
}