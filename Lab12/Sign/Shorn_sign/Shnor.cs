using System;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Numerics;

namespace ShnorDS
{
    class Program
    {
        static void Main()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Console.WriteLine("\nЭЦП по Шнорру\n");

            BigInteger p = 2267;
            BigInteger q = 103;

            string text = File.ReadAllText(".\\text.txt");
            BigInteger g = 354;
            BigInteger obg = 967;
            int x = 30;

            BigInteger y = BigInteger.ModPow(obg, x, p);
            BigInteger a = BigInteger.ModPow(g, 13, p);

            using (var md5 = MD5.Create())
            {
                var hashBytes = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(text + a));
                BigInteger hash = new BigInteger(hashBytes);
                hash = BigInteger.Abs(hash);

                File.WriteAllText(".\\True.txt", hash.ToString());
                BigInteger b = (13 + x * hash) % q;
                BigInteger dov = BigInteger.ModPow(g, b, p);
                BigInteger X = (dov * BigInteger.ModPow(y, hash, p)) % p;

                var hashBytes2 = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(text + X));
                BigInteger hash2 = new BigInteger(hashBytes2);
                hash2 = BigInteger.Abs(hash2);

                bool f = hash == hash2;
                Console.WriteLine(f);

                string text2 = File.ReadAllText(".\\False.txt");
                var hashBytes3 = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(text2 + X));
                BigInteger hash3 = new BigInteger(hashBytes3);
                hash3 = BigInteger.Abs(hash3);

                bool f2 = hash == hash3;
                Console.WriteLine(f2);
            }

            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds} ms");
            Console.ReadLine();
        }
    }
}
