using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace SpaceShooter2
{
    internal class Ship : SpaceObject
    {
        Rectangle rect;
        List<Rocket> shootingRockets;
        List<Rocket> toRemoveRockets;
        double maxCanvasWidth; //eigenlijk moet dit helemaal niet
        double maxCanvasHeight;
        bool hasShotLeft;
        public Ship(double movementSpeed, double width, double height, Canvas canvas, Point location, double canvasHeight, double canvasWidth)
        {
            toRemoveRockets = new List<Rocket>();
            shootingRockets= new List<Rocket>();
            IsHitDetectable = true;
            MovementSpeed = movementSpeed;
            maxCanvasHeight = canvasHeight;
            maxCanvasWidth = canvasWidth;
            rect = new Rectangle();
            Width = width;
            Height = height;
            rect.Width = Width;
            rect.Height = Height;
            rect.Fill = Brushes.White;
            Place = location;
            Canvas.SetLeft(rect, Place.X);
            Canvas.SetTop(rect, Place.Y);
            Parent = canvas;
            Parent.Children.Add(rect);
        }
        /// <summary>
        /// Use only for Testing purposes
        /// </summary>
        /// <param name="canvas"></param>
        public Ship(Canvas canvas)
        {
            toRemoveRockets = new List<Rocket>();
            shootingRockets = new List<Rocket>();
            IsHitDetectable = true;
            MovementSpeed = 15;
            maxCanvasHeight = 450;
            maxCanvasWidth = 800;
            rect = new Rectangle();
            Width = 80;
            Height = 40;
            rect.Width = Width;
            rect.Height = Height;
            rect.Fill = Brushes.White;
            Place = new Point(maxCanvasWidth/2, 15);
            Canvas.SetLeft(rect, Place.X);
            Canvas.SetBottom(rect, Place.Y);
            Parent = canvas;
            Parent.Children.Add(rect);
        }
        #region Movement
        //Dit kan in principe ook met een boolean en in een method, maar dit maakt het makkelijker om te lezen.
        public void MoveLeft()
        {
            if (Canvas.GetLeft(rect) - MovementSpeed > 0)
                Canvas.SetLeft(rect, Canvas.GetLeft(rect) - MovementSpeed);
        }
        public void MoveRight()
        {
            if (Canvas.GetLeft(rect) + MovementSpeed < (Parent.ActualWidth - Width))
                Canvas.SetLeft(rect, Canvas.GetLeft(rect) + MovementSpeed);
        }
        #endregion
        #region Shooting
        public void ShootRocket()
        {
            Rocket rock = new Rocket(new Point(Canvas.GetLeft(rect) + (hasShotLeft? 0 : Width), Canvas.GetBottom(rect)), Parent);
            hasShotLeft = !hasShotLeft;
            CanvasRows.Add(rock);
            shootingRockets.Add(rock);
        }

        public void MoveRockets()
        {
            if (shootingRockets.Count > 0)
                foreach (Rocket bullet in shootingRockets)
                {
                    if (bullet.MoveRocket()|| bullet.ToBeRemoved)
                        toRemoveRockets.Add(bullet);
                }
            if(toRemoveRockets.Count > 0)
                foreach(Rocket bullet in toRemoveRockets)
                    shootingRockets.Remove(bullet);

            toRemoveRockets.Clear();
        }
        #endregion

    }
}
