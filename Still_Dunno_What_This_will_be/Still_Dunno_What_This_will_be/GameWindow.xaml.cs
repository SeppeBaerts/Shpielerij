using GameEngineLib;
using GameEngineLib.PowerUps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        FileInfo game;
        Player player;
        DispatcherTimer refresher = new DispatcherTimer();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fs">The game-File that needs to be opened</param>
        
        public GameWindow(string filePath)
        {
            InitializeComponent();
            LevelController.IsCreateWindow = LevelController.IsTestWindow = false;
            game = new FileInfo(filePath);
            refresher.Interval = new TimeSpan(0, 0, 0, 0, 20);
            refresher.Tick += Refresher_Tick;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TemporaryStorage.HasCompleted = false;
            CreateObjects();
        }

        private void CreateObjects()
        {
            TemporaryStorage.Items.Clear();
            TemporaryStorage.HasCompleted = TemporaryStorage.HasDied = false;
            refresher.Stop();
            GameCanvas.Children.Clear();
            string[] objectConcepts;
            using (StreamReader sr = new StreamReader(game.FullName))
            {
                while (!sr.EndOfStream)
                {
                    objectConcepts = sr.ReadLine().Split(';');
                    double left = double.Parse(objectConcepts[1]);
                    double top = double.Parse(objectConcepts[2]);
                    int height = int.Parse(objectConcepts[3]);
                    int width = int.Parse(objectConcepts[4]);
                    int movementSpeed = objectConcepts.Count() > 5? int.Parse(objectConcepts[5]): 0;
                    bool hasCollisionDetection = objectConcepts.Count() > 6? objectConcepts[6].Trim('=') == "1" : false;
                    int movementAmount = objectConcepts.Count() > 7 ? int.Parse(objectConcepts[7]) : 0;
                    string nextFile = objectConcepts.Count() > 8 && objectConcepts[0] == "EE"? objectConcepts[8] : null;
                    bool canBePickedUp = objectConcepts.Count() > 8 && objectConcepts[0] == "OO" || objectConcepts[0] == "--" ? objectConcepts[8] == "1" : false;
                    GameItem gI = new GameItem();
                    if (objectConcepts[0] == "OO") gI = new GameItemCircle(left, top, width, height, hasCollisionDetection, canBePickedUp);
                    else if (objectConcepts[0] == "--") gI = new GameItemRectangle(left, top, width, height, hasCollisionDetection, canBePickedUp, movementSpeed, movementAmount);
                    else if (objectConcepts[0] == "EE") gI = new GameItemEndPoint(left, top, width, height, hasCollisionDetection, movementSpeed, nextFile);
                    else if (objectConcepts[0] == "XX") gI = new GameItemGameOver(left, top, width, height);
                    else if (objectConcepts[0] == "PP")
                    {
                        player = new Player(left, top, width, height, movementSpeed, false); //De false nog aanpasbaar maken
                        player.Parent = GameCanvas;
                    }
                    else if (objectConcepts[0] == "PUJB"){
                        PowerJumpBoost pow = new PowerJumpBoost(left, top);
                        pow.Parent = GameCanvas;
                    }

                    if (!gI.IsEmpty)
                        gI.Parent = GameCanvas;
                }
            }
            TemporaryStorage.CurrentPlayer = player; 
            if (TemporaryStorage.GameOverRect.IsEmpty)
            {
                GameItemGameOver killPlatForm = new GameItemGameOver(0, GameCanvas.ActualHeight, ((int)Math.Truncate(GameCanvas.ActualWidth)), 50);
                killPlatForm.Parent = GameCanvas;
                TemporaryStorage.GameOverRect = killPlatForm.OutlineRect;
            }
            refresher.Start();
        }

        private void Refresher_Tick(object sender, EventArgs e)
        {
            refresher.Stop();
            player?.MovePlayer();
            TemporaryStorage.Move();
            if (!TemporaryStorage.HasCompleted && !TemporaryStorage.HasDied)
                refresher.Start();
            else EndGame();
        }
        private void EndGame()
        {
            if (TemporaryStorage.HasCompleted)
                FinishedLevel();
            else if (TemporaryStorage.HasDied)
                IsDead();
        }
        private void IsDead()
        {
            GameCanvas.Children.Clear();
            Label finished = new Label
            {
                Content = "Ya died",
                Foreground = Brushes.Green,
                FontSize = 28,
            };
            Canvas.SetTop(finished, 50);
            Canvas.SetLeft(finished, GameCanvas.ActualWidth/2);
            GameCanvas.Children.Add(finished);
            Canvas.SetLeft(finished, (GameCanvas.ActualWidth / 2) - (finished.ActualWidth/2));
            Button tryAgain = new Button()
            {
                Content = "Try again",
                Background = Brushes.Transparent,
                Foreground = Brushes.Red,
                FontSize = 28,
                Width = 150,
                BorderBrush = Brushes.Red,
                BorderThickness = new Thickness(5)
            };
            tryAgain.Click += TryAgain_Click;
            Canvas.SetLeft(tryAgain, (GameCanvas.ActualWidth / 2) - (tryAgain.Width / 2));
            Canvas.SetBottom(tryAgain, 50);
            GameCanvas.Children.Add(tryAgain);
        }

        private void TryAgain_Click(object sender, RoutedEventArgs e)
        {
            CreateObjects();
        }

        private void FinishedLevel()
        {
            if (string.IsNullOrWhiteSpace(TemporaryStorage.BigEnd.NextLevel))
            {
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
                Canvas.SetLeft(close, (GameCanvas.ActualWidth / 2) - (close.Width / 2));
                Canvas.SetBottom(close, 50);
                GameCanvas.Children.Add(close);
            }
            else
            {
                game = new FileInfo(TemporaryStorage.BigEnd.NextLevel);
                CreateObjects();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GameCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left) player.Direction = 'L';
            else if (e.Key == Key.Right) player.Direction = 'R';
            else if (e.Key == Key.Up) player.Direction = 'U';
            else if (e.Key == Key.Down) player.Direction = 'D';
            else if (e.Key == Key.Space) player.Jump();
            else if (e.Key == Key.M) TemporaryStorage.PlayerMoves = !TemporaryStorage.PlayerMoves;
            else if (e.Key == Key.LeftShift) TemporaryStorage.PressingLiftKey = true;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (IsCurrentDirection(e.Key))
                player.Direction = '0';
            if (e.Key == Key.LeftShift)
                TemporaryStorage.PressingLiftKey = false;
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
            TemporaryStorage.Items.Clear();
            TemporaryStorage.HasCompleted = false;
            refresher.Stop();
            player = null;
            refresher = null;
        }
    }
}
