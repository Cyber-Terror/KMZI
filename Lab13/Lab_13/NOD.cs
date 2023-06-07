using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_13
{
    internal class NOD
    {
        public static int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }

        public static int ModInverse(int a, int b)
        {
            a = a % b;
            for (int x = 1; x < b; x++)
                if ((a * x) % b == 1)
                    return x;
            return 1;
        }
    }
}
