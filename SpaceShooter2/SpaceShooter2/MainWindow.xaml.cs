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
using System.Windows.Threading;

namespace SpaceShooter2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rock rk;
        Ship ship;
        bool isRight, isLeft, isStill;

        DispatcherTimer refresh = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            refresh.Interval = new TimeSpan(0, 0, 0, 0, 15);
            refresh.Tick += Refresh_Tick;
        }

        private void Refresh_Tick(object sender, EventArgs e)
        {
            refresh.Stop();
            if (isRight)
                ship.MoveRight();
            if(isLeft)
                ship.MoveLeft();
            rk.FallDown();
            if (rk.ToBeRemoved)
            {
                rk = null;
                rk = new Rock(MainCanvas);
                CanvasRows.Add(rk);
            }
            ship.MoveRockets();
            CanvasRows.Check();
            refresh.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ship = new Ship(MainCanvas);
            rk = new Rock(MainCanvas);
            CanvasRows.one = new CanvasRow(MainCanvas, 2);
            CanvasRows.two = new CanvasRow(MainCanvas, 2, CanvasRows.one);
            CanvasRows.one.Down = CanvasRows.two;
            CanvasRows.Initiate();
            CanvasRows.Add(rk);
            refresh.Start();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
                isRight = false;
            if (e.Key == Key.Left)
                isLeft = false;
            isStill = (!Keyboard.IsKeyDown(Key.Right)) && (!Keyboard.IsKeyDown(Key.Left));
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                ship.ShootRocket();
            isRight = Keyboard.IsKeyDown(Key.Right);
            isLeft = Keyboard.IsKeyDown(Key.Left);
        }
    }
}
