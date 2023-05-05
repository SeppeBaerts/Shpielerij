using MAUI_APP_TEST.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Platform;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace MAUI_APP_TEST;

public partial class MainPage : ContentPage
{
    ObservableCollection<MainViewModel> obsList = new()
        {
        new("seppe", 122),
        new("yeet", 123),
        new("woaw", 124),
        };

    public MainPage()
	{
        InitializeComponent();
        MainList.ItemsSource = obsList;
    }

    private void MyButton_Clicked(object sender, EventArgs e)
    {
        obsList.Add(new MainViewModel(EnTask.Text, 125));
        EnTask.Text = string.Empty;
    }

    private void SwipeItem_Clicked(object sender, EventArgs e)
    {
        MainViewModel viewModel = ((SwipeItem)sender).BindingContext as MainViewModel;
        MainList.BatchBegin();
        obsList.Remove(viewModel);
        MainList.BatchCommit();

    }

    private void SwipeGestureRecognizer_Swiped_1(object sender, SwipedEventArgs e)
    {
        //voor het tinder-soort van app
        if (e.Direction == SwipeDirection.Right)
            DisplayAlert("swiped", "right", "toppie");
        else if (e.Direction == SwipeDirection.Left)
            DisplayAlert("swiped", "left", "yeet");
    }
}

