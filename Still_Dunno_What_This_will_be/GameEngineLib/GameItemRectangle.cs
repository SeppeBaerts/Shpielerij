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
    public class GameItemRectangle : GameItem
    {
        public GameItemRectangle(double left, double top, int width, int height, bool canBePickedUp, bool colDetection = true, int movementSpeed = 5, int movementAmount = 0) : base(left, top, width, height, canBePickedUp, movementSpeed, colDetection, movementAmount)
        {
            ObjectElement = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = Brushes.Red,
                ContextMenu = CMenu,
            };
            ObjectElement.MouseDown += ObjectElement_MouseDown;
        }

        private void ObjectElement_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SetOutline();
            SelectedObject.ChangeGameItem(this);
        }
        public override string ToString()
        {
            return $"--;{base.ToString()}";
        }
        public override void SetLocation(int x, int y)
        {
            Canvas.SetLeft(ObjectElement, x);
            Canvas.SetTop(ObjectElement, y);
        }
        internal override void Dup_Click(object sender, RoutedEventArgs e)
        {
            GameItemRectangle rectie = new GameItemRectangle(ActualLeft+15, ActualTop+15, Width, Height, CanBePickedUp, true, MovementSpeed, MovingAmount);
            rectie.Parent = Parent;
        }
    }
}
