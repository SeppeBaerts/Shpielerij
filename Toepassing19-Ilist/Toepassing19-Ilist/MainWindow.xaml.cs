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

namespace Toepassing19_Ilist
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ListModule mod;
        public MainWindow()
        {
            InitializeComponent();
            mod = new ListModule(LbxBox);
        }

        private void BtnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            mod.Add(TxtModule.Text);
        }

        private void BtnZoeken_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Positie:{mod.IndexOf(TxtModule.Text)}");
        }

        private void BtnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            mod.Remove(TxtModule.Text);
        }
    }
}
