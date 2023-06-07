using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    class ElGamal
    {
        private Random random = new Random();
        private BigInteger p, g, x, y;

        private BigInteger GeneratePrimeNumber()
        {
            BigInteger number = random.Next(3, 1000);
            while (!GCD.IsSimple(number))
            {
                number = random.Next(3, 1000);
            }
            return number;
        }

        public int GetPRoot(BigInteger p)
        {
            for (int i = 0; i < p; i++)
                if (IsPRoot(p, i))
                    return i;
            return 0;
        }

        public bool IsPRoot(BigInteger p, BigInteger a)
        {
            if (a == 0 || a == 1)
                return false;
            BigInteger last = 1;
            HashSet<BigInteger> set = new HashSet<BigInteger>();
            for (int i = 0; i < p - 1; i++)
            {
                last = (last * a) % p;
                if (set.Contains(last))
                    return false;
                set.Add(last);
            }
            return true;
        }

        public ElGamal()
        {
            p = GeneratePrimeNumber();
            g = GetPRoot(p);
            x = random.Next(2, (int)p);
            y = BigInteger.ModPow(g, x, p);
            Console.WriteLine(new string('-', 38) + "El-Gamal" + new string('-', 38));
            Console.WriteLine($"p = {p}, g = {g}, x = {x}, y = {y}\n");
            Console.WriteLine($"PUBLIC KEY: (p, g, y) = ({p}, {g}, {y})");
            Console.WriteLine($"PRIVATE KEY: (p, g, x) = ({p}, {g}, {x})");
            Console.WriteLine();
        }

        public BigInteger[,] Encrypt(string text)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            BigInteger[,] encryptedText = new BigInteger[bytes.Length, 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                int k = random.Next(2, (int)p - 2);
                encryptedText[i, 0] = BigInteger.ModPow(g, k, p);
                encryptedText[i, 1] = BigInteger.ModPow(BigInteger.Multiply(BigInteger.Pow(y, k), bytes[i]), 1, p);
            }
            return encryptedText;
        }

        public string Decrypt(BigInteger[,] encryptedText)
        {
            byte[] decryptedText = new byte[encryptedText.GetUpperBound(0) + 1];
            for (int i = 0; i < encryptedText.GetUpperBound(0) + 1; i++)
            {
                BigInteger inverseA = GCD.ModInverse(BigInteger.Pow(encryptedText[i, 0], (int)x), p);
                decryptedText[i] = Convert.ToByte(BigInteger.ModPow(BigInteger.Multiply(encryptedText[i, 1], inverseA), 1, p).ToString());
            }
            return Encoding.ASCII.GetString(decryptedText);
        }
    }
}
