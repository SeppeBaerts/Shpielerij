using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wietse_Agenda_Tryout
{
    internal class DragBox
    {
        public TextBlock DraggingBox { get; set; }
        public Canvas ParentCanvas { get; set; }
        public string CardText { get; set; }
        public bool IsParent { get; set; }

        private Point dropLocation;

        public Point DropLocation
        {
            get { return dropLocation; }
            set {
                Canvas.SetLeft(DraggingBox, value.X - (DraggingBox.ActualWidth / 2));
                Canvas.SetTop(DraggingBox, value.Y-15);
                dropLocation = value;
            }
        }

        public DragBox(Canvas parentCanvas, string cardText, bool isParent, Point DropLocation)
        {
            IsParent = isParent;
            DraggingBox = new TextBlock
            {
                Text = cardText,
                Background = IsParent ? Brushes.Maroon : Brushes.Orange,
                Foreground = IsParent ? Brushes.White : Brushes.Black,
                FontSize = 15,
                Width = 150,
                Height = IsParent? 25 : 50,
                TextWrapping = TextWrapping.Wrap,

            };
            Border brd = new Border
            {
                Height = 50,
                Width = 150,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(2),
            };
            //border toevoegen
            ParentCanvas = parentCanvas;

            Canvas.SetLeft(DraggingBox, DropLocation.X);
            Canvas.SetTop(DraggingBox, DropLocation.Y);            
            Canvas.SetLeft(brd, DropLocation.X);
            Canvas.SetTop(brd, DropLocation.Y);
            parentCanvas.Children.Add(brd);
            ParentCanvas.Children.Add(DraggingBox);
            if(!isParent)
                DraggingBox.MouseMove += Text_MouseMove;
        }
        private void Text_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                DraggingBox.IsHitTestVisible = false;
                DragDrop.DoDragDrop(DraggingBox, new DataObject(DataFormats.Serializable, DraggingBox), DragDropEffects.Move);
                DraggingBox.IsHitTestVisible = true;
            }
        }
        public TextBlock GetTextBlock()
        {
            return DraggingBox;
        }
    }
}
