using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SpaceShooter2
{
    internal class Rocket : SpaceObject
    {
        private Ellipse rocket;
        public Rocket(Point origin, Canvas canvas)
        {
            IsHitDetectable= true;
            MovementSpeed = 15;
            rocket = new Ellipse();
            Height = 50;
            Width= 10;
            rocket.Height = Height;
            rocket.Width = Width;
            rocket.Fill = Brushes.Red;
            Parent = canvas;
            Place = origin;
            Canvas.SetBottom(rocket, Place.Y + Height);
            Canvas.SetLeft(rocket, Place.X);
            Parent.Children.Add(rocket);
        }
        public Rocket()
        {

        }
        public override Ellipse GetEllipse()
        {
            return rocket;
        }
        public bool MoveRocket()
        {
            Canvas.SetBottom(rocket, Canvas.GetBottom(rocket) + MovementSpeed);
            if (Canvas.GetBottom(rocket) > Parent.ActualHeight - Height)
            {
                Parent.Children.Remove(rocket);
                ToBeRemoved = true;
                return true;
            }
            return false;
        }
        public override bool HasHit(SpaceObject a)
        {
            return base.HasHit(this, a);
        }
        public override bool IsHigher(double maxHeight)
        {
            if (Parent.ActualHeight - Canvas.GetBottom(rocket) < maxHeight) return true;
            else return false;
        }
        public override bool IsLower(double minHeight)
        {
            if (Parent.ActualHeight - Canvas.GetBottom(rocket) > minHeight) return true;
            else return false;
        }
    }
}
