using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SpaceShooter2
{
    internal class CanvasRow
    {
        //only add space objects that are hittestdetectable
        private bool isTop;
        private bool isBottom;
        public Canvas TargetCanvas { get; set; }
        public double Top { get; set; }
        public double Bottom { get; set; }
        public CanvasRow Down { get; set; }
        public CanvasRow Up { get; set; }
        public List<SpaceObject> Objects { get; set; }

        private List<SpaceObject> movingDown = new List<SpaceObject>();
        private List<SpaceObject> movingUp = new List<SpaceObject>();
        private List<SpaceObject> removing = new List<SpaceObject>();

        public CanvasRow(Canvas canvas, double top, double bottom, CanvasRow up = null)
        {
            TargetCanvas = canvas;
            Top = top;
            Bottom = bottom;
            Up = up;
            Objects = new List<SpaceObject>();
        }
        public CanvasRow(Canvas canvas, double amountRows, CanvasRow up)
        {
            TargetCanvas = canvas;
            Top = up.Bottom;
            Bottom = Top + (canvas.ActualHeight/amountRows);
            Up = up;
            Objects = new List<SpaceObject>();
        }
        public CanvasRow(Canvas canvas, double amountRows, double top =0)
        {
            TargetCanvas = canvas;
            Top = top;
            Bottom = Top + (canvas.ActualHeight / amountRows);
            Objects = new List<SpaceObject>();
        }

        public void ReceiveSpaceObject(SpaceObject spaceObject)
        {
            Objects.Add(spaceObject);
        }

        public void ChangeSpaceObjects(SpaceObject spaceObjects, bool isDown)
        {
            if (!spaceObjects.ToBeRemoved)
            {
                if (isDown)
                    Down.ReceiveSpaceObject(spaceObjects);
                else
                    Up.ReceiveSpaceObject(spaceObjects);
            }
            Objects.Remove(spaceObjects);
        }

        public void CheckCollission()
        {
            foreach (SpaceObject spaceObject in Objects)
            {
                if (spaceObject.ToBeRemoved ) 
                    removing.Add(spaceObject);
                else
                {
                    for (int i = Objects.IndexOf(spaceObject) + 1; i < Objects.Count && !SpaceObject.SameType(spaceObject, Objects[i]); i++)
                    {
                        if (spaceObject.HasHit(Objects[i]))
                            spaceObject.ToBeRemoved = Objects[i].ToBeRemoved = true;
                    }
                    if (spaceObject.IsHigher(Top))
                        movingUp.Add(spaceObject);
                    else if (spaceObject.IsLower(Bottom))
                        movingDown.Add(spaceObject);
                }
            }
            foreach(SpaceObject spaceObj in movingUp)
                ChangeSpaceObjects(spaceObj, false);
            foreach (SpaceObject spaceObj in movingDown)
                ChangeSpaceObjects(spaceObj, true);
            foreach(SpaceObject spaceObj in removing)
            {
                Objects.Remove(spaceObj);
                TargetCanvas.Children.Remove(spaceObj.GetEllipse());
            }
            removing.Clear();
            movingDown.Clear();
            movingUp.Clear();

        }
        ///Okey dus --> 
        ///der zijn 2opties
        ///1) Je laat de 'spawner' van een spaceObject kijken in welke CanvasRow hij hoort, en hierna laat je de CanvasRowwen het eigenlijk 
        ///beslissen, dwz dat je tijdens het controleren gaat kijken of het object nog steeds in deze canvasrow hoort en zo niet, ga je die 
        ///naar de 'previousRow'/'NextRow' gaan
        ///2)je laat de 'spawner' van een speifiek spaceObject alle keren controleren waar zijn children thuis horen.
    }
}
