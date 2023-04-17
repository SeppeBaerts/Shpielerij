using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oefening2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("*************************");
            sb.Append("       WIPE METHOD     ");
            sb.Append("*************************");
            Console.WriteLine(sb.ToString());
            Console.Write(@"Geef een bestandsNaam, typ ""close"" to stop this program ");
            string content = Console.ReadLine();
            if (content.ToLower() != "close")
            {
                FileInfo fi = new FileInfo(content.Trim('"'));
                Console.WriteLine($"Bent u zeker dat u {fi.FullName} wilt verwijderen? (y/n)");
                if (Console.ReadLine().ToLower() == "y")
                    Wipe(fi);
            }
        }
        static void Wipe(FileInfo fi)
        {
            using (StreamWriter sw = new StreamWriter(fi.FullName)) ;
            Console.WriteLine($"inhoud verwijderd van {fi.FullName}");
            Console.ReadLine();

        }
    }
}
