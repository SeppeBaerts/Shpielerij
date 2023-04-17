using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamWriter_Console_App_PWP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (StreamWriter sw = new StreamWriter("node.txt"))
            {
                foreach (string arg in args)
                    sw.Write($"{arg} ");
                sw.WriteLine();

                string tekst = Console.ReadLine();
                while (tekst.ToLower() != "exit")
                {
                    sw.WriteLine(tekst);
                    tekst = Console.ReadLine();
                }
                
            }
        }
    }
}
