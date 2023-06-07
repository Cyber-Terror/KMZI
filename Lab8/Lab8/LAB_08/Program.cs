namespace LAB_08
{
    using System;
    using System.Diagnostics;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lab8");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Stopwatch stopwatch1 = new Stopwatch();
            Stopwatch stopwatch2 = new Stopwatch();
            LinearCongruentialGenerator.Clue = 324212;
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(Convert.ToString(LinearCongruentialGenerator.Rand(), 2));
            }

            Console.WriteLine();

            var keyRC4 = new byte[] { 13, 19, 90, 92, 240 };
            string encoded,text = "The quick brown fox jumps over the lazy dog. It was a sunny day, and the birds were chirping happily in the trees. John went for a walk in the park and enjoyed the fresh air. He met his friend Sarah, and they had a delightful conversation. Time flew by as they talked about their hobbies and shared funny stories. Eventually, they decided to grab a cup of coffee at their favorite café. The aroma of freshly brewed coffee filled the air, creating a warm and inviting atmosphere. They sipped their drinks and laughed, making memories that would last a lifetime.";

            Console.WriteLine($" Normal text: {text}");

            stopwatch1.Start();
            Console.WriteLine($" Encrypted text: {encoded = RC4Crypt.Crypt(text, keyRC4)}");
            stopwatch1.Stop();
            Console.WriteLine("Time encr:" + stopwatch1.ElapsedMilliseconds + " ms");
         
            stopwatch2.Start();
            Console.WriteLine($" Decrytped text: {RC4Crypt.Crypt(encoded, keyRC4)}");
            stopwatch2.Stop();
            Console.WriteLine("Time encr:" + stopwatch1.ElapsedMilliseconds + " ms");
        }
    }
}
