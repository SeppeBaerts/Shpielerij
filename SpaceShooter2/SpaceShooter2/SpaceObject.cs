using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace SpaceShooter2
{
    internal class SpaceObject : IDisposable
    {
        public bool IsHitDetectable { get; set; }
        public bool ToBeRemoved { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double MovementSpeed { get; set; }
        public Point Place { get; set; }
        public Canvas Parent { get; set; }

        public void Dispose()
        {
            Width= 0;
            Height= 0;
            Parent = null;
        }
        public virtual bool IsHigher(double maxHeight) { return false; }
        public virtual bool IsLower(double minHeight) { return false; }

        public virtual Ellipse GetEllipse()
        {
            return null;
        }

        public virtual bool HasHit(SpaceObject a)
        {
            return false;
        }
        public static bool SameType (SpaceObject a, SpaceObject b)
        {
            if (a is Rocket && b is Rocket || a is Rock && b is Rock) return true;
            else return false;
        }
        /// <summary>
        /// Looks if this space object has collided with another space object.
        /// </summary>
        /// <param name="a">first object</param>
        /// <param name="b">second object</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual bool HasHit(SpaceObject a, SpaceObject b)
        {
            //This is broken, hit detection left works, i think, but above and below do not.
            Ellipse ellipseA = new Ellipse();
            Ellipse ellipseB = new Ellipse();
            Rocket mRocket = new Rocket();
            Rock mRock = new Rock();
            if (a is Rocket rocket && b is Rock rock) 
            {
                 mRocket = rocket;
                mRock = rock;
                 ellipseA = rocket.GetEllipse();
                 ellipseB = rock.GetEllipse();
            }
            else if(a is Rock rock1 && b is Rocket rocket1)
            {
                mRocket = rocket1;
                mRock = rock1;
                ellipseA = rocket1.GetEllipse();
                ellipseB = rock1.GetEllipse();
            }
            if (a.IsHitDetectable && b.IsHitDetectable)
            {
                if (Canvas.GetLeft(ellipseA) <= Canvas.GetLeft(ellipseB) + b.Width && Canvas.GetLeft(ellipseA) + a.Width >= Canvas.GetLeft(ellipseB)) 
                {
                    if (Parent.ActualHeight - Canvas.GetBottom(ellipseA) - mRocket.Height <= Canvas.GetTop(ellipseB) + mRock.Height)
                        return true;
                    else return false;
                }
                return false;
            }
            else
                return false;
        }
    }
}
