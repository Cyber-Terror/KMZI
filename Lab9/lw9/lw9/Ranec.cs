using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw9
{
    class Ranec
    {
        public int[] superOrder(int z)
        {
            Random rnd = new Random();
            int[] k = new int[z];
            int sum = 0;
            for (int i = 0; i < z; i++)
            {
                k[i] = rnd.Next(sum+1, sum + 23);
                sum += k[i];
            }
            return k;
        }

        public int[] normalOrder(int[] d, int a, int n, int z)
        {
            int[] e = new int[z];

            for (int i = 0; i < z; i++)
            {
                e[i] = (d[i] * a) % n;
            }
            return e;
        }

        
        public int[] encode(int[] e, string M, int z)
        {
            int j = 0;
            int[] result = new int[M.Length];
            int total = 0;


            foreach (char Mi in M)
            {
                total = 0;
                string Mi2 = '0' + GetBytes(Mi.ToString()); 
              

                for (int i = 0; i < Mi2.Length; i++)
                {
                    if (Mi2[i] == '1') total += e[i];
                }
                result[j] = total;
                j++;
            }
            return result;
        }

       
        public string decode(int[] d, int Si, int z)
        {
            string res = "";
            string res2 = "";

            for (int i = z; i > 0; i--)
            {
                if (Si >= d[i - 1])
                {
                    res += '1';
                    Si = Si - d[i - 1];
                }
                else
                {
                    res += '0';
                }
            }
            for (int i = res.Length - 1; i > -1; i--)
            {
                res2 += res[i];
            }
            return res2;
        }

        public int a_1(int a, int n)
        {
            int res = 0;
            for (int i = 0; i < 10000; i++)
            {
                if (((a * i) % n) == 1) return (i);
            }
            return (res);
        }

        public string Str(int[] a)
        {
            string res = "";
            foreach (int x in a)
            {
                res += x.ToString() + "; ";
            }
            return res;
        }

        public string GetBytes(String str)
        {
            String strB = "";
            for (int i = 0; i < str.Length; i++)
            {
                strB += Convert.ToString((int)str[i], 2);
            }
            return strB;
        }

        public int getN(int sum)
        {
            return sum + 1;
        } 
         public int getA(int n)
        {
            Random rnd = new Random();
            while (true)
            {
                int a = rnd.Next(1, 1000000);
                if (Evklid(a, n) == 1)
                {
                    return a;
                }
            }  
        }
        public static int Evklid(int a, int b)
        {
            while (a != b)
            {
                if (a > b) a -= b;
                else b -= a;
            }
            return a;
        }
    }
}
