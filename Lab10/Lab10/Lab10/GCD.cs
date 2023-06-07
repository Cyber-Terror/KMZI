using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    class GCD
    {
        public static bool IsSimple(BigInteger N)
        {
            for (int i = 2; i <= (N / 2); i++)
            {
                if (N % i == 0)
                    return false;
            }
            return true;
        }

        public static BigInteger GetNOD(BigInteger a, BigInteger b)
        {
            while (true)
            {
                if (a > b)
                    a -= b;
                else if (a < b)
                    b -= a;
                else
                    return a;
            }
        }

        public static BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            a = a % m;
            for (BigInteger x = 1; x < m; x++)
                if ((a * x) % m == 1)
                    return x;
            return 1;
        }
    }
}
