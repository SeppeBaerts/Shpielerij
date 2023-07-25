using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ClassLibAnimation
{
    public class Animation<T>
    {
        /// <summary>
        /// Amount of time in seconds
        /// </summary>
        public int Time { get; set; }
        internal DispatcherTimer clock { get; set; }
        internal virtual int RefreshRate => 1000/FPS;
        public UIElement AnimationObject { get; set; }
        internal int FPS { get; set; }
        internal T oldValue;
        internal T targetValue;
        internal double step;
        private int animationRepeat = 1;
        internal int animationMaxRepeat;
        public Animation(int time, UIElement animationObject)
        {
            FPS = 60;
            AnimationObject = animationObject;
            Time = time;
            clock = new DispatcherTimer();
            clock.Interval = new TimeSpan(0, 0, 0, 0, RefreshRate);
            clock.Tick += Clock_Tick;
        }

        internal virtual void Clock_Tick(object sender, EventArgs e)
        {
            if (animationRepeat != animationMaxRepeat)
            {
                clock.Start();
                animationRepeat++;
            }
            else
                animationRepeat = 1;
        }
        public virtual void Start()
        {
            clock?.Start();
        }
        public virtual void Stop() 
        { 
            clock?.Stop();
        }

    }
}
