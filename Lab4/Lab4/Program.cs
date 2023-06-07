using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace Lab4
{
    internal class Program
    {
        
        const int k = 21;
        const string keyWord = "антон";
        const int rows = 4;
        const int columns = 8;
        const string alphabetBel = "абвгдеёжзійклмнопрстуўфхцчшыьэюя";//32
        

        const string file = "../../../txt/file.txt";
        const string encr_caesar = "../../../txt/encr.txt";
        const string encr_trithemus = "../../../txt/encrTri.txt";
        const string decr_trithemus = "../../../txt/decr.txt";

        static void Main(string[] args)
        {

            //
            Console.WriteLine("Lab_4\nVariant 6 \n ");
            Console.WriteLine("\nAlogrithms: Caesar (key=21) & Trithemius (key word=(антон)) \n ");
            Console.WriteLine("Alphabet:Belarussian");
            Console.WriteLine("Lab_4\nTask1=Caesar");

            FileWriter(EncryptCaesar(), encr_caesar);
            FileWriter(DecryptCaesar(), file);

            Console.WriteLine(" Task2=Trithemius");
            FileWriter(EncryptTrithemius(keyWord), encr_trithemus);
            FileWriter(DecryptTrithemius(keyWord), decr_trithemus);



            //var strFile = FileReader(encr_trithemus);
            //CountSymbolFrequency(strFile);




        }
        public static char[] FileReader(string path = file)
        {
            var result = "";
            using (var sr = new StreamReader(path))
                result = sr.ReadToEnd().ToLower();
            return result.ToCharArray();
        }




        public static bool FileWriter(char[] result, string path = encr_caesar)
        {
            try
            {
                using (var sw = new StreamWriter(path))
                    sw.WriteLine(result);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
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

        public static char[] EncryptCaesar(string fileName = file)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var str = FileReader(fileName);
            var alphLength = alphabetBel.Length;
            var length = str.Length;

            for (var i = 0; i < length; ++i)
                for (var j = 0; j < alphLength; ++j)
                    if (str[i] == alphabetBel[j])
                    {
                        var index = (j + k) % alphLength;
                        str[i] = alphabetBel[index];
                        break;
                    }

            stopWatch.Stop();
            Console.WriteLine($"Encrypt Caesar:\t{stopWatch.ElapsedMilliseconds} ElapsedMilliseconds ");
            return str;
        }


        
        public static char[] DecryptCaesar(string fileName = encr_caesar)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var str = FileReader(fileName);
            var alphLength = alphabetBel.Length;
            var length = str.Length;

            for (var i = 0; i < length; ++i)
                for (var j = 0; j < alphLength; ++j)
                    if (str[i] == alphabetBel[j])
                    {
                        var index = (j - k + alphLength) % alphLength;
                        str[i] = alphabetBel[index];
                        break;
                    }

            stopWatch.Stop();
            Console.WriteLine($"Decrypt Caesar:\t{stopWatch.ElapsedMilliseconds} ElapsedMilliseconds ");
            return str;
        }
        public static char[,] InitializeTrithemiusTable(string key)
        {
            var table = new char[rows, columns];
            var index = 0;

            foreach (var c in key.Distinct())
            {
                table[index / columns, index % columns] = c;
                index++;
            }

            foreach (var c in alphabetBel)
            {
                if (index >= rows * columns)
                    break;
                if (!key.Contains(c))
                {
                    table[index / columns, index % columns] = c;
                    index++;
                }
            }

            return table;
        }


       
        public static char[] EncryptTrithemius(string key, string fileName = file)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            bool cycleGo;
            var result = FileReader(fileName);
            var table = InitializeTrithemiusTable(key);

            for (var i = 0; i < result.Length; ++i)
            {
                cycleGo = true;
                for (var row = 0; row < rows && cycleGo; ++row)
                    for (var column = 0; column < columns && cycleGo; ++column)
                        if (result[i] == table[row, column])
                        {
                            result[i] = (row != rows - 1) ? table[row + 1, column] : table[0, column];
                            cycleGo = false;
                            break;
                        }
            }

            stopWatch.Stop();
            Console.WriteLine($"Encrypt Trithemius:\t{stopWatch.ElapsedMilliseconds} ElapsedMilliseconds");
            return result;
        }


        public static char[] DecryptTrithemius(string keyword, string fileName = encr_trithemus)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            bool cycleGo;
            var result = FileReader(fileName);
            var table = InitializeTrithemiusTable(keyword);

            for (var i = 0; i < result.Length; ++i)
            {
                cycleGo = true;
                for (var row = 0; row < rows && cycleGo; ++row)
                    for (var column = 0; column < columns && cycleGo; ++column)
                        if (result[i] == table[row, column])
                        {
                            result[i] = (row == 0) ? table[rows - 1, column] : table[row - 1, column];
                            cycleGo = false;
                            break;
                        }
            }

            stopWatch.Stop();
            Console.WriteLine($"Decrypt Trithemus:\t{stopWatch.ElapsedMilliseconds} ElapsedMilliseconds");
            return result;
        }
    }
}