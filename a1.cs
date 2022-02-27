using System;
using System.Diagnostics;
using System.IO;

internal class Projekt1
{
    private const int C_Max = 256 * 1024 * 1024 - 1;
    private static int[] Tab;

    private static long Cnt;
//----------------
    private static int BinSearch(int N, int Number)
    {
        var Left = 0;
        var Right = N - 1;
        int Middle;
        while (Left <= Right)
        {
            Cnt++;
            Middle = (Left + Right) >> 1; // dzielenie przez 2

            var val = Tab[Middle];

            if (val == Number) return Middle;
            if (val > Number) Right = Middle - 1;
            else Left = Middle + 1;
        }

        return -1;
    }
//----------------
    private static int LinSearch(int N, int Number)
    {
        for (var i = 0; i < N; i++)
        {
            Cnt++;
            if (Tab[i] == Number) return i;
        }

        return -1;
    }
//--------------------
    private static void LinPes()
    {
        var Stoper = new Stopwatch();
        var file = new StreamWriter("lin_pes.csv");
        for (var i = 100; i <= 200; i += 10)
        {
            var n = i * 100000;
            Cnt = 0;

            Stoper.Start();
            LinSearch(n, 0);
            Stoper.Stop();

            file.WriteLine("Iteracja: {0}; Wynik: {1}; {3} Czas: {3}", i, n, Cnt, Stoper.ElapsedMilliseconds);
            Console.WriteLine("Iteracja: {0}; WYnik: {1}; {2} Czas: {3}", i,  n, Cnt, Stoper.ElapsedMilliseconds);
            Stoper.Reset();
        }

        file.Close();
    }
//--------------------
    private static void LinAvg()
    {
        var Stoper = new Stopwatch();
        var file = new StreamWriter("lin_avg.csv");
        for (var i = 100; i <= 200; i += 10)
        {
            var n = i * 100;
            Cnt = 0;

            Stoper.Start();
            for (var j = 0; j < n; j++) LinSearch(n, Tab[j]);
            Stoper.Stop();

            file.WriteLine("Iteracja: {0}; Wynik: {1}; {2}; Czas: {3}", i, n, (double) Cnt / n, Stoper.ElapsedMilliseconds);
            Console.WriteLine("Iteracja: {0}; Wynik: {1}; {2}; Czas: {3}", i, n, 1.0 * Cnt / n, Stoper.ElapsedMilliseconds);
            Stoper.Reset();
        }

        file.Close();
    }
//--------------------
    private static void BinPes()
    {
        var Stoper = new Stopwatch();
        var file = new StreamWriter("bin_pes.csv");
        for (var i = 10; i <= 28; i++)
        {
            var n = (1 << i) - 1; // 2^i - 1
            Cnt = 0;

            Stoper.Start();
            BinSearch(n, 0);
            Stoper.Stop();
            file.WriteLine("{0}; {1}; {2}; Czas: {3}", i, n, Cnt, Stoper.ElapsedMilliseconds);
            Console.WriteLine("{0}; {1}; {2}; Czas: {3}", i, n, Cnt, Stoper.ElapsedMilliseconds);
            Stoper.Reset();
        }

        file.Close();
    }
//--------------------
    private static void BinPesCzas()
    {
        var Stoper = new Stopwatch();
        var file = new StreamWriter("bin_pes_czas.csv");
        for (var i = 10; i <= 28; i++)
        {
            var n = (1 << i) - 1; // 2^i - 1
            Cnt = 0;

            Stoper.Start();
            for (var j = 0; j < 10000000; j++) BinSearch(n, 0);
            Stoper.Stop();

            file.WriteLine("Iteracja: {0}; Wynik: {1}; Czas: {2}", i, n, Stoper.ElapsedMilliseconds);
            Console.WriteLine("Iteracja: {0}; Wynik: {1}; Czas: {2}", i, n, Stoper.ElapsedMilliseconds);
            Stoper.Reset();
        }

file.Close();
    }
//--------------------
    private static void BinAvg()
    {
        var Stoper = new Stopwatch();
        var file = new StreamWriter("bin_avg.csv");
        for (var i = 10; i <= 28; i++)
        {
            var n = (1 << i) - 1; // 2^i - 1
            Cnt = 0;

            Stoper.Start();
            for (var j = 0; j < n; j++) BinSearch(n, Tab[j]);
            Stoper.Stop();

            file.WriteLine("Iteracja: {0}; Wynik: {1}; {2}; Czas: {3}", i, n, 1.0 * Cnt / n, Stoper.ElapsedMilliseconds);
            Console.WriteLine("Iteracja: {0}; Wynik: {1}; {2}; Czas: {3}", i, n, 1.0 * Cnt / n, Stoper.ElapsedMilliseconds);
        }

        file.Close();
    }
//--------------------
    private static void Main()
    {
        Console.WriteLine("Alokacja tablicy...");
        Tab = new int [C_Max];
        for (var i = 0; i < C_Max; i++) Tab[i] = i + 1;


        Console.WriteLine("LinPes:");
        LinPes();

        Console.WriteLine("LinAvg:");
        LinAvg();

        Console.WriteLine("BinAvg:");
        BinAvg();

        Console.WriteLine("BinPes:");
        BinPesCzas();
    }
}
