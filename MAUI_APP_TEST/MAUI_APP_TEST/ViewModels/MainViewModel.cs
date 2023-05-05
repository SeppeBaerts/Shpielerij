using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAUI_APP_TEST.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public MainViewModel(string text, int price) 
    { 
        Items = new ObservableCollection<string>();
        Text = text;
        Price = price;
    }
    ObservableCollection<string> items;
    public ObservableCollection<string> Items 
    {
        get => items;
        set
        {
            OnPropertyChanged(nameof(Items));
        }
    }
    string text;
    public string Text
    {
        get => text;
        set
        {
            text = value;
            OnPropertyChanged(nameof(Text));
        }
    }
    int price;
    public int Price
    {
        get => price;
        set
        {
            price = value;
            OnPropertyChanged(nameof(price));
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public void Add()
    {
        if (string.IsNullOrWhiteSpace(Text))
            return;
        items.Add(Text);
        OnPropertyChanged(nameof(Items));
    }
}
