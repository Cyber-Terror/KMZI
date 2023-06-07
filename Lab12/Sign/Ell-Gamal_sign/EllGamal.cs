using System;
using System.Numerics;

namespace ElGamalDS
{
    class EllGamal
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ЭЦП по Гамалю\n");
            int p = 2137;   // простое
            int g = 2127;   // первый корень < p
            int x = 1116;   // < p
            int y = (int)BigInteger.ModPow(g, x, p);
            int k = 7;      // взаимно простое с p-1
            int a = (int)BigInteger.ModPow(g, k, p);
            Console.WriteLine($"p={p}\ng={g}\nx={x}\ny={y}\nk={k}\na={a}\n");
            int H = 2119;
            int m = p - 1;
            int k_1 = 0;
            for (int i = 0; i < 10000; i++)
            {
                if (((k * i) % m) == 1)
                {
                    k_1 = i;
                    break;
                }
            }
            var b = (k_1 * (H - (x * a) % m) % m) % m;
            Console.WriteLine($"H={H}\nk_1={k_1}\nb={b}\nS = {a},{b}\n");
            Console.WriteLine("Верификация:");
            var ya = BigInteger.ModPow(y, a, p);
            var ab = BigInteger.ModPow(a, b, p);
            var pr1 = BigInteger.ModPow(ya * ab, 1, p);
            var pr2 = BigInteger.ModPow(g, H, p);
            if (pr1 == pr2)
            {
                Console.WriteLine($"{pr1} = {pr2}\nВерификация успешна");
            }
            else Console.WriteLine($"\nВерификация не пройдена");
            Console.ReadKey();
        }
    }
}
