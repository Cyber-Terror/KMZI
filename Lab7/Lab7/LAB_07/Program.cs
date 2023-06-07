using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace LAB_07
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var encode = new DESCryptoEncode();
            var decode = new DESCryptoDecode();

            string text = "";
            var secret = "DIMITRIAANTON";
            var secret1 = "CDIMITRIAANTON";
            var weakKey1 = "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF";
            var weakKey2 = "000000000000000000000000000000000000000000000000";
            var weakKey3 = "111111111111111111111111111111111111111111111111";
            var semi_weakKey1 = "010101010101010101010101010101010101010101010101";
            var semi_weakKey2 = "FEFEFEFEFEFEFEFEFEFEFEFEFEFEFEFEFEFEFEFEFEFEFEFE";
            var semi_weakKey3 = "E0E0E0E0F1F1F1F1";


            Stopwatch stopwatch1 = new Stopwatch();
            Stopwatch stopwatch2 = new Stopwatch();

            //

            do
            {
                Console.WriteLine("Lab7 DES\nIf you want to QUIT press q\nPrint another text to encrypt and decrypt\nPress 'ENTER' to use default text");
                text = Console.ReadLine();
                Console.Clear();

                if (text.StartsWith(" ") || text.Length == 0)
                {
                    text = "DIMITRIADI ANTON VLADIMIROVICH";
                }

                Console.WriteLine($"Original text : {text}");
                stopwatch1.Start();
                var crypted = encode.Encode(text, secret);
                var crypted1 = encode.Encode(text, semi_weakKey2);
                
                Console.WriteLine($"After encoding : {crypted}");
                Console.WriteLine($"After encoding weakKey : {crypted1}");
               // File.WriteAllText("C:\\University_work\\3k2sem\\Cryptography\\Lab7\\Lab7\\text.txt", text);
                // Console.WriteLine($"After encoding : {crypted1}");
                stopwatch1.Stop();
                Console.WriteLine("Time encr:" + stopwatch1.ElapsedMilliseconds + " ms");
                stopwatch2.Start();
                var decoded = decode.Decode(crypted, secret);
                Console.WriteLine($"After decoding : {decoded}");
                stopwatch2.Stop(); 
                Console.WriteLine("Time encr:" + stopwatch2.ElapsedMilliseconds + " ms");
               Console.WriteLine("Count change bits="+ LavinEffect(crypted,crypted1));
            }
            while (text != "q");
        }
        public static int LavinEffect(string encr1, string encr2)
        {
            var changedBits = 0;

            for (int i = 0; i < encr1.Length; i++)
            {
                var text1 = encr1[i];
                var text2 = encr2[i];

                int xor = text1 ^ text2;
                while (xor != 0)
                {
                    if ((xor & 1) == 1)
                        changedBits++;
                    xor >>= 1;
                }
            }

            return changedBits;
        }
    }


}
