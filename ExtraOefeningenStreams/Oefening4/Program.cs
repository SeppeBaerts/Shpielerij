using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Oefening4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("*************************");
            sb.Append("      Zoek een naam in mensen.txt      ");
            sb.Append("*************************");
            Console.WriteLine(sb.ToString());
            Console.WriteLine("Searching for mensen.txt");
            FileInfo fi = new FileInfo(LocalValues.FilePath);
            //Console.WriteLine(fi.Exists? "File found! :D" : $"Something went wrong, please check if {fi.FullName} exists.");
            if (fi.Exists)
            {
                Console.WriteLine("Welke naam zoek je?");
                try
                {
                    List<string> list = LocalValues.FindFirstNames(Console.ReadLine());

                Console.WriteLine("--------------------------------------");
                if (list.Count > 0)
                    foreach (string s in list)
                        Console.WriteLine($"-{s}");
                else
                    CreatePeople();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
                CreatePeople();
            Console.WriteLine("Want to search again? (y/n)");
            if (Console.ReadLine().ToLower() == "y") Main(args);
        }
        static void CreatePeople(int howMany = 5)
        {
            Console.WriteLine($"Not enough people in file, please Create {howMany} more people");
            for(int i = 0; i < howMany; i++)
            {
                Console.WriteLine($"Person {i + 1}");
                Console.WriteLine("----------------------------");
                LocalValues.AddPerson();
            }
        }
    }
}
