using Lab_13;
using System.Diagnostics;

Console.WriteLine("--------- 1.1");
int xMin = 176, xMax = 200, a = -1, b = 1, p = 751;
for (int x = xMin; x <= xMax; x++)
{
    Console.WriteLine($"x = {x}, y = {Math.Sqrt((x * x * x - x + b) % p)}");
}

Console.WriteLine("--------- 1.2");
int k = 9;
int[] P = { 72, 497 }, Q = { 61, 622 }, R = { 70, 556 };
Console.WriteLine($" P({P[0]}, {P[1]}), Q({Q[0]}, {Q[1]}), R({R[0]}, {R[1]})");
int[] scalarMultiple = ElipticCrivie.scalarMultiple(k, P, a, p);
int[] lQ = ElipticCrivie.scalarMultiple(k, Q, a, p);
Console.WriteLine($"a) scalarMultiple = 9P = {scalarMultiple.Select(el => el.ToString()).Aggregate((prev, current) => "R(" + prev + ", " + current + ")")}");
Console.WriteLine($"b) P + Q = {ElipticCrivie.CalculateSum(P, Q, p).Select(el => el.ToString()).Aggregate((prev, current) => "R(" + prev + ", " + current + ")")}");
Console.WriteLine($"c) scalarMultiple + lQ - R = 9P + 7Q - R = {ElipticCrivie.CalculateSum(ElipticCrivie.CalculateSum(scalarMultiple, lQ, p), ElipticCrivie.InversePoint(R), p).Select(el => el.ToString()).Aggregate((prev, current) => "R(" + prev + ", " + current + ")")}");
Console.WriteLine($"d) P - Q + R = {ElipticCrivie.CalculateSum(ElipticCrivie.CalculateSum(P, ElipticCrivie.InversePoint(Q), p), R, p).Select(el => el.ToString()).Aggregate((prev, current) => "R(" + prev + ", " + current + ")")}");
Console.WriteLine();

Console.WriteLine("--------- 2");
int d = 44;
string text = "димитриади".ToLower();
Console.WriteLine($"Text: {text}");
var stopwatch = Stopwatch.StartNew();
int[,] encrText = ElipticCrivie.Encrypt(text, new int[] { 0, 1 }, a, p, d);
stopwatch.Stop();
Console.WriteLine($"Encrypted text: {string.Join(" ", encrText.Cast<int>())}");
Console.WriteLine($"Encryption time: {stopwatch.ElapsedMilliseconds} ms");
stopwatch.Restart();
Console.WriteLine($"Decrypted text: {text}");
stopwatch.Stop();
Console.WriteLine($"Decryption time: {stopwatch.ElapsedMilliseconds} ms");

Console.ReadKey();
