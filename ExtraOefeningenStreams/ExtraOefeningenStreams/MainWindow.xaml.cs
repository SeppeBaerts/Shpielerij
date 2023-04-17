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
using System.Diagnostics;

namespace ExtraOefeningenStreams
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProcessStartInfo oef1 = new ProcessStartInfo(@"C:\Users\12202625\offline documenten\Shpielerij\CSharpShpielerij\ExtraOefeningenStreams\Oefening1\bin\Debug\Oefening1.exe");
        ProcessStartInfo oef2 = new ProcessStartInfo(@"C:\Users\12202625\offline documenten\Shpielerij\CSharpShpielerij\ExtraOefeningenStreams\Oefening2\bin\Debug\Oefening2.exe");
        ProcessStartInfo oef3 = new ProcessStartInfo(@"C:\Users\12202625\offline documenten\Shpielerij\CSharpShpielerij\ExtraOefeningenStreams\Oefening3\bin\Debug\Oefening3.exe");
        ProcessStartInfo oef4 = new ProcessStartInfo(@"C:\Users\12202625\offline documenten\Shpielerij\CSharpShpielerij\ExtraOefeningenStreams\Oefening4\bin\Debug\Oefening4.exe");
        ProcessStartInfo oef5 = new ProcessStartInfo(@"C:\Users\12202625\offline documenten\Shpielerij\CSharpShpielerij\ExtraOefeningenStreams\Oefening5\bin\Debug\Oefening5.exe");
        ProcessStartInfo oef6 = new ProcessStartInfo(@"C:\Users\12202625\offline documenten\Shpielerij\CSharpShpielerij\ExtraOefeningenStreams\Oefening6\bin\Debug\Oefening6.exe");
        ProcessStartInfo oef7 = new ProcessStartInfo(@"C:\Users\12202625\offline documenten\Shpielerij\CSharpShpielerij\ExtraOefeningenStreams\Oefening7\bin\Debug\Oefening7.exe");
        ProcessStartInfo oef8 = new ProcessStartInfo(@"C:\Users\12202625\offline documenten\Shpielerij\CSharpShpielerij\ExtraOefeningenStreams\Oefening8\bin\Debug\Oefening8.exe");
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartOef_Click(object sender, RoutedEventArgs e)
        {
            string name = ((Button)sender).Name;
            switch (((Button)sender).Name)
            {
                case nameof(BtnOef1):
                    Process.Start(oef1);
                    break;
                case nameof(BtnOef2):
                    Process.Start(oef2);
                    break;
                case nameof(BtnOef3):
                    Process.Start(oef3);
                    break;
                case nameof(BtnOef4):
                    Process.Start(oef4);
                    break;
                case nameof(BtnOef5):
                    Process.Start(oef5);
                    break;
                case nameof(BtnOef6):
                    Process.Start(oef6);
                    break;
                case nameof(BtnOef7):
                    Process.Start(oef7);
                    break;
                case nameof(BtnOef8):
                    Process.Start(oef8);
                    break;
                default:
                    break;

            }
        }
    }
}
