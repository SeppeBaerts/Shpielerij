using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wietse_Agenda_Tryout
{
    internal class DragBox
    {
        //eigenlijk is dit bad practice, dit werkt niet op alle platforme
        public Label DraggingBox { get; set; }
        public TextBlock Blokkie { get; set; }
        public Canvas ParentCanvas { get; set; }
        public StackPanel ParentStackPanel { get; set; }
        public string CardText { get; set; }
        public bool IsParent { get; set; }
        public string CardContent { get; set; }

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
            string uid = SettingStatic.Uid.ToString();
            IsParent = isParent;
            Blokkie = new TextBlock
            {
                Text = cardText,
                FontSize = 15,
                Foreground = IsParent ? Brushes.White : Brushes.Black,
                TextWrapping = TextWrapping.Wrap,
            };
            DraggingBox = new Label
            {
                Content = Blokkie,
                Background = IsParent ? Brushes.Maroon : Brushes.Orange,
                Width = 150,
                MinHeight = IsParent ? 35 : 50,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(2),
                Padding = new Thickness(0),
                Uid = uid
            };
            ParentCanvas = parentCanvas;
            Canvas.SetLeft(DraggingBox, DropLocation.X);
            Canvas.SetTop(DraggingBox, DropLocation.Y);            
            ParentCanvas.Children.Add(DraggingBox);
            if(!isParent)
                DraggingBox.MouseMove += Text_MouseMove;
            SettingStatic.DragBoxesDictionary.Add(uid, this);

        }
        public DragBox(string cardText, StackPanel parent, string cardContent)
        {
            string uid = SettingStatic.Uid.ToString();
            CardText = cardText;
            Blokkie = new TextBlock
            {
                Text = cardText,
                FontSize = 15,
                Foreground = Brushes.Black,
                TextWrapping = TextWrapping.Wrap,
            };
            DraggingBox = new Label
            {
                Content = Blokkie,
                Background = Brushes.Orange,
                Width = 150,
                MinHeight = 50,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(2),
                Padding = new Thickness(0),
                Uid = uid,
            };
            CardContent = cardContent;
            DraggingBox.Tag = CardContent;
            ParentStackPanel = parent;
            parent.Children.Add(DraggingBox);
            DraggingBox.MouseMove += Text_MouseMove;
            SettingStatic.DragBoxesDictionary.Add(uid, this);

        }
        private void Text_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                DraggingBox.IsHitTestVisible = false;
                try
                {
                    DragDrop.DoDragDrop(DraggingBox, new DataObject(DataFormats.Serializable, DraggingBox), DragDropEffects.Move);

                }
                catch
                {
                    MessageBox.Show("something went wrong, we don't know exactly what... but we will try to fix it.");
                }
                DraggingBox.IsHitTestVisible = true;
            }
        }
        public Label GetTextBlock()
        {
            return DraggingBox;
        }
    }
}
