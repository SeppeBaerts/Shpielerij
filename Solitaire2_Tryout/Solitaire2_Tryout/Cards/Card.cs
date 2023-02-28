using Solitaire2_Tryout.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Solitaire2_Tryout
{
    internal class Card
    {
        public string CardName { get; set; }
        public string CardColor {
            get
            {
                return (CardName.Contains("Harten") || CardName.Contains("Ruiten")) ? "Rood" : "Zwart";
            }
        }
        public int CardValue { get; set; }
        public Image CardImage { get; set; }
        public Pile Parent { get; set; }
        public bool IsTurned { get; set; }
        public bool IsNested { get; set; }
        public bool IsMoving { get; set; }
        public Canvas ParentCanvas { get; set; }

        private Point oldMouseLocation;

        public ImageSource CardImgSource { get
            {
                return IsTurned ? new BitmapImage(new Uri($"/Assets/Kaarten/{CardName}.png", UriKind.RelativeOrAbsolute)) : new BitmapImage(new Uri($"/Assets/Kaarten/BackOfCard.png", UriKind.RelativeOrAbsolute));
            } }

        public Card(string cardName, int cardValue, bool isTurned, Canvas parentCanvas)
        {
            CardName = cardName;
            CardValue = cardValue;
            IsTurned = isTurned;
            CardImage = new Image
            {
                Height = 120,
                Source = CardImgSource,
            };
            CardImage.MouseLeftButtonDown += CardImage_MouseLeftButtonDown;
            CardImage.MouseMove += CardImage_MouseMove;
            CardImage.MouseLeave += CardImage_MouseLeave;
            Canvas.SetLeft(CardImage, 0);
            Canvas.SetTop(CardImage, 0);
            ParentCanvas = parentCanvas;
            ParentCanvas.Children.Add(CardImage);
        }

        private void CardImage_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            IsMoving = false;
            oldMouseLocation = e.GetPosition(ParentCanvas);

        }

        private void CardImage_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (IsMoving)
            {
                Canvas.SetLeft(CardImage, Canvas.GetLeft(CardImage) + e.GetPosition(ParentCanvas).X - oldMouseLocation.X);
                Canvas.SetTop(CardImage, Canvas.GetTop(CardImage) + e.GetPosition(ParentCanvas).Y - oldMouseLocation.Y);
                oldMouseLocation = e.GetPosition(ParentCanvas);
            }
            if (!IsMoving)
            {
                oldMouseLocation = e.GetPosition((Image)sender);
            }
        }

        private void CardImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsMoving = true;
        }

        public void Move(Point location)
        {
            int indexnumber = Parent.Children.IndexOf(this);
            if (IsNested)
            {
                Parent.Children[indexnumber-1].Move(new Point(location.X, location.Y+20));
            }
            Canvas.SetTop(CardImage, location.Y);
            Canvas.SetLeft(CardImage, location.X);
        }
    }
}
