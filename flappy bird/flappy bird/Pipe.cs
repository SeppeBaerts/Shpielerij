using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace flappy_bird
{
    internal class Pipe
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public double LocationX { get; set; }
        public bool WentOver { get; set; }
        Rectangle upperPipe;
        Rectangle lowerPipe;
        Canvas MainCanvas { get; set; }
        public Pipe(double height, double width, Canvas mainCanvas)
        {
            Height = height;
            Width = width;
            MainCanvas = mainCanvas;
            upperPipe = new Rectangle
            {
                Width = Width,
                Height = Height,
                Fill = Brushes.Blue
            };
            lowerPipe = new Rectangle
            {
                Width = Width,
                Height = MainCanvas.ActualHeight - (Height + 150),
                Fill= Brushes.Blue
            };
            Canvas.SetTop(upperPipe, 0);
            Canvas.SetBottom(lowerPipe, 0);
            Canvas.SetLeft(upperPipe, MainCanvas.ActualWidth + width);
            Canvas.SetLeft(lowerPipe, MainCanvas.ActualWidth + width);
            MainCanvas.Children.Add(upperPipe);
            MainCanvas.Children.Add(lowerPipe);
        }
        public void Update()
        {
            Canvas.SetLeft(upperPipe, Canvas.GetLeft(upperPipe) - LocalSettings.Speed);
            Canvas.SetLeft(lowerPipe, Canvas.GetLeft(lowerPipe) - LocalSettings.Speed);
        }
        public double GetLeftLocation()
        {
            return Canvas.GetLeft(lowerPipe);
        }
        public double GetUpperLocation()
        {
            return MainCanvas.ActualHeight - Height;
        }
        public double GetLowerLocation()
        {
           return MainCanvas.ActualHeight - (Height + 150);
        }
        public void Delete()
        {
            MainCanvas.Children.Remove(upperPipe);
            MainCanvas.Children.Remove(lowerPipe);
        }
    }
}
