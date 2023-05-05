using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameEngineLib
{
    public class GameItemGameOver : GameItem
    {
        public GameItemGameOver(double left, double top, int width, int height) : base(left, top, width, height, 0, true)
        {
            ObjectElement = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = LevelController.IsCreateWindow ? Brushes.Gray : Brushes.Transparent,
                ContextMenu = CMenu,
            };
            ObjectElement.MouseDown += ObjectElement_MouseDown;
            TemporaryStorage.GameOverRect = OutlineRect;
        }
        public override string ToString()
        {
            return $"XX;{base.ToString()}";
        }
        private void ObjectElement_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SetOutline();
            SelectedObject.ChangeGameItem(this);
        }
    }
}
