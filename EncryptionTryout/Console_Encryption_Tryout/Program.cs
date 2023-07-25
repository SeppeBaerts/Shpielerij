using ClassLibEncryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Encryption_Tryout
{
    internal class Program
    {
        static string[,] settings = {
            {"Cre -K", "Create key, This will Create and give you a private key." },
            {"Enc", "This will prompt you to create a message to encrypt. If you haven't made an encryption key, your system will do it for you."},
            {"Decr","This will prompt you to Paste a message to Decrypt. If you have changed your private key, this will no longer work and you lost the information for ever." }
        };
        static void Main(string[] args)
        {
            Console.Write(">");
            switch (Console.ReadLine())
            {
                case "Cre -K":
                    CreateKey();
                    break;
                case "Enc":
                    EncryptMessage();
                    break;
                case "Decr":
                    DecryptMessage();
                    break;
                case "help":
                    WriteHelp();
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
            Main(args);

        }
        static void DecryptMessage()
        {
            Console.Write("Message to Decrypt: ");
            Console.WriteLine($"Decrypted message: {EncryptionLayer.DecryptMessage(Console.ReadLine())}");
        }
        static void EncryptMessage()
        {
            Console.Write("Message to encrypt: ");
            Console.WriteLine("Encrypted Message: " + EncryptionLayer.EncryptMessage(Console.ReadLine()));
        }
        static void CreateKey()
        {
            Console.WriteLine(EncryptionLayer.TempCreateKey());
        }
        static void WriteHelp()
        {
            for(int i = 0; i < settings.GetLength(0); i++)
                Console.WriteLine($"{settings[i, 0] + ':',-25} {settings[i, 1]}");
        }
    }
}
