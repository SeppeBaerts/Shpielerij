using Solitaire2_Tryout.Cards;
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

namespace Solitaire2_Tryout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Card> cards;
        List<Pile> piles;
        public MainWindow()
        {
            InitializeComponent();
            CreateCardsAndList();
        }
        private void CreateCardsAndList()
        {
            cards = new List<Card>();
            piles = new List<Pile>();
            Card card1 = new Card("Klaveren 5", 5, true, MainCanvas);
            Pile pile1 = new Pile(1, new Point(15, 15));
            cards.Add(card1);
            piles.Add(pile1);
            pile1.Children.Add(card1);
            
        }
    }
}
