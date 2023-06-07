using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Lb2
{
    class P
    {
        static void Main(string[] args)
        {
            char[] greek = { 'α', 'β', 'γ', 'δ', 'ε', 'z', 'η', 'θ', 'ι', 'κ', 'λ', 'μ', 'ν', 'ξ', 'ο', 'π', 'ρ', 'Σ', 'τ', 'υ', 'φ', 'χ', 'ψ', 'ω' };
            char[] spanish = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'ñ', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            char[] binary = { '1', '0' };
            Console.WriteLine("Для какого алфавита выполним энтропию");
            Console.WriteLine("1. Greek, 2. spanish, 3. binary");
            int selected;
            int.TryParse(Console.ReadLine(), out selected);

            if (selected == 1)
            {
                Entropy("Στν Ελλαδα υπαρχει πλουσια ιστορια και πολιτισμοσ, που αναδεικνυονται" +
                        " μεσω των αρχαιολογικων ανασκαφων και τωνz μνημειων πουψ διατηρουνται σε διαφορα μερη της χωρασ. Η Ακροπολη της Αθηνασ και το Παρθενωνασ " +
                        "ειναι βυο αξπο τα πιο διασημα αρχαιολογικα μνημεια στην Ελλαδα και στον κοσμο. ", greek);
            }
            else if (selected == 2)
            {
                Entropy("En España, hay una rica historia y cultura que se refleja a traves de los monumentos y " +
                        "las ruinas arqueologicas que se conservan en todo el pais. La Sagrada Familia y el Parque Guell en Barcelona, la Alhambra en Granada, y el Palacio Real en Madrid son " +
                        "algunos de los monumentos mas famosos de España. La cocina española es tambien muy conocida por sus sabores frescos y autenticos ingredientes locales, " +
                        "como mariscos y carnes, asi como por sus vinos y aceites de oliva. Ademas, España es conocida por su cultura y arte, como la arquitectura, la escultura y " +
                        "la pintura, que reflejan la rica historia del pais. ", spanish);
            }
            else if (selected == 3)
            {
                Console.WriteLine(Binary("\tBinary Text"));
                Entropy(Binary("Binary Text"), binary);
            }

            Console.WriteLine("\t3 Задание (посчитать количество информации)");
            Console.WriteLine("\tδημητριaδησ aντωνησ");
            Console.WriteLine("\tDimitriadi Antón");
            Console.WriteLine("\tGreek");
            double g1 = Entropy("δημητριaδησ aντωνησ", greek);
            Console.WriteLine("Количество информации:    " + ("δημητριaδησ aντωνησ".Length * g1));
            Console.WriteLine("Spanish");
            double s1 = Entropy("Dimitriadi Anton ", spanish);
            Console.WriteLine("Количество информации:    " + ("Dimitriadi Anton".Length * s1));
            Console.WriteLine("Binary");
            double a1 = Entropy(Binary("Dimitriadi Anton"), binary);
            Console.WriteLine("Количество информации:    " + (Binary("Dimitriadi Anton").Length * a1));
            Console.WriteLine("4 задание");
            double b1 = (double)(1 - (-0.1 * Math.Log(0.1, 2) - (1 - 0.1) * Math.Log((1 - 0.1), 2)));
            Console.WriteLine("0.1: " + (Binary("Dimitriadi Anton").Length * b1));
            Console.WriteLine("Количество информации:    " + (Binary("Dimitriadi Anton").Length * b1));
            double b2 = (double)(1 - (-0.5 * Math.Log(0.5, 2) - (1 - 0.5) * Math.Log((1 - 0.5), 2)));
            Console.WriteLine("0.5: " + (Binary("Dimitriadi Anton").Length * b2));
            Console.WriteLine("Количество информации:    " + (Binary("Dimitriadi Anton").Length * b2));
            double b3 = (double)(1 - (-0.9999999999999999 * Math.Log(0.9999999999999999, 2) - (1 - 0.9999999999999999) * Math.Log((1 - 0.9999999999999999), 2)));
            Console.WriteLine("1.0: " + (Binary("Dimitriadi Anton").Length * b3));
            Console.WriteLine("Количество информации:    " + (Binary("Dimitriadi Anton").Length * b3));
            Console.WriteLine();
            Console.WriteLine(" Эффективная энтропия для греческого ");
            double s2 = (double)(1 - (-0.1 * Math.Log(0.1, 2) - (1 - 0.1) * Math.Log((1 - 0.1), 2)));
            Console.WriteLine("0.1: " + (Binary("Dimitriadi Anton").Length * s2));
            Console.WriteLine("Количество информации:    " + ("Dimitriadi Anton".Length * s2));
            double s3 = (double)(1 - (-0.5 * Math.Log(0.5, 2) - (1 - 0.5) * Math.Log((1 - 0.5), 2)));
            Console.WriteLine("0.5: " + (Binary("Dimitriadi Anton").Length * s3));
            Console.WriteLine("Количество информации: " + ("Dimitriadi Anton".Length * s3));
            double s4 = (double)(1 - (-0.9999999999999999 * Math.Log(0.9999999999999999, 2) - (1 - 0.9999999999999999) * Math.Log((1 - 0.9999999999999999), 2)));
            Console.WriteLine("1.0: " + (Binary("Dimitriadi Anton").Length * s4));
            Console.WriteLine("Количество информации: 0");
            Console.ReadLine();
        }
        public static double Entropy(string t, char[] a)
        {
            double r = 0;
            for (int i = 0; i < a.Length; i++)
            {
                int c = Regex.Matches(t, a[i].ToString(), RegexOptions.IgnoreCase).Count;
                double p = (double)c / t.Length;
                if (p != 0)
                {
                    r += p * Math.Log(p, 2);
                }

                Console.WriteLine(a[i].ToString() + "     " + p);
            }
            Console.WriteLine("Энтропия:");
            Console.WriteLine(-r);
            return -r;
        }
        public static string Binary(string t)
        {
            byte[] buf = Encoding.ASCII.GetBytes(t);
            StringBuilder sb = new StringBuilder(buf.Length * 8);
            foreach (byte b in buf)
            {
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            string bStr = sb.ToString();
            return bStr;
        }
    }
}