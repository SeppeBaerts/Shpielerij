using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibEncryption
{
    public static class EncryptionLayer
    {
        public static string EncryptionReference { get; set; }
        private static string EncryptionKey { get; set;}
        private static int encryptionKeyLength = 24;
        private static char[] characters = "abcdefghijklmopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 /*-+".ToCharArray();


        public static string EncryptMessage(string message)
        {
            //throw new NotImplementedException();
            if (EncryptionKey == null)
                CreateEncryptionKey();
            StringBuilder encryptedMessage = new StringBuilder();
            int encryptionStringPlace = 0;
            char[] toEncryptChars = message.ToCharArray();
            for (int i = 0; i < toEncryptChars.Length; i++)
            {
                if (encryptionStringPlace >= EncryptionKey.Length)
                    encryptionStringPlace = 0;
                encryptedMessage.Append(EncryptionReference[Math.Abs(EncryptionReference.IndexOf(EncryptionKey[encryptionStringPlace]) - EncryptionReference.IndexOf(toEncryptChars[i]))]);
                encryptionStringPlace++;
            }
            return encryptedMessage.ToString();
        }

        public static string DecryptMessage(string message)
        {
            StringBuilder decryptedMessage = new StringBuilder();
            int encryptionStringPlace = 0;
            char[] toDecryptChars = message.ToCharArray();
            for (int i = 0; i < toDecryptChars.Length; i++)
            {
                //moet nog aan gewerkt worden
                //okey dus --> wanneer je een negatief getal hebt zou je eigenlijk bij de decryption de waardes moeten optellen
                //Je pakt de absolute waarde van 2 getallen min elkaar, en dat werkt. Maar stel dat je D(als encryptionKey; waarde: 5)  en C(als messageWaarde; waarde 8) hebt,
                //dan doe je 5-8, dit komt neer op -3; hier nemen we de absolute waarde van --> 3; en dan pakken we het 3e getal uit de sequence. Maar vanaf het moment dat je dit wilt
                //decrypten, kom je op 3-5: -2 of 5-3: 2; dit is dus niet de messageWaarde van 8 die je zou willen.
                if (encryptionStringPlace >= EncryptionKey.Length)
                    encryptionStringPlace = 0;
                int firstNumber = EncryptionReference.IndexOf(EncryptionKey[encryptionStringPlace]);
                int secondNumber = EncryptionReference.IndexOf(toDecryptChars[i]);
                int encryptionKeyPlace = Math.Abs( secondNumber - firstNumber);
                decryptedMessage.Append(EncryptionReference[encryptionKeyPlace]);
                encryptionStringPlace++;
            }
            return decryptedMessage.ToString();
        }
        private static void CreateEncryptionKey()
        {
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < characters.Length; i++)
            {
                char c = characters[rnd.Next(0, characters.Length)];
                while (sb.ToString().Where(k => k == c).Count() != 0)
                    c = characters[rnd.Next(0, characters.Length)];
                sb.Append(c);
            }
            EncryptionReference = sb.ToString();
            sb.Clear();
            for (int i = 0; i < encryptionKeyLength; i++)
                sb.Append(characters[rnd.Next(0, characters.Length)]);
            EncryptionKey = sb.ToString();
            sb = null;
        }
        public static string TempCreateKey()
        {
            CreateEncryptionKey();
            return EncryptionKey;
        }
    }
}
