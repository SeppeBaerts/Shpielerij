using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oefening8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            do
            {
                Console.Clear();
                sb.Clear();
                sb.Append("*************************");
                sb.Append("      Ik wil je NOOIT meer zien, laat mij geeee-rust....      ");
                sb.Append("*************************");
                Console.WriteLine(sb.ToString());
                Console.WriteLine(); Console.WriteLine("Voornaam");
                string voornaam = Console.ReadLine();
                Console.WriteLine(); Console.WriteLine("Achternaam");
                LocalValues.DeleteFirst(voornaam, Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("Want to go again? (y/n)");
            } while (Console.ReadLine().ToLower() == "y");
        }
    }
}
