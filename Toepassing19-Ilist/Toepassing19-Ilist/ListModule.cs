using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Toepassing19_Ilist
{
    internal class ListModule : Modules
    {
        private int aantal = 0;
        private ListBox modules;

        public List<string> Inhoud { get; set; }
        public ListModule(ListBox listBox)
        {
            modules = listBox;
            Inhoud = new List<string> { };
        }

        public void Add(string text)
        {
            aantal++;
            Inhoud.Add(text);
            modules.Items.Add(text);
        }

        public int IndexOf(string text)
        {
            return Inhoud.IndexOf(text);
        }

        public void Remove(string text)
        {
            RemoveAt(IndexOf(text));
        }
        public void RemoveAt(int index)
        {
            aantal--;
            modules.Items.RemoveAt(index);
            Inhoud.RemoveAt(index);
        }
    }
}
