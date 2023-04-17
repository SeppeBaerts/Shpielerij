using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class LocalValues
{
    public static string FilePath = (@"C:\Users\12202625\offline documenten\Shpielerij\CSharpShpielerij\ExtraOefeningenStreams\Oefening3\bin\Debug\mensen.txt");
    static bool isUpdated;
    static List<string[]> peeps = new List<string[]>();
    public static List<string[]> People {
        get
        {
            if (!isUpdated)
            {
                peeps.Clear();
                using (StreamReader sr = new StreamReader(FilePath))
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (!string.IsNullOrWhiteSpace(line))
                            peeps.Add(line.Split('-'));
                    }
                isUpdated = true;
            }
            return peeps;
        }    
    }
    public static void AddPerson()
    {
        Console.Write("Voornaam: ");
        string voorn = Console.ReadLine();
        Console.Write("Achternaam: ");
        string achternaam = Console.ReadLine();
        Console.Write("Leeftijd: ");
        using (StreamWriter sw = new StreamWriter(FilePath, true))
            sw.WriteLine($"{(voorn.Contains('-') ? "Ongeldig" : voorn)}-{(achternaam.Contains('-') ? "Ongeldig" : achternaam)}-{(byte.TryParse(Console.ReadLine(), out byte leeftijd) ? leeftijd.ToString() : "Ongeldig")}");
        Console.WriteLine();
        isUpdated = false;
    }
    public static List<string> FindFirstNames(string lastName)
    {
        List<string> names = new List<string>();
        foreach (string[] s in People)
            if (s[1].ToLower().Equals(lastName.ToLower())) 
                names.Add(s[0]);
        return names;
    }
    public static bool? IsOlder(string voornaam, string achternaam, int minAge=18)
    {
        foreach (string[] s in People)
        {
            if (s[0].ToLower() == voornaam.ToLower() && s[1].ToLower() == achternaam.ToLower() && int.TryParse(s[2], out int leeftijd) && leeftijd > minAge)
                return true;
            else if (s[0].ToLower() == voornaam.ToLower() && s[1].ToLower() == achternaam.ToLower())
                return false;
        }
        return null;
    }
    public static bool IsDuplicate(string voornaam, string achternaam)
    {
        bool isDuplicate = false;
        foreach (string[] s in People)
        {
            if (!isDuplicate)
                isDuplicate = s[0].ToLower() == voornaam.ToLower() && s[1].ToLower() == achternaam.ToLower();
            else if (s[0].ToLower() == voornaam.ToLower() && s[1].ToLower() == achternaam.ToLower())
                return true;
        }
        return false;
    }
    public static void DeleteFirst(string voornaam, string naam)
    {
        StringBuilder sb = new StringBuilder();
        long pos = 0;
        string bigString = "";
        using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite))
        {
            string[] strings;
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    bigString = sr.ReadLine();
                    strings = bigString.Split('-');
                    if (strings[0] == voornaam && strings[1] == naam)
                    {
                        pos = sb.Length-2;
                        break;
                    }
                    sb.AppendLine(bigString);
                }
            }
        }
        using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Write))
        {
            fs.Position = pos;
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine("".PadLeft(bigString.Length));
            }
        }
        //Dit is dus een manier om het te doen, Maar hi overschrijd het niet echt, het worden gewoon spaties.
        //Ge gaat een andere file moeten maken denk ik, of hier alles in een stringbuilder zetten buiten die ene string? wss iets met als strings[0]==........ && !isGeweest en dan de isgeweest naar true zetten, hierna gewoon de stringbuilder doorlopen met een else statement
        //Easy Peasy dus eigenlijk, maar you know, ik wilde weerde fancy doen.
    }
}
