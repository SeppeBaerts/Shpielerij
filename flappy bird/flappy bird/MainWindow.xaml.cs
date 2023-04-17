using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace flappy_bird
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Pipe pipe ;
        List<Pipe> pipes = new List<Pipe>();
        List<Pipe> pipesToRemove = new List<Pipe>();
        DispatcherTimer refreshTimer = new DispatcherTimer();
        Bird bird;
        int score = 0;
        int jumpcount= 0;        
        public MainWindow()
        {
            InitializeComponent();
             LocalSettings.RefreshRate = 100;
            refreshTimer.Interval = LocalSettings.FramesPerSecond;
            refreshTimer.Tick += Refresh;
        }

        private void Refresh(object sender, EventArgs e)
        {
            refreshTimer.Stop();
            if (!bird.IsDead && pipes.First().GetLeftLocation() < bird.LeftPosition + bird.Width && pipes.First().GetLeftLocation() + pipes.First().Width > bird.LeftPosition)
                bird.CheckDead(pipes.First());
            else
                bird.ChangeColor();

            foreach (Pipe pipe in pipes)
            {
                pipe.Update();
                if (!pipe.WentOver && pipe.GetLeftLocation() < bird.LeftPosition)
                {
                    score++;
                    pipe.WentOver = true;
                    UpdateScore();
                }
                if(pipe.GetLeftLocation() < 0 - pipe.Width)
                    pipesToRemove.Add(pipe);
            }
            foreach(Pipe pipe in pipesToRemove)
            {
                pipe.Delete();
                pipes.Remove(pipe);
            }

            pipesToRemove.Clear();
            if (pipes.Last().GetLeftLocation() < MainCanvas.ActualWidth/1.5)
                CreatePipe();
            if (jumpcount < 0)
                bird.Fall();
            else if (jumpcount == 0)
                jumpcount--;
            else
            {
                bird.Jump();
                jumpcount--;
            }
            refreshTimer.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreatePipe();
            bird = new Bird(LocalSettings.RelativeRefreshRate(8),LocalSettings.RelativeRefreshRate(5), MainCanvas);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && !bird.IsDead)
            {
                if (!refreshTimer.IsEnabled)
                    refreshTimer.Start();
                jumpcount += 8;
            }
            else if (e.Key == Key.Escape)
            {
                _ = 1 == 1;
            }
            else if (e.Key == Key.R)
            {
                refreshTimer.Stop();
                score = 0;
                foreach (Pipe pipe in pipes)
                    pipe.Delete();
                bird.Delete();
                pipes.Clear();
                CreatePipe();
                bird = new Bird(8, 5, MainCanvas);
            }
            else if (e.Key.Equals(Key.P))
            {
                if (refreshTimer.IsEnabled)
                    refreshTimer.Stop();
                else
                    refreshTimer.Start();
            }
            else if (e.Key == Key.Add)
            {
                LocalSettings.RefreshRate += 10;
                refreshTimer.Stop();
                refreshTimer.Interval = LocalSettings.FramesPerSecond;
                refreshTimer.Start();
            }

        }
        private void UpdateScore()
        {
            TxbScore.Text = $"Score: {score}";
        }
        private void CreatePipe()
        {
            pipe = new Pipe(new Random().Next(0, (int)MainCanvas.ActualHeight - 150), 50, MainCanvas);
            pipes.Add(pipe);
        }
    }
}
