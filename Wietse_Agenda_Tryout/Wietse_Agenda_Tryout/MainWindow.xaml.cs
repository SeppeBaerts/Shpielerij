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

namespace Wietse_Agenda_Tryout
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

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void MainCanvas_DragOver(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);
            if (data is UIElement element)
            {
                Canvas.SetLeft(element, e.GetPosition(MainCanvas).X);
                Canvas.SetTop(element, e.GetPosition(MainCanvas).Y - 15);
                if (!MainCanvas.Children.Contains(element))
                    MainCanvas.Children.Add(element);
            }
        }

        private void MnuAgenda_Click(object sender, RoutedEventArgs e)
        {
            AgendaWindow agendawindow = new AgendaWindow();
            agendawindow.Show();
        }

        private void MainCanvas_DragLeave(object sender, DragEventArgs e)
        {
            if(e.OriginalSource == MainCanvas)
            {
                if(e.Data.GetData(DataFormats.Serializable) is UIElement element)
                    MainCanvas.Children.Remove(element);
            }
        }

        private void BtnGetVakken_Click(object sender, RoutedEventArgs e)
        {
           string[] strings = TxtGetText.Text.Split('\r');
            double x = -200;
            double y = 0;
            foreach(string s in strings)
            {
                string st = s.Trim('\n');
                if (st.StartsWith("-"))
                {
                    y +=50;
                    DragBox drag = new DragBox(MainCanvas, st.Trim('-'), false, new Point(x,y));
                }
                else
                {                    
                    x += 200;
                    y = 10;
                    DragBox drag = new DragBox(MainCanvas, st, true, new Point(x, y));
                }
            }
        }
    }
}
