using GameEngineLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Still_Dunno_What_This_will_be
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text File(*.txt)|*.txt";
            if ((bool)open.ShowDialog())
            {
                GameWindow game = new GameWindow(open.FileName);
                game.ShowDialog();
            }
        }
        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            //This will open the settings (background color, file save location,...
        }

        private void BtnCreateLevel_Click(object sender, RoutedEventArgs e)
        {
            CreateGameWindow cgw = new CreateGameWindow();
            cgw.Show();
            //This will Create a new level
        }
    }
}
