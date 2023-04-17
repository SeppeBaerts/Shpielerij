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
    internal class Bird
    {
        public double JumpDistance { get; set; }
        public double FallDistance { get; set; }
        Ellipse bird;
        ImageBrush brush = new ImageBrush();
        ImageSourceConverter converter = new ImageSourceConverter();
        public Canvas MainCanvas { get; set; }
        public double LeftPosition { get; private set; }
        public double Width { get; private set; }
        public bool IsDead { get; set; }
        public Bird(double jumpDistance, double fallDistance,  Canvas mainCanvas)
        {
            brush.ImageSource = (ImageSource)converter.ConvertFromString(@"C:\Users\12202625\offline documenten\Shpielerij\CSharpShpielerij\flappy bird\flappy bird\pngegg.png");
            brush.Viewport = new Rect(-0.12, -0.38, 1.28, 1.68);
            brush.Stretch = Stretch.Fill;
            LeftPosition = 80;
            Width = 35;
            JumpDistance = jumpDistance;
            bird = new Ellipse();
            bird.Width = Width;
            bird.Fill = brush;
            bird.Height = bird.Width;
            MainCanvas = mainCanvas;
            Canvas.SetBottom(bird, 255);
            Canvas.SetLeft(bird, LeftPosition);
            MainCanvas.Children.Add(bird);
            FallDistance = fallDistance;
        }
        public void Jump()
        {
            if (MainCanvas != null)
                Canvas.SetBottom(bird, Canvas.GetBottom(bird) + JumpDistance);
        }
        public void Fall()
        {
            if (MainCanvas != null && Canvas.GetBottom(bird) > 0)
                Canvas.SetBottom(bird, Canvas.GetBottom(bird) - FallDistance);
        }
        public void CheckDead(Pipe pipe)
        {
            //bird.Fill = Brushes.Orange;
            IsDead = Canvas.GetBottom(bird) < pipe.GetLowerLocation() || Canvas.GetBottom(bird) + bird.Height > pipe.GetUpperLocation();
        }
        public void Delete()
        {
            MainCanvas.Children.Remove(bird);
        }
        public void ChangeColor()
        {
            //bird.Fill = IsDead ? Brushes.Green : Brushes.Red;
        }
    }
}
