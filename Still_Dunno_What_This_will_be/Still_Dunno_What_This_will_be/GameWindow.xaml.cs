using GameEngineLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Still_Dunno_What_This_will_be
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {

        FileStream game;
        Player player;
        DispatcherTimer refresher = new DispatcherTimer();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fs">The game-File that needs to be opened</param>
        
        public GameWindow(FileStream fs)
        {
            InitializeComponent();
            LevelController.IsCreateWindow = LevelController.IsTestWindow = false;
            TemporaryStorage.GameRects.Clear();
            game = fs;
            refresher.Interval = new TimeSpan(0, 0, 0, 0, 20);
            refresher.Tick += Refresher_Tick;

        }

        private void Refresher_Tick(object sender, EventArgs e)
        {
            refresher.Stop();
            player?.MovePlayer();
            TemporaryStorage.Move();
            if (!TemporaryStorage.HasCompleted)
                refresher.Start();
            else FinishedLevel();
        }
        private void FinishedLevel()
        {
             TemporaryStorage.GameRects.Clear();
            GameCanvas.Children.Clear();
            Label finished = new Label
            {
                Content = "Game Completed!",
                Foreground = Brushes.Green,
                FontSize = 28,
            };
            Canvas.SetTop(finished, 50);
            Canvas.SetLeft(finished, 50);
            GameCanvas.Children.Add(finished);
            Button close = new Button()
            {
                Content = "Close",
                Background = Brushes.Transparent,
                Foreground = Brushes.Red,
                FontSize = 28,
                Width = 150,
                BorderBrush = Brushes.Red,
                BorderThickness = new Thickness(5)
            };
            close.Click += Close_Click;
            Canvas.SetLeft(close, (GameCanvas.ActualWidth / 2) - (close.Width/2));
            Canvas.SetBottom(close, 50);
            GameCanvas.Children.Add(close);
            
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateObjects()
        {
            GameCanvas.Children.Clear();
            string[] objectConcepts;
            using (StreamReader sr = new StreamReader(game))
            {
                while (!sr.EndOfStream)
                {
                    objectConcepts = sr.ReadLine().Split(';');
                    double left = double.Parse(objectConcepts[1]);
                    double top = double.Parse(objectConcepts[2]);
                    int height = int.Parse(objectConcepts[3]);
                    int width = int.Parse(objectConcepts[4]);
                    int movementSpeed = int.Parse(objectConcepts[5]);
                    bool hasCollisionDetection = objectConcepts[6].Trim('=') == "1";
                    int movementAmount = int.Parse(objectConcepts[7]);
                    GameItem gI = new GameItem();
                    if (objectConcepts[0] == "OO") gI = new GameItemCircle(left, top, width, height,hasCollisionDetection);
                    else if (objectConcepts[0] == "--") gI = new GameItemRectangle(left, top, width, height,hasCollisionDetection, movementSpeed, movementAmount);
                    else if (objectConcepts[0] == "EE") gI = new GameItemEndPoint(left, top, width, height, hasCollisionDetection, movementSpeed);
                    else if (objectConcepts[0] == "PP")
                    {
                        player = new Player(left, top, width, height, movementSpeed, false); //De false nog aanpasbaar maken
                        player.Parent = GameCanvas;
                    }

                    if (!gI.IsEmpty)
                        gI.Parent = GameCanvas;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TemporaryStorage.HasCompleted = false;
            CreateObjects();
            refresher.Start();
        }

        private void GameCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left) player.Direction = 'L';
            else if (e.Key == Key.Right) player.Direction = 'R';
            else if (e.Key == Key.Up) player.Direction = 'U';
            else if (e.Key == Key.Down) player.Direction = 'D';
            else if (e.Key == Key.Space) player.Jump();
            else if (e.Key == Key.M) TemporaryStorage.PlayerMoves = !TemporaryStorage.PlayerMoves;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (IsCurrentDirection(e.Key))
                player.Direction = '0';
        }
        private bool IsCurrentDirection(Key key)
        {
            if (key == Key.Left && player.Direction == 'L') return true;
            else if (key == Key.Right && player.Direction == 'R') return true;
            else if (key == Key.Up && player.Direction == 'U') return true;
            else if (key == Key.Down &&  player.Direction == 'D') return true;
            return false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            TemporaryStorage.GameRects.Clear();
            TemporaryStorage.Items.Clear();
            TemporaryStorage.HasCompleted = false;
            refresher.Stop();
            player = null;
            refresher = null;
        }
    }
}
