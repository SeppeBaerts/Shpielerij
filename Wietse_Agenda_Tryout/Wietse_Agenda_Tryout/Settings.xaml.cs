using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wietse_Agenda_Tryout
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        StreamReader sr;
        public Settings()
        {
            InitializeComponent();

        }
        //bool = ::SettingName;SettingTrueOrFalse
        //string = --SettingName;SettingStringValue
        //int = ,,SettingName,SettingIntValue --> nog Parsen
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] settings;
            using(sr = new StreamReader("../../SettingsFile.txt"))
            {
                while (!sr.EndOfStream)
                {
                    settings = sr.ReadLine().Split(';');
                    if (settings[0].StartsWith("::"))
                    {
                        CreateSetting(settings[0].TrimStart(':'), settings[1].ToLower() == "true");
                    }
                    else if (settings[0].StartsWith("--"))
                    {
                        CreateSetting(settings[0].TrimStart('-'), settings[1]);
                    }
                    else if (settings[0].StartsWith(",,"))
                    {
                        CreateSetting(settings[0].TrimStart(','), int.Parse(settings[1]));
                    }
                }
            }
        }
        private void CreateSetting(string name, string value)
        {
            WrapPanel wrap = new WrapPanel();
            Label lbl = new Label
            {
                Content = name,
                Margin = new Thickness(10, 0, 0, 0),
            };
            wrap.Children.Add(lbl);
            TextBox txt = new TextBox
            {
                Text = value,
                MinWidth = 25,
            };
            wrap.Children.Add(txt);
            StackSettings.Children.Add(wrap);
        }
        private void CreateSetting(string name, bool value)
        {
            WrapPanel wrap = new WrapPanel();
            Label lbl = new Label
            {
                Content = name,
                Margin = new Thickness(10, 0, 0, 0),
            };
            wrap.Children.Add(lbl);
            CheckBox check = new CheckBox
            {
                IsChecked = value,
                VerticalAlignment = VerticalAlignment.Center,
            };
            wrap.Children.Add(check);
            StackSettings.Children.Add(wrap);
        }
        private void CreateSetting(string name, int value) 
        {
        
        }
        //private void CreateSettingsPair()
        //{
        //    SettingStatic.settingsPair.Add("Opsomming karakter", "-");
        //    SettingStatic.settingsPair.Add("::Show content on startup", "True");
        //    //extra kleuren:

        //}
    }
}
