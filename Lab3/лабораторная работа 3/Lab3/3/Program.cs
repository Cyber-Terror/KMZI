using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lab3");
            PrimeFactorization(64);
            Console.WriteLine("\t НОД для 2 чисел:");
            Console.WriteLine("Число A = ");
            int numberA1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Число B = ");
            int numberB1 = int.Parse(Console.ReadLine());
            Console.WriteLine("НОД для  " + numberA1 + "  и " + numberB1 + " = " + GreatestCommonDivisor(numberA1, numberB1));
            Console.WriteLine("\n\tНОД для 3 чисел:");
            Console.WriteLine("Число A = ");
            int numberA2 = int.Parse(Console.ReadLine());
            Console.WriteLine("Число B = ");
            int numberB2 = int.Parse(Console.ReadLine());
            Console.WriteLine("Число C = ");
            int numberC2 = int.Parse(Console.ReadLine());
            Console.WriteLine("НОД для " + numberA2 + ", " + numberB2 + " и " + numberC2 + " = " + GreatestCommonDivisor(GreatestCommonDivisor(numberA2, numberB2), numberC2));
            Console.WriteLine("\n\t --------------------- Поиск простых чисел: --------------------------");
            Console.WriteLine("\t Поиск простых чисел на интервале. Сравнить это число с n/ln(n)");

            Console.WriteLine("\t Начало интервала: 2");
            int start = 2;
            Console.WriteLine("\t Введите конец интервала: ");
            int finish = int.Parse(Console.ReadLine());
            Console.WriteLine("\t Простые числа на интервале [{0}, {1}]:", start, finish);
            List<int> primeNumbers = Enumerable.Range(start, finish - start + 1).Where(IsPrimeNumber).ToList();
            Console.WriteLine(string.Join(", ", primeNumbers));
            Console.WriteLine("\t Количество простых чисел: " + primeNumbers.Count);
            Console.WriteLine("\t n/ln(n): " + CalculateNLogN(finish));
            Console.WriteLine("\t Округление: " + Math.Round(CalculateNLogN(finish)));
            Console.WriteLine("\t Введите начало интервала: ");
            uint start1 = uint.Parse(Console.ReadLine());
            Console.WriteLine("\t Введите конец интервала: ");
            uint finish1 = uint.Parse(Console.ReadLine());
            Console.WriteLine("\t Повторить п. 1 для интервала [m, n]. Сравнить полученные результаты с «ручными» вычислениями, используя «решето Эратосфена» ");
            List<uint> primeNumbersEratosthenes = Enumerable.Range((int)start1, (int)(finish1 - start1 + 1)).Where(IsPrimeNumber).Select(i => (uint)i).ToList();
            Console.WriteLine(string.Join(", ", primeNumbersEratosthenes));
            Console.WriteLine("Является ли конкатенация простым числом?\n\t Первое число: ");
            int num1 = int.Parse(Console.ReadLine());
            Console.WriteLine("\t Второе число: ");
            int num2 = int.Parse(Console.ReadLine());
            int newNumber = int.Parse(num1.ToString() + num2.ToString());
            Console.WriteLine(newNumber);
            CheckIfPrime(newNumber);
            Console.ReadLine();
        }

        public static int GreatestCommonDivisor(int numberA, int numberB)
        {
            return numberB == 0 ? numberA : GreatestCommonDivisor(numberB, numberA % numberB);
        }

        public static void CheckIfPrime(int number)
        {
            bool isPrime = IsPrimeNumber(number);

            if (isPrime)
            {
                Console.WriteLine("Число простое");
            }
            else
            {
                Console.WriteLine("Число не простое");
            }
        }

        public static bool IsPrimeNumber(int number)
        {
            return number > 1 && Enumerable.Range(2, (int)Math.Sqrt(number) - 1).All(i => number % i != 0);
        }

        static double CalculateNLogN(int number)
        {
            return number / Math.Log(number);
        }

        static void PrimeFactorization(int number)
        {
            Console.Write(number + " = ");

            for (int i = 2; i <= number; i++)
            {
                int count = 0;

                while (number % i == 0)
                {
                    count++;
                    number /= i;
                }

                if (count == 0) continue;

                Console.Write(i);

                if (count > 1)
                {
                    Console.Write("^" + count);
                }

                if (number != 1)
                {
                    Console.Write(" * ");
                }
            }

            Console.WriteLine();
        }
    }
}
