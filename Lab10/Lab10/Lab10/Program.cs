using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    class Program
    {
        static void Main(string[] args)
        {
          //  Request1();
            RSA rsa = new RSA();
            string text = "DIMITRIADI ANTON VLADIMIROVICH";
            Console.WriteLine($"Text: {text}");
            var stopwatch = Stopwatch.StartNew();
            BigInteger[] encryptedTextRSA = rsa.Encrypt(text);
            stopwatch.Stop();
            Console.WriteLine($"Encrypted text: {encryptedTextRSA.Select(el => el.ToString()).Aggregate((prev, current) => prev + " " + current)}");
            Console.WriteLine($"Encryption time: {stopwatch.ElapsedMilliseconds} ticks");
            stopwatch.Restart();
            Console.WriteLine($"Decrypted text: {rsa.Decrypt(encryptedTextRSA)}");
            stopwatch.Stop();
            Console.WriteLine($"Decryption time: {stopwatch.ElapsedMilliseconds} ticks");

            Console.WriteLine();

            ElGamal elGamal = new ElGamal();
            Console.WriteLine($"Text: {text}");
            stopwatch.Restart();
            BigInteger[,] encryptedTextElGamal = elGamal.Encrypt(text);
            stopwatch.Stop();
            Console.WriteLine($"Encrypted text: {string.Join(" ", encryptedTextElGamal.Cast<BigInteger>())}");
            stopwatch.Restart();
            Console.WriteLine($"Encryption time: {stopwatch.ElapsedMilliseconds} ticks");
            Console.WriteLine($"Decrypted text: {elGamal.Decrypt(encryptedTextElGamal)}");
            stopwatch.Stop();
            Console.WriteLine($"Decryption time: {stopwatch.ElapsedMilliseconds} ticks");
            Console.ReadKey();
        }
        public static void Request1()
        {
            BigInteger a = new BigInteger(15);
            var x = 250000;
            BigInteger n;
            string N = "100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            BigInteger.TryParse(N, out n);
            Stopwatch stpwch = new Stopwatch();
            for (int i = 0; i < 8; i++)
            {
                stpwch.Reset();
                stpwch.Start();
                var y = BigInteger.Pow(a,x) % n;
                //Console.WriteLine(y);
                stpwch.Stop();
                Console.WriteLine($" x {x} time {stpwch.ElapsedMilliseconds}");
                x += 250000;

            }
        }
    }
    
}
