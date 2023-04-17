using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace SpaceShooter2
{
    internal class Rock : SpaceObject
    {
        private Ellipse rock;
        bool isUp; //Gebruik deze voor in de lijst te zetten; zo moet je minder dingen controleren
        bool isLeft;

        public Rock(double movementSpeed, double width, double height, Canvas canvas, Point location)
        {
            IsHitDetectable = true;
            rock = new Ellipse();
            Width = width;
            Height = height;
            rock.Width = Width;
            rock.Height = Height;
            rock.Fill = Brushes.White;
            MovementSpeed= movementSpeed;
            Place = location;
            Parent = canvas;
            Canvas.SetLeft(rock, Place.X);
            Canvas.SetTop(rock, Place.Y);
            Parent.Children.Add(rock);
        }
        /// <summary>
        /// JUST FOR TEST PURPOSES
        /// </summary>
        public Rock(Canvas canvas) 
        {
            IsHitDetectable = true;
            rock = new Ellipse();
            Width = 25;
            Height = 25;
            rock.Width = Width;
            rock.Height = Height;
            rock.Fill = Brushes.White;
            MovementSpeed = 2;
            Place = new Point(new Random().Next((int)canvas.ActualWidth), -50);
            Parent = canvas;
            Canvas.SetLeft(rock, Place.X);
            Canvas.SetTop(rock, Place.Y);
            Parent.Children.Add(rock);
        }
        public Rock()
        {

        }

        /// <summary>
        /// Looks if this space object has collided with another space object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool HasHit(SpaceObject obj)
        {
            return base.HasHit(this, obj);
        }
        /// <summary>
        /// Moves the Rock downwards with the amount of movement speed
        /// </summary>
        public void FallDown()
        {
            Canvas.SetTop(rock, Canvas.GetTop(rock)+MovementSpeed);
            if (Canvas.GetTop(rock) > Parent.ActualHeight) 
                ToBeRemoved= true;
        }
        public override Ellipse GetEllipse()
        {
            return rock;
        }
        public override bool IsLower(double minHeight)
        {
            if (Canvas.GetTop(rock) > minHeight) return true;
            else return false;
        }
    }
}
