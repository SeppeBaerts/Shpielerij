using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oefening1
{
    internal class Oefening1
    {
        static void Main(string[] args)
        {
            string[] begroeting =
            {
                "Goedenmorgen",
                "Goedenmiddag",
                "Goedenavond"
            };
            Console.Write("Naam:");
            using (StreamWriter sw = new StreamWriter("groet.txt"))
                sw.WriteLine($"{begroeting[GetTime()]} {Console.ReadLine()}");
        }
        static int GetTime()
        {
            if (DateTime.Now.Hour < 12)
                return 0;
            else if (DateTime.Now.Hour < 16)
                return 1;
            else
                return 2;
        }
    }
}
