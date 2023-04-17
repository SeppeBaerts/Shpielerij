using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter2
{
    internal static class CanvasRows
    {
        public static CanvasRow one;
        public static CanvasRow two;
        public static CanvasRow three;
        public static CanvasRow four;
        public static CanvasRow five;
        public static CanvasRow six;
        public static List<CanvasRow> rows = new List<CanvasRow>();
        public static void Initiate()
        {
            rows.Add(one);
            rows.Add(two);
        }
        public static void Add(SpaceObject obj)
        {
            if (obj.IsLower(one.Bottom))
                two.ReceiveSpaceObject(obj);
            else 
                one.ReceiveSpaceObject(obj);
        }
        public static void Check()
        {
            foreach (CanvasRow row in rows)
                row?.CheckCollission();
        }
    }
}
