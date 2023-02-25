using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Solitaire2_Tryout.Cards
{
    internal class Pile
    {
        public List<Card> Children { get; set; } = new List<Card>();
        public int PileNumber { get; set; }
        public Point LocationTop { get; set; }
        public Pile(int pileNumber, Point pileTop) 
        {
            PileNumber = pileNumber;
            LocationTop = pileTop;
        }

        public void ChangePile(Pile droppingPile)
        {

        }

    }
}
