using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    class RSA
    {
        private Random random = new Random();
        private BigInteger p, q, n, fi, e, d;

        private BigInteger GeneratePrimeNumber()
        {
            BigInteger number = random.Next(1000, 5000);
            while (!GCD.IsSimple(number))
            {
                number = random.Next(1000, 5000);
            }
            return number;
        }

        private BigInteger GenerateCoprimeNumber(BigInteger coprime)
        {
            BigInteger number = random.Next(2, (int)coprime - 1), nod = GCD.GetNOD(number, coprime);
            while (nod != 1)
            {
                number = random.Next(2, (int)coprime - 1);
                nod = GCD.GetNOD(number, coprime);
            }
            return number;
        }

        public RSA()
        {
            p = GeneratePrimeNumber();
            q = GeneratePrimeNumber();
            n = p * q;
            fi = (p - 1) * (q - 1);
            e = GenerateCoprimeNumber(fi);
            d = GCD.ModInverse(e, fi);
            Console.WriteLine(new string('-', 40) + "RSA" + new string('-', 40));
            Console.WriteLine($"p = {p}, q = {q}, n = p * q = {p * q}, fi(n) = {fi}, e = {e}, d = {d}\n");
            Console.WriteLine($"PUBLIC KEY: (e, n) = ({e}, {n})");
            Console.WriteLine($"PRIVATE KEY: (d, n) = ({d}, {n})");
            Console.WriteLine();
        }

        public BigInteger[] Encrypt(string text)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            BigInteger[] encryptedText = new BigInteger[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                encryptedText[i] = BigInteger.ModPow(bytes[i], e, n);
            }
            return encryptedText;
        }

        public string Decrypt(BigInteger[] encryptedText)
        {
            byte[] decryptedText = new byte[encryptedText.Length];
            for (int i = 0; i < encryptedText.Length; i++)
            {
                decryptedText[i] = Convert.ToByte(BigInteger.ModPow(encryptedText[i], d, n).ToString());
            }
            return Encoding.ASCII.GetString(decryptedText);
        }
    }
}
