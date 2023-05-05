using GameEngineLib;
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
using System.Windows.Shapes;

namespace Still_Dunno_What_This_will_be
{
    /// <summary>
    /// Interaction logic for GameObjectOptions.xaml
    /// </summary>
    public partial class GameObjectOptions : Window
    {
        public GameObjectOptions(string[] options)
        {
            InitializeComponent();
            foreach (string option in options)
            {
                WrapPanel wrap = new WrapPanel();
                MainStack.Children.Add(wrap);
                Label lbl = new Label
                {
                    Content = option.TrimStart(';'),
                    FontSize = 18
                };
                UIElement el = new UIElement();
                if (option.StartsWith(";;"))
                {
                    el = new CheckBox();
                }
                else
                {
                    el = new TextBox
                    {
                        Width = 150,
                        FontSize = 18,
                    };
                }
                wrap.Children.Add(lbl);
                wrap.Children.Add(el);
            }
            Button btn = new Button {
                Content = "Submit",
            };
            btn.Click += Btn_Click;
            MainStack.Children.Add(btn);
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            List<int> answers = new List<int>();
            foreach(var child in MainStack.Children)
            {
                if (child is WrapPanel wrap)
                {
                    if (wrap.Children[1] is TextBox text && int.TryParse(text.Text, out int amount) && amount > 1)
                        answers.Add(amount);
                    else if (wrap.Children[1] is CheckBox check)
                        answers.Add((bool)check.IsChecked ? -1 : -2);
                }
            }
            TemporaryStorage.Antwoorden = answers;
            Close();
        }
    }
}
