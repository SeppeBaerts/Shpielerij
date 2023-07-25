using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ClassLibAnimation
{
    public class AnimationFade : Animation<double>
    {
        public AnimationFade(int time, UIElement animationObject) : base(time, animationObject)
        {
            oldValue = animationObject.Opacity;
            targetValue = animationObject.Opacity > 0.5 ? 0 : 1;
            step = (targetValue - oldValue) / (FPS * Time);
        }
        public override void Start()
        {
            oldValue = AnimationObject.Opacity;
            targetValue = AnimationObject.Opacity > 0.5 ? 0 : 1;
            step = (targetValue - oldValue) / (FPS * Time);
            base.Start();
        }
        internal override void Clock_Tick(object sender, EventArgs e)
        {
            clock.Stop();
            if (AnimationObject.Opacity != targetValue)
            {
                AnimationObject.Opacity += step;
                clock.Start();
            }
            if (AnimationObject.Opacity < 0 || AnimationObject.Opacity > 1)
                AnimationObject.Opacity = AnimationObject.Opacity < 0 ? 0 : 1;
        }
    }
}
