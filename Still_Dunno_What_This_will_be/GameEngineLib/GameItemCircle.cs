using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameEngineLib
{
    public class GameItemCircle : GameItem
    {
        public GameItemCircle(double left, double top, int width, int height,bool canBePickedUp = false, bool colDetection = true) : base(left, top, width, height,canBePickedUp, 5, colDetection)
        {
            ObjectElement = new Ellipse
            {
                Width = width,
                Height = height,
                Fill = Brushes.Red,
                ContextMenu = CMenu,
            };
            ObjectElement.MouseLeftButtonDown += ObjectElement_MouseDown;
        }

        public override string ToString()
        {
            return $"OO;{base.ToString()}";
        }
        private void ObjectElement_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SetOutline();
            SelectedObject.ChangeGameItem(this);
        }
        public override void SetLocation(int x, int y)
        {
            Canvas.SetLeft(ObjectElement, x);
            Canvas.SetTop(ObjectElement, y);
        }
        internal override void Dup_Click(object sender, RoutedEventArgs e)
        {
            GameItemCircle circle = new GameItemCircle(ActualLeft+50, ActualTop+50, Width, Height);
            circle.Parent = Parent;
        }

    }
}
