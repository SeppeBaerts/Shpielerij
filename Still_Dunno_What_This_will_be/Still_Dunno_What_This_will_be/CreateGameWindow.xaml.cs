using GameEngineLib;
using GameEngineLib.PowerUps;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
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

namespace Still_Dunno_What_This_will_be
{
    /// <summary>
    /// Interaction logic for CreateGameWindow.xaml
    /// </summary>
    public partial class CreateGameWindow : Window
    {
        //List<GameItem> gameItems = new List<GameItem>();
        List<int> answers = new List<int>();
        string fileName = "TempFile.txt";
        GameItem propObject;

        public CreateGameWindow()
        {
            InitializeComponent();
            LevelController.IsCreateWindow = true;
        }
        private void MnuController()
        {
            //This is something that would say; create a circle, or i dunno what else
        }

        #region MenuItems
        private void MnuCreateCircle_Click(object sender, RoutedEventArgs e)
        {
            GameItemCircle circle = new GameItemCircle(55, 55, 25, 25, true);
            circle.Parent = GameCanvas;
        }

        private void MnuEndpoint_Click(object sender, RoutedEventArgs e)
        {
            GameItemEndPoint endPoint = new GameItemEndPoint(55, 55, 150, 50);
            endPoint.Parent = GameCanvas;
        }

        private void MnuCreatePlayer_Click(object sender, RoutedEventArgs e)
        {
            Player player = new Player(55, 55, 50, 50, 5, false);
            player.Parent = GameCanvas;
        }

        private void MnuCreateRectangle_Click(object sender, RoutedEventArgs e)
        {
            GameItemRectangle rectangle = new GameItemRectangle(15, 15, 150, 20, true); ;
            rectangle.Parent = GameCanvas;
        }
        private void MnuCreateDead_Click(object sender, RoutedEventArgs e)
        {
            GameItemGameOver gameOver = new GameItemGameOver(15, 15, 500, 20);
            gameOver.Parent = GameCanvas;
        }

        private void MnuCreateJumpBoost_Click(object sender, RoutedEventArgs e)
        {
            PowerJumpBoost pow = new PowerJumpBoost(15, 15);
            pow.Parent = GameCanvas;
        }

        private void MnuSaveGame_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text Files(*.txt)|*.txt";
            if ((bool)dialog.ShowDialog())
            {
                using (StreamWriter sw = new StreamWriter(dialog.FileName))
                    foreach (GameItem gi in TemporaryStorage.Items)
                        sw.WriteLine(gi.ToString());
            }
        }

        private void MnuTestGame_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false))
                foreach (GameItem gi in TemporaryStorage.Items)
                    sw.WriteLine(gi.ToString());
            TemporaryStorage.Clear();
            GameWindow gw = new GameWindow(fileName);
            gw.ShowDialog();
            TemporaryStorage.Revert();
        }

        private void MnuOpenSaveFIle_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text file *.txt|*.txt";
            if ((bool)ofd.ShowDialog())
            {
                fileName = ofd.FileName;
                try
                {
                    LoadTemplate(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show($"Could not open {ofd.FileName}", "YA BROKE IT");
                }
            }
        }

        #endregion
        #region ChangingProperty's
        private void ChangePropertys()
        {
            propObject = SelectedObject.GetSelectedGameItem();
            SelectedObject.GetPropertys(out List<string> propNames, out List<object> propValues);
            StackProp.Children.RemoveRange(1, StackProp.Children.Count);
            if (propNames.Count() == propValues.Count())
            {
                for (int i = 0; i < propNames.Count(); i++)
                {
                    Button btn = new Button();
                    btn.IsDefault = true;
                    btn.Click += Btn_Click;
                    UIElement ui = new UIElement();
                    WrapPanel wrap = new WrapPanel();
                    StackProp.Children.Add(wrap);
                    Label label = new Label
                    {
                        Content = $"{propNames[i]}: ",
                        Foreground = Brushes.White,
                        FontSize = 18,
                    };
                    wrap.Children.Add(label);
                    if (propValues[i] is bool yesNo)
                    {
                        ui = new CheckBox
                        {
                            IsChecked = yesNo,
                            VerticalAlignment = VerticalAlignment.Center,

                        };
                        ((CheckBox)ui).Checked += Ui_Checked;
                        ((CheckBox)ui).Unchecked += Ui_Checked;
                    }
                    else if (propValues[i] is int number)
                    {
                        ui = new TextBox
                        {
                            Text = $"{number}",
                            FontSize = 18,
                            Width = 100,
                            HorizontalAlignment = HorizontalAlignment.Right,
                        };
                        ui.KeyDown += Ui_KeyDown;
                    }
                    else if (propValues[i] is double number1)
                    {
                        ui = new TextBox
                        {
                            Text = $"{number1}",
                            FontSize = 18,
                            Width = 100,
                            HorizontalAlignment = HorizontalAlignment.Right,

                        };
                        ui.KeyDown += Ui_KeyDown;
                    }
                    else if (propValues[i] is string saySomething)
                    {
                        //Hier nog die enter = changeValue bijzetten.
                        ui = new TextBox
                        {
                            Text = saySomething,
                            FontSize = 18,
                            Width = 100,
                            HorizontalAlignment = HorizontalAlignment.Right,
                        };
                    }
                    ui.LostFocus += Ui_LostFocus;
                    wrap.Children.Add(ui);
                }
                if (propObject is GameItemEndPoint end)
                {
                    Button btn = new Button
                    {
                        Content = "Add next Level",
                        FontSize = 18,
                        Padding = new Thickness(5),
                    };
                    StackProp.Children.Add(btn);
                    btn.Click += Btn_Click;
                }
            }
            else throw new ArgumentOutOfRangeException("2 Lists must be the same length");
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Text Files(*.txt)|*.txt",
            };
            if ((bool)ofd.ShowDialog())
                ((GameItemEndPoint)propObject).NextLevel = ofd.FileName;
        }

        private void Ui_Checked(object sender, RoutedEventArgs e)
        {
            ValueDirector(sender);
        }

        private void Ui_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
                ((TextBox)sender).Text = (double.Parse(((TextBox)sender).Text) - 1).ToString();
            else if (e.Key == Key.Up)
                ((TextBox)sender).Text = (double.Parse(((TextBox)sender).Text) + 1).ToString();
            else if (e.Key == Key.Enter)
            {
                Keyboard.ClearFocus();
                ValueDirector(sender);
            }
            if (!(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9|| e.Key == Key.Subtract|| e.Key == Key.OemMinus||e.Key == Key.Tab))
                e.Handled = true;
        }

        private void Ui_LostFocus(object sender, RoutedEventArgs e)
        {
            ValueDirector(sender);
        }
        private void ValueDirector(object sender)
        {
            if (sender is CheckBox check && check.Parent is WrapPanel wrap)
                ChangeValue((string)((Label)wrap.Children[0]).Content, (bool)check.IsChecked);
            else if (sender is TextBox text && text.Parent is WrapPanel wrap1 && double.TryParse(text.Text, out double number))
                ChangeValue((string)((Label)wrap1.Children[0]).Content, number);
            else if (sender is TextBox text1 && text1.Parent is WrapPanel wrap2)
                ChangeValue((string)((Label)wrap2.Children[0]).Content, text1.Text);
        }
        private void ChangeValue(string prop, object changedValue)
        {
            prop = prop.Replace(": ", "").ToLower();
            try
            {
                switch (prop)
                {
                    case "width": propObject.Width = ((int)(double)changedValue); return;
                    case "height": propObject.Height = ((int)(double)changedValue); return;
                    case "collision detection": propObject.HasCollisionDetection = (bool)changedValue; return;
                    case "speed": propObject.MovementSpeed = ((int)(double)changedValue); return;
                    case "gravity": propObject.HasGravity = (bool)changedValue; return;
                    case "amount movement": propObject.MovingAmount = ((int)(double)changedValue); return;
                    case "has health": ((Player)propObject).HasHealth = (bool)changedValue; return;
                    case "health": ((Player)propObject).Health = ((int)(double)changedValue); return;
                    default: break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Somehting went horribly wrong trying to save the property\nThere is no excuse. Only an explaination: {ex.Message}", "We done goofed");
            }

        }
        #endregion

        private bool HasNumbers(int expectedAnswers = 4)
        {
            if (TemporaryStorage.Antwoorden != null && TemporaryStorage.Antwoorden.Count == expectedAnswers)
            {
                answers = TemporaryStorage.Antwoorden;
                return true;
            }
            else MessageBox.Show("Something went wrong, did you fill it out correctly?", "Something went wrong");
            return false;
        }

        private void GameCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (!SelectedObject.JustChanged && SelectedObject.HasSelectedGameItem())
            {
                SelectedObject.GetSelectedGameItem().SetLocation(e.GetPosition(GameCanvas));
                SelectedObject.ClearGameOutline();
            }
            else {
                SelectedObject.JustChanged = false;
                if (SelectedObject.HasSelectedGameItem())
                    ChangePropertys();
            }
        }

        private void GetOptions(string[] extra = null)
        {
            List<string> strings = new List<string>
            {
                {"Left: " },
                {"Top: " },
                {"Height: " },
                {"Width: " }
            };
            if (extra?.Count() > 0) strings.AddRange(extra);
            GameObjectOptions goo = new GameObjectOptions(strings.ToArray());
            goo.ShowDialog();
        }

        private void LoadTemplate(string filePath)
        {
            GameCanvas.Children.Clear();
            TemporaryStorage.Items.Clear();
            string[] objectConcepts;
            using (StreamReader sr = new StreamReader(filePath))
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
                    if (objectConcepts[0] == "OO") gI = new GameItemCircle(left, top, width, height, hasCollisionDetection);
                    else if (objectConcepts[0] == "--") gI = new GameItemRectangle(left, top, width, height, hasCollisionDetection,false, movementSpeed, movementAmount);
                    else if (objectConcepts[0] == "EE") gI = new GameItemEndPoint(left, top, width, height, hasCollisionDetection, movementSpeed);
                    else if (objectConcepts[0] == "PP") gI = new Player(left, top, width, height,movementSpeed, false);
                    gI.Parent = GameCanvas;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StackProp.Visibility = (StackProp.Visibility == Visibility.Visible ?Visibility.Collapsed : Visibility.Visible);
        }

        private void MnuDevTesting_Checked(object sender, RoutedEventArgs e)
        {
            LevelController.ShowDevOutlines = (bool)((MenuItem)sender).IsChecked;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            LevelController.IsCreateWindow = false;
            LevelController.ShowDevOutlines = false;
        }

    }
}
