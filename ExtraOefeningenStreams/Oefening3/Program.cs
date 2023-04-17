using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Oefening3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb= new StringBuilder();
            sb.Append("*************************");
            sb.Append("       Read Name And Bday      ");
            sb.Append("*************************");
            Console.WriteLine(sb.ToString());
            Console.Write("Start Writing? (y/n)");
            if (Console.ReadLine().ToLower() == "y")
            {
                CreatePerson()
            }
        }
        public static void CreatePerson()
        {
            using (StreamWriter sw = new StreamWriter("mensen.txt", true))
            {
                Console.Write("Voornaam: ");
                string voorn = Console.ReadLine();
                Console.Write("Achternaam: ");
                string achternaam = Console.ReadLine();
                Console.Write("Leeftijd: ");
                sw.WriteLine($"{(voorn.Contains('-') ? "Ongeldig" : voorn)}-{(achternaam.Contains('-') ? "Ongeldig" : achternaam)}-{(byte.TryParse(Console.ReadLine(), out byte leeftijd) ? leeftijd.ToString() : "Ongeldig")}");
            }
        }
    }
}
