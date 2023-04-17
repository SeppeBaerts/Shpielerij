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

namespace DirectoryOpvragen
{
    /// <summary>
    /// Interaction logic for Directorythings.xaml
    /// </summary>
    public partial class Directorythings : Window
    {
        public Directorythings(string Path)
        {
            InitializeComponent();
            try
            {
                using (StreamReader stream = new StreamReader(Path))
                    TxtFileTekst.Text = stream.ReadToEnd();
            }
            catch
            {
                MessageBox.Show("File not a text file");
            }


        }
    }
}
