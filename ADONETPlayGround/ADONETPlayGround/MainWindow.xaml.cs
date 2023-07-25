using System;
using System.Collections.Generic;
using System.Data;
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

namespace ADONETPlayGround
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] levs = new string[]{ "level1" };
        public MainWindow()
        {
            InitializeComponent();
            foreach (string level in levs)
                MnuStates.Items.Add(MakeMenuItem(level));
            DataTables.Dtstudents.Columns.Add("Naam");
            DataTables.Dtstudents.Columns.Add("Voornaam");
            DataTables.Dtstudents.Columns.Add("Richting");
        }
        private MenuItem MakeMenuItem(string header)
        {
            MenuItem mnu = new MenuItem()
            {
                Header = header,
            };
            mnu.Click += Mnu_Click;
            return mnu;
        }

        private void Mnu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mnu = sender as MenuItem;
            switch(mnu.Header)
            {
                case "level1":
                    ExecuteLevel1();
                    break;
                default:
                    break;
            }
        }
        private void ExecuteLevel1()
        {
            Students seppe = new Students("Seppe", "Baerts", "Programmeren");
            var parameters = seppe;
            DataTables.Dtstudents.Rows.Add(parameters.Naam, parameters.Voornaam, parameters.Richting);
            DataTables.Dtstudents.Rows.Add(new Students("Katy", "Geyens", "Programmeren").GetProperties());
            DataTables.Dtstudents.Rows.Add(new Students("Koen", "Baerts", "Programmeren").GetProperties());
            DataTables.Dtstudents.Rows.Add(new Students("Wietse", "Baerts", "Programmeren").GetProperties());
            MainDataGrid.ItemsSource = DataTables.Dtstudents.AsDataView();
        }

        private void MnuShowTextBox_Click(object sender, RoutedEventArgs e)
        {
            TxtFilter.Visibility = TxtFilter.Visibility == Visibility.Visible? Visibility.Collapsed :  Visibility.Visible;
        }

        private void MnuSearchByTextBox_Click(object sender, RoutedEventArgs e)
        {
            var filteren = from row in ((DataView)MainDataGrid.ItemsSource).Table.AsEnumerable()
                         where row["Naam"].ToString().Contains(TxtFilter.Text)
                         select row;
            MainDataGrid.ItemsSource = filteren.AsDataView();


        }
    }
}
