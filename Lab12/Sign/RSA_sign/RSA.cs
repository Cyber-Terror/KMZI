using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;

namespace RsaDS
{
    class RSA
    {
        public static readonly char[] characters = { '#', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-' };
        public bool IsSimple(long n)
        {
            if (n < 2) return false;
            if (n == 2) return true;

            for (long i = 2; i < n; i++)
            {
                if (n % i == 0) return false;
            }

            return true;
        }
        public int Exp(int d, int m)
        {
            int e = 10;

            while ((e * d) % m != 1) e++;

            return e;
        }
        public int Secret(int m)
        {
            int d = m - 1;

            for (int i = 2; i <= m; i++)
            {
                if ((m % i == 0) && (d % i == 0))
                {
                    d--;
                    i = 1;
                }
            }

            return d;
        }
        public List<string> Encode(string hash, int e, int n)
        {
            List<string> result = new List<string>();

            foreach (char c in hash)
            {
                int index = Array.IndexOf(characters, c);
                BigInteger bi = BigInteger.ModPow(new BigInteger(index), e, n);
                result.Add(bi.ToString());
            }

            return result;
        }
        public string Decode(List<string> input, int d, int n)
        {
            try
            {
                string result = "";

                foreach (string item in input)
                {
                    BigInteger bi = BigInteger.ModPow(new BigInteger(Convert.ToDouble(item)), d, n);
                    int index = Convert.ToInt32(bi.ToString());
                    result += characters[index].ToString();
                }

                return result;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
    class Program
    {
        public static readonly char[] characters = { '#', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-' };

        static void Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Console.WriteLine("\nЭЦП RSA\n");

            RSA rsa = new RSA();
            string M = File.ReadAllText("first.txt");
            Process.Start("first.txt");
            int p = 101;
            int q = 103;
            string hash = M.GetHashCode().ToString();
            int n = p * q;
            int m = (p - 1) * (q - 1);
            int d = rsa.Secret(m);
            int e_ = rsa.Exp(d, m);
            Console.WriteLine($" p = {p}\n q = {q}\n n = {n}\n ф(n) = {m}\n d = {d}\n e = {e_}\n M = {M}\n hash = {hash}\n");
            List<string> sign = rsa.Encode(hash, e_, n);

            List<string> input = new List<string>();
            string hash2 = File.ReadAllText("first.txt").GetHashCode().ToString();
            string result = rsa.Decode(sign, d, n);
            Console.WriteLine($"хэш ЭЦП = {result}");
            Console.WriteLine($"хэш-файл = {hash2}");

            if (result.Equals(hash2))
                Console.WriteLine("Верификация успешна\n");
            else
                Console.WriteLine("Верификация не пройдена\n");

            Console.WriteLine($"{stopwatch.ElapsedMilliseconds} ms");
            Console.ReadKey();
        }
    }
}
