using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Toepassing19_Ilist
{
    internal interface Modules
    {
        void Add(string text);
        int IndexOf(string text);
        void Remove(string text);
    }
}
