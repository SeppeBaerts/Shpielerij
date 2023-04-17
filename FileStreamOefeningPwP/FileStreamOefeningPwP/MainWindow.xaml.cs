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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileStreamOefeningPwP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string bestandsNaam = "Namen.txt";
        const char separator = '|';
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLeesBestand_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(bestandsNaam))
            {
                LsbVoornamen.Items.Clear();
                LsbAchternamen.Items.Clear();
                string[] volledigeNaam;
                using (StreamReader sr = new StreamReader(bestandsNaam))
                {
                    while (!sr.EndOfStream)
                    {
                        volledigeNaam = sr.ReadLine().Split(separator);
                        LsbVoornamen.Items.Add(volledigeNaam[0]);
                        LsbAchternamen.Items.Add(volledigeNaam[1]);
                    }
                }
            }
            else
                MessageBox.Show("SoMeTHinG WeNt WroNg", "WoOpS");

        }

        private void BtnOpslaanBestand_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("Namen.txt"))
            {
                for(int i = 0; i < LsbAchternamen.Items.Count; i++)
                    sw.WriteLine($"{LsbVoornamen.Items[i]}{separator}{LsbAchternamen.Items[i]}");
            }
        }

        private void BtnVoegToe_Click(object sender, RoutedEventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(TxtVoornaam.Text) || string.IsNullOrWhiteSpace(TxtAchternaam.Text)))
            {
                LsbVoornamen.Items.Add(TxtVoornaam.Text);
                LsbAchternamen.Items.Add(TxtAchternaam.Text);
                TxtVoornaam.Clear();
                TxtAchternaam.Clear();
                TxtVoornaam.Focus();
            }
            else
                MessageBox.Show("Gelieven een geldig voor-en achternaam in te geven");
        }
    }
}
