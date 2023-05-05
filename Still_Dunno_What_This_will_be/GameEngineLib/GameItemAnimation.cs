using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GameEngineLib
{
    public class GameItemAnimation
    {
        /// <summary>
        /// The amount of times an animation loops
        /// </summary>
        private static int smoothness = 50;
        public static List<object> changingValues = new List<object>();
        public GameItem gameItem { get; set; }
        private Shape targetShape { get; set; }
        public GameItemAnimation(GameItem target)
        {
            gameItem = target;
            targetShape = target.ObjectElement as Shape;
        }
        public void Dissepear(UIElement ui, int seconden = 5)
        {
            seconden = (seconden *1000)/ smoothness;
            changingValues.Add(ui.Opacity);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval += new TimeSpan(0, 0, 0, 0, seconden);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

        }
    }
}
