using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameEngineLib
{
    public class GameItemEndPoint : GameItemRectangle
    {
        public GameItemEndPoint(double left, double top, int width, int height, bool colDetection = true, int movementSpeed = 5) : base(left, top, width, height, colDetection, movementSpeed)
        {
            ((Shape)ObjectElement).Fill = Brushes.Green;
            TemporaryStorage.EndPoint = OutlineRect;
        }
        public override string ToString()
        {
            return base.ToString().Replace("--;", "EE;");
        }

    }
}
