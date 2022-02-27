using System;
using System.Diagnostics;
using System.Collections.Generic;

class Projekt2
{
    static bool JestPierwsza(ulong n)
    {
        if( n < 2 ) return false;
        else if( n < 4 ) return true;
        else if( n%2 == 0 ) return false;
        else
        {
            for(ulong i=3; i*i <= n; i+=2)
            {
                if( n%i == 0 ) return false;
            }
        }
        return true;
    }
//--------------------
    static bool JestPierwsza2(ulong n, List<ulong> lst)
    {
        if( n < 2 ) return false;
        else if( n < 4 ) return true;
        else if( n%2 == 0 ) return false;
        else
        {
            foreach(ulong p in lst)
            {
                if( p*p > n ) break;
                if( n%p == 0 ) return false;
            }
        }
        return true;
    }
//--------------------
    static List<ulong> GeneratorPierwszych(int N)
    {
        List<ulong> lst = new List<ulong>();
        if( N == 0 ) return lst;

        lst.Add(2); if( N == 1 ) return lst;
        lst.Add(3); if( N == 2 ) return lst;

        ulong x = 5;
        while(true)
        {
            //if( IsPrime(x) ) lst.Add(x);
            if( JestPierwsza(x) ) lst.Add(x);
            if( lst.Count == N ) break;
            x += 2;
        }
        return lst;
    }
//--------------------
    static List<ulong> GeneratorPierwszych2(int N)
    {
        List<ulong> lst2 = new List<ulong>();
        if( N == 0 ) return lst2;

        lst2.Add(2); if( N == 1 ) return lst2;
        lst2.Add(3); if( N == 2 ) return lst2;

        ulong x = 5;
        while(true)
        {
            //if( IsPrime(x) ) lst.Add(x);
            if( JestPierwsza2(x, lst2) ) lst2.Add(x);
            if( lst2.Count == N ) break;
            x += 2;
        }
        return lst2;
    }
//--------------------

    static void Main()
    {
        int N = 1000000;

        Stopwatch stoper = new Stopwatch();
        stoper.Start();
        List<ulong> lst = GeneratorPierwszych(N);
        stoper.Stop();

        for(int i=0; i<lst.Count && i<100; i++)
            Console.WriteLine(lst[i]);

        Console.WriteLine("Generator Pierwszych - Czas wykonania = " + stoper.ElapsedMilliseconds);

        Stopwatch st = new Stopwatch();

        st.Start();
        List<ulong> lst2 = GeneratorPierwszych2(N);
        st.Stop();

        for(int i=0; i<lst2.Count && i<100; i++)
            Console.WriteLine(lst2[i]);

        Console.WriteLine("Generator pierwszych 2 - Czas wykonania = " + st.ElapsedMilliseconds);
    }
}
