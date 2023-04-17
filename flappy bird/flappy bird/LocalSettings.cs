using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flappy_bird
{
    internal static class LocalSettings
    {
        public static double Speed { get {
                return RefreshRate / (12 * RelativeRefresh);
            } }
        public static double RelativeRefresh { get
            {
                return RefreshRate / 60;
            } }
        public static double RefreshRate { get; set; }
        public static TimeSpan FramesPerSecond => new TimeSpan(0, 0, 0, 0, (int)(1000 / RefreshRate));
        public static double RelativeRefreshRate(double amount)
        {
            double relativeAmount = 60 / amount;
            return RefreshRate / (relativeAmount * RelativeRefresh);
        }
    }
}
