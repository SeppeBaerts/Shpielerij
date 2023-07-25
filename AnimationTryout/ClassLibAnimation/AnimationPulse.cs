using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ClassLibAnimation
{
    public class AnimationPulse : Animation<double>
    {
        private bool gettingBigger = true;
        private double currentScale;
        /// <summary>
        /// Gives a pulsating animation using the transform scale property of a UI element.
        /// </summary>
        /// <param name="time"> amount of time in seconds.</param>
        /// <param name="animationObject">The UiElement you want to Animate</param>
        /// <param name="repeatingAmount">The amount of times your animation will run (min = 1)</param>
        /// <param name="maxScale">The highest scale that your object will go to</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public AnimationPulse(int time, UIElement animationObject, int repeatingAmount = 1, double maxScale = 2) : base(time, animationObject)
        {
            targetValue = maxScale;
            oldValue = AnimationObject.RenderTransform == Transform.Identity ? 1 : animationObject.RenderTransform.Value.M11;
            if (repeatingAmount > 0)
                animationMaxRepeat = repeatingAmount;
            else throw new ArgumentOutOfRangeException("Repeating must be a positive number.");
        }
        public override void Start()
        {
            step = (targetValue - oldValue) / (FPS * Time) *4;
            currentScale = oldValue;
            base.Start();
        }
        internal override void Clock_Tick(object sender, EventArgs e)
        {
            clock.Stop();
            if ((currentScale <= targetValue && gettingBigger) || (currentScale >= 1 && !gettingBigger))
            {
                currentScale += gettingBigger ? step : -step;
                AnimationObject.RenderTransform = new ScaleTransform(currentScale, currentScale);
                clock.Start();
            }
            else if (gettingBigger)
            {
                currentScale = targetValue;
                gettingBigger = false;
                clock.Start();
            }
            else
            {
                currentScale = oldValue;
                gettingBigger = true;
                AnimationObject.RenderTransform = new ScaleTransform(1, 1);
                base.Clock_Tick(sender, e);
            }
        }
        public override void Stop()
        {
            base.Stop();
        }
    }
}
