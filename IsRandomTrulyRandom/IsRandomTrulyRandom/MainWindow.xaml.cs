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

namespace IsRandomTrulyRandom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();
        int one, two, three, four, five, six, seven, eight, nine = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CreateRandom()
        {
            if (int.TryParse(TxtAmount.Text, out int amount)) {
                for (int i = 0; i < amount; i++)
                    AssignRandom(rnd.Next(0,100));
            }
        }
        private void AssignRandom(int randomNumber)
        {
            int firstNumber = int.Parse(randomNumber.ToString()[0].ToString());
            switch (firstNumber)
            {
                case 1: one++; break;
                case 2: two++; break;
                case 3: three++; break;
                case 4: four++; break;
                case 5: five++; break;
                case 6: six++; break;
                case 7: seven++; break;
                case 8: eight++; break;
                case 9: nine++; break;
                default: break;
            }
        }
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            CreateRandom();
            UpdateTextBoxes();
        }
        private void UpdateTextBoxes()
        {
            TxtOne.Text = one.ToString();
            TxtTwo.Text = two.ToString();
            TxtThree.Text = three.ToString();
            TxtFour.Text = four.ToString();
            TxtFive.Text = five.ToString();
            TxtSix.Text = six.ToString();
            TxtSeven.Text = seven.ToString();
            TxtEight.Text = eight.ToString();
            TxtNine.Text = nine.ToString();
        }
    }
}
