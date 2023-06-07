using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab5
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            string key1 = "Антон";
            string key2 = "Дзімітрыадзі";
            string file = "test.txt";
            string mulipleEncr = "multipleEncr.txt";
            string text = "";

            Permutation peremPermutation = new Permutation();
          
            Console.WriteLine("Lab5\n1) ROUTE PERMUTATION:");

            using (StreamReader sr = new StreamReader(file, Encoding.UTF8))
            {
                text = sr.ReadToEnd();
                text = text.Replace(" ", "");
            }

            //1st block start
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            Console.WriteLine("Text:\t"+text+"\n");
            peremPermutation.CountRowsColumns(5, text.Length);
            var res = peremPermutation.Encrypt(text);
            Console.WriteLine("Encrypted text:\t"+res+"\n");
            stopwatch1.Stop();

            Console.WriteLine("Time enrypted " + stopwatch1.ElapsedMilliseconds + " ms");
            Console.WriteLine();

            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            string res1 = peremPermutation.Decrypt(res);
            Console.WriteLine("Decryted text:\t" + res1.Replace("_","") + "\n");
            stopwatch2.Stop();
            Console.WriteLine("Time decryted:" + stopwatch2.ElapsedMilliseconds + " ms");
            //1st block end

            Console.WriteLine("2)MULTIPLE PERMUTATION");
            Stopwatch stopwatch3 = new Stopwatch();
            stopwatch3.Start();
             MultiplePermutationEncrypt(file,mulipleEncr,key1,key2);
            stopwatch3.Stop();
            Console.WriteLine("Time encrypted" + stopwatch3.ElapsedMilliseconds + " ms");

            Console.WriteLine();
            Stopwatch stopwatch4 = new Stopwatch();
            stopwatch4.Start();
            MultiplePermutationDecrypt(mulipleEncr,file,key1,key2);
            stopwatch4.Stop();
            Console.WriteLine("Time decrypted " + stopwatch4.ElapsedMilliseconds + " ms");
            Console.WriteLine("Check output file");

            Console.ReadLine();


        }
        // Функция шифрования
        
        class Permutation
        {
            private int s;
            private int k;
            char[,] table;
            public void CountRowsColumns(int countString, int lengthMessage)
            {
                s = countString;
                k = (lengthMessage - 1) / countString + 1;
            }
            public string Encrypt(string input)
            {
                var map1 = new Dictionary<char, int>();
                foreach (char c in input)
                {
                    if (!map1.ContainsKey(c))
                        map1.Add(c, 1);
                    else
                        map1[c] += 1;
                }
                int len1 = input.Length;
                int l = 0;
                string result = "";
                table = new char[k, s];
                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < s; j++)
                    {
                        if (l < input.Length)
                        {
                            table[i, j] = input[l];
                            l++;
                        }
                        else
                        {
                            table[i, j] = '_';
                        }
                    }
                }
                for (int i = 0; i < s; i++)
                {
                    for (int j = 0; j < k; j++)
                    {
                        result += table[j, i];
                    }
                }
                var map2 = new Dictionary<char, int>();
                foreach (char c in result)
                {
                    if (!map2.ContainsKey(c))
                        map2.Add(c, 1);
                    else
                        map2[c] += 1;
                }
                int len2 = result.Length;
                return result;
            }
            public string Decrypt(string output)
            {
                int p = 0;
                string result = "";
                table = new char[k, s];
                for (int i = 0; i < s; i++)
                {
                    for (int j = 0; j < k; j++)
                    {
                        if (p < output.Length)
                        {
                            table[j, i] = output[p];
                            p++;
                        }
                        else
                        {
                            table[j, i] = '_';
                        }

                    }
                }
                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < s; j++)
                    {
                        result += table[i, j];
                    }

                }
                return result;
            }
        }

        
       
        static void MultiplePermutationEncrypt(string inputFile, string encryptedFile, string key1, string key2)
        {
            string plainText = File.ReadAllText(inputFile);
            string encryptedText = "";
            int n = key1.Length, m = key2.Length;

            var key1sorted = key1.ToCharArray();
            var key2sorted = key2.ToCharArray();

            var separatedText = Regex.Matches(plainText, ".{1," + (m * n) + "}").Cast<Match>().Select(m => m.Value).ToList();

            foreach (var substring in separatedText)
            {
                var localResult = substring;

                int temp = 0;
                char lastChar_OrepatedWith = '`';
                var locseckey1 = key1;
                foreach (var ch in key1sorted)
                {
                    if (lastChar_OrepatedWith != ch)
                    {
                        SwapRow(ref localResult, locseckey1.IndexOf(ch), temp, n, m);
                        SwapCharacters(ref locseckey1, locseckey1.IndexOf(ch), temp++);
                    }
                    else
                    {
                        SwapRow(ref localResult, locseckey1.LastIndexOf(ch), temp, n, m);
                        SwapCharacters(ref locseckey1, locseckey1.LastIndexOf(ch), temp++);
                    }
                    lastChar_OrepatedWith = ch;
                }

                temp = 0;
                lastChar_OrepatedWith = '`';
                var locseckey2 = key2;
                foreach (var ch in key2sorted)
                {
                    if (lastChar_OrepatedWith != ch)
                    {
                        SwapColumn(ref localResult, locseckey2.IndexOf(ch), temp, n, m);
                        SwapCharacters(ref locseckey2, locseckey2.IndexOf(ch), temp++);
                    }
                    else
                    {
                        SwapColumn(ref localResult, locseckey2.LastIndexOf(ch), temp, n, m);
                        SwapCharacters(ref locseckey2, locseckey2.LastIndexOf(ch), temp++);
                    }
                    lastChar_OrepatedWith = ch;
                }
                encryptedText += localResult;
            }
            File.WriteAllText(encryptedFile, encryptedText);
        }
        static void MultiplePermutationDecrypt(string encryptedFile, string decryptedFile, string key1, string key2)
        {
            string encryptedText = File.ReadAllText(encryptedFile);
            string decryptedText = "";

            int n = key1.Length, m = key2.Length;

            var key1sorted = key1.ToCharArray();
            var key2sorted = key2.ToCharArray();

            var separatedText = Split(encryptedText, m * n);

            foreach (var substring in separatedText)
            {
                var localResult = substring;

                int temp = 0;
                char lastChar_OrepatedWith = '`';
                var locseckey1 = String.Concat(key1sorted.Where(c => key1sorted.Contains(c)));
                foreach (var ch in key1)
                {
                    if (lastChar_OrepatedWith != ch)
                    {
                        SwapRow(ref localResult, locseckey1.IndexOf(ch), temp, n, m);
                        SwapCharacters(ref locseckey1, locseckey1.IndexOf(ch), temp++);
                    }
                    else
                    {
                        SwapRow(ref localResult, locseckey1.LastIndexOf(ch), temp, n, m);
                        SwapCharacters(ref locseckey1, locseckey1.LastIndexOf(ch), temp++);
                    }
                    lastChar_OrepatedWith = ch;
                }

                
                temp = 0;
                lastChar_OrepatedWith = '`';
                var locseckey2 = String.Concat(key2sorted.Where(c => key2sorted.Contains(c)));
                foreach (var ch in key2)
                {
                    if (lastChar_OrepatedWith != ch)
                    {
                        SwapColumn(ref localResult, locseckey2.IndexOf(ch), temp, n, m);
                        SwapCharacters(ref locseckey2, locseckey2.IndexOf(ch), temp++);
                    }
                    else
                    {
                        SwapColumn(ref localResult, locseckey2.LastIndexOf(ch), temp, n, m);
                        SwapCharacters(ref locseckey2, locseckey2.LastIndexOf(ch), temp++);
                    }
                    lastChar_OrepatedWith = ch;
                }
                decryptedText += localResult;
            }
           
        }
        private static void SwapCharacters(ref string str, int poschar1, int poschar2)
        {
            if (poschar1 < 0 || poschar1 >= str.Length - 1 || poschar2 < 0 || poschar2 >= str.Length - 1)
            {
                return;
            }
            var bufferBuilder = new StringBuilder(str);
            char ch1 = str[poschar1];
            char ch2 = str[poschar2];
            bufferBuilder.Remove(poschar1, 1);
            bufferBuilder.Insert(poschar1, ch2);
            bufferBuilder.Remove(poschar2, 1);
            bufferBuilder.Insert(poschar2, ch1);
            str = bufferBuilder.ToString();
        }

        private static void SwapColumn(ref string str, int column1, int column2, int n, int m)
        {
            for (int i = 0; i < n; i++)
            {
                SwapCharacters(ref str, i * m + column1, i * m + column2);
            }
        }

        private static void SwapRow(ref string str, int row1, int row2, int n, int m)
        {
            for (int i = 0; i < m; i++)
            {
                SwapCharacters(ref str, row1 * m + i, row2 * m + i);
            }
        }
        static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        static void CountSymbolFrequency(char[] text)
        {
            Dictionary<char, int> letterFrequency = new Dictionary<char, int>();


            foreach (char c in text)
            {
                char toLowerLetter = char.ToLower(c);
                if (char.IsLetter(toLowerLetter) && char.IsLower(toLowerLetter))
                {
                    if (letterFrequency.ContainsKey(toLowerLetter))
                    {
                        letterFrequency[toLowerLetter]++;
                    }
                    else
                    {
                        letterFrequency.Add(toLowerLetter, 1);
                    }
                }
            }
            int sumUp = letterFrequency.Sum(x => x.Value);


            foreach (KeyValuePair<char, int> elem in letterFrequency.OrderByDescending(key => key.Value))
            {
                double frequencyPercentage = (double)elem.Value / sumUp * 100;
                Console.WriteLine("{0} - {1:F2}", elem.Key, frequencyPercentage);
            }
        }

    }
}
    