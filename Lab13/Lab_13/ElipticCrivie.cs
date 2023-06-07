using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_13
{
    internal class ElipticCrivie
    {
        private static Random random = new Random();

        private static string alphabeth = "абвгдежзийклмнопрстуфхцчшщъыьэюя";





        private static int[,] points =
            { { 189, 297 }, { 189, 454 }, { 192, 32 }, { 192, 719 }, { 194, 205 }, { 194, 546 }, { 197, 145 }, { 197, 606 },
            { 198, 224 }, { 198, 527 }, { 200, 30 }, { 200, 721 }, { 203, 324 }, { 203, 427 }, { 205, 372 }, { 205, 379 },
            { 206, 106 }, { 206, 645 }, { 209, 82 }, { 209, 669 }, { 210, 31 }, { 210, 720 }, { 215, 247 }, { 215, 504 },
            { 218, 150 }, { 218, 601 }, { 221, 138 }, { 221, 613 }, { 226, 9 }, { 226, 742 }, { 227, 299 }, { 227, 542 }
        };

        public static int[] InversePoint(int[] P)
        {
            return new int[2] { P[0], (-1) * P[1] };
        }

        private static int Lambda(int[] P, int a, int p)
        {
            return NOD.Mod(NOD.Mod(3 * (P[0] * P[0]) + a, p) * NOD.ModInverse(2 * P[1], p), p);
        }

        private static int Lambda(int[] P, int[] Q, int p)
        {
            return NOD.Mod(NOD.Mod(Q[1] - P[1], p) * NOD.Mod(NOD.ModInverse(Q[0] + NOD.Mod(-P[0], p), p), p), p);
        }

        public static int[] CalculateSum(int[] P, int[] Q, int p)
        {
            int lambda = Lambda(P, Q, p);
            int x = NOD.Mod(lambda * lambda - P[0] - Q[0], p);
            int y = NOD.Mod(lambda * (P[0] - x) - P[1], p);
            return new int[] { x, y };
        }

        public static int[] CalculateSum(int[] P, int a, int p)
        {
            int lambda = Lambda(P, a, p);
            int x = NOD.Mod(lambda * lambda - P[0] - P[0], p);
            int y = NOD.Mod(lambda * (P[0] - x) - P[1], p);
            return new int[] { x, y };
        }

        public static int[] scalarMultiple(int k, int[] P, int a, int p)
        {
            int[] scalarMultiple = P;
            for (int i = 0; i < (int)Math.Log(k, 2); i++)
                scalarMultiple = CalculateSum(scalarMultiple, a, p);
            k = k - (int)Math.Pow(2, (int)Math.Log(k, 2));
            while (k > 1)
            {
                for (int i = 0; i < (int)Math.Log(k, 2); i++)
                    scalarMultiple = CalculateSum(scalarMultiple, CalculateSum(P, a, p), p);
                k = k - (int)Math.Pow(2, (int)Math.Log(k, 2));
            }
            if (k == 1) scalarMultiple = CalculateSum(scalarMultiple, P, p);
            return scalarMultiple;
        }


        //2
        public static int[,] Encrypt(string text, int[] G, int a, int p, int d)
        {
            int[] Q = scalarMultiple(d, G, a, p), P;
            int[,] encrText = new int[text.Length, 4];
            int k;
            Console.WriteLine($"G = ({G[0]}, {G[1]}), d = {d}, Q = ({Q[0]}, {Q[1]})");
            for (int i = 0; i < text.Length; i++)
            {
                k = random.Next(2, d);
                P = Enumerable.Range(0, points.GetLength(1)).Select(x => points[alphabeth.IndexOf(text[i]), x]).ToArray();
                int[] C1 = scalarMultiple(k, G, a, p), kQ = scalarMultiple(k, Q, a, p), C2;
                C2 = CalculateSum(P, kQ, p);
                encrText[i, 0] = C1[0]; encrText[i, 1] = C1[1];
                encrText[i, 2] = C2[0]; encrText[i, 3] = C2[1];
            }
            return encrText;
        }

        public static string Decrypt(int[,] encrText, int a, int p, int d)
        {
            string decryptedText = "";
            for (int i = 0; i < encrText.GetUpperBound(0) + 1; i++)
            {
                int[] C1 = scalarMultiple(d, new int[] { encrText[i, 0], encrText[i, 1] }, a, p), C2 = { encrText[i, 2], encrText[i, 3] };
                int[] P = CalculateSum(C2, InversePoint(C1), p);
                for (int k = 0; k < points.GetUpperBound(0) + 1; k++)
                {
                    if (points[k, 0] == P[0] && points[k, 1] == P[1])
                    {
                        decryptedText += alphabeth[k];
                        break;
                    }
                }
            }
            return decryptedText;
        }
    }
}
