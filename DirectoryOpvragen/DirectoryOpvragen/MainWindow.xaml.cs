using Microsoft.Win32;
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

namespace DirectoryOpvragen
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

        private void BtnControleer_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(TxtFolder.Text))
            {
                foreach (string dir in Directory.GetDirectories(TxtFolder.Text))
                    LbxMappen.Items.Add(dir);
                foreach (string dir in Directory.GetFiles(TxtFolder.Text))
                    LbxFiles.Items.Add(dir);
            }
            else
                MessageBox.Show("Folder does not exsist");
        }

        private void LbxMappen_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Directorythings dir = new Directorythings(LbxFiles.SelectedValue.ToString());
            dir.ShowDialog();
        }

        private void BtnOpenDialog_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            //if (ofd.ShowDialog() == true)
            //{
            //    ofd.file
            //}
        }
    }
}
