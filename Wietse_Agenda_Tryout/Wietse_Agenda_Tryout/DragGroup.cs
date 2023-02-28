using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Wietse_Agenda_Tryout
{
    internal class DragGroup
    {
        public string Vak { get; set; }
        public List<DragBox> Children { get; set; }
        = new List<DragBox>();
        private WrapPanel wrap;
        public DragGroup(string vak, Canvas canvas)
        {
            Vak = vak;
            wrap = new WrapPanel
            {
                Orientation = Orientation.Vertical,
            };
            GroupBox box = new GroupBox
            {
                Width = 100,
                Height = 300,
                Content = wrap,
                Header = Vak
            };
            Canvas.SetLeft(box, 50);
            Canvas.SetTop(box, 50);
            canvas.Children.Add(box);
        }
        public void AddChild(DragBox child) 
        {
            Children.Add(child);
            wrap.Children.Add(child.GetTextBlock());
        }

    }
}
