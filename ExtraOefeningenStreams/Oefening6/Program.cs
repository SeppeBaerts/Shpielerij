using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oefening6
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
                sb.Append("      Zijde gij wel ouder?      ");
                sb.Append("*************************");
                Console.WriteLine(sb.ToString());
                Console.WriteLine(); Console.WriteLine("Voornaam");
                string voornaam = Console.ReadLine();
                Console.WriteLine(); Console.WriteLine("Achternaam");
                bool? something = LocalValues.IsOlder(voornaam, Console.ReadLine());
                if (something != null)
                    Console.WriteLine((bool)something ? "Ja" : "Nee");
                else
                    Console.WriteLine("Person not found");
                Console.WriteLine();
                Console.WriteLine("Want to go again? (y/n)");
            } while (Console.ReadLine().ToLower() == "y");

        }
    }
}
