using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oefening5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                StringBuilder sb = new StringBuilder();
                sb.Append("*************************");
                sb.Append("      Krijg een random naam      ");
                sb.Append("*************************");
                Console.WriteLine(sb.ToString());
                Console.WriteLine(RandomNaam());
                Console.WriteLine();
                Console.WriteLine("Want another go?(y/n)");
            } while (Console.ReadLine().ToLower() == "y");

            
        }
        static string RandomNaam()
        {
            List<string[]> peeps = LocalValues.People;
            return peeps[new Random().Next(0, peeps.Count)][0];
        }
    }
}
