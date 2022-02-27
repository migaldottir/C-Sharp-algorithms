using System;
using System.Diagnostics;

internal class Projekt3
{
    private static void InsertionSort(int[] T) // proste wstawianie
    {
        var Stoper = new Stopwatch();
        var n = T.Length;
        Stoper.Start();
        for (var i = 1; i < n; i++)
        {
            var j = i; // elementy 0 .. i-1 są już posortowane
            var Buf = T[j]; // bierzemy i-ty (j-ty) element
            while (j > 0 && T[j - 1] > Buf)
            {
                // przesuwamy elementy
                T[j] = T[j - 1];
                j--;
            }
            T[j] = Buf; // i wpisujemy na docelowe miejsce
        }
        Stoper.Stop();
        ShowAfter(T);
        Console.WriteLine("Czas wynosi: {0}", Stoper.Elapsed);
        Stoper.Reset();
    }
//---------------
    private static void SelectionSort(int[] T) // proste wybieranie
    {
        var Stoper = new Stopwatch();
        var n = T.Length;
        Stoper.Start();
        for (var i = 0; i < n - 1; i++)
        {
            var Buf = T[i]; // bierzemy i-ty element
            var k = i; // i jego indeks
            for (var j = i + 1; j < n; j++)
                if (T[j] < Buf) // szukamy najmniejszego z prawej
                {
                    k = j;
                    Buf = T[j];
                }
            T[k] = T[i]; // zamieniamy i-ty z k-tym
            T[i] = Buf;
        }
        Stoper.Stop();
        ShowAfter(T);
        Console.WriteLine("Czas wynosi: {0}", Stoper.Elapsed);
        Stoper.Reset();
    }
//---------------
    private static void CocktailSort(int[] T) // koktailowe
    {
        var Stoper = new Stopwatch();
        var n = T.Length;
        var Left = 1;
        var Right = n - 1;
        var k = n - 1;
        Stoper.Start();
        do
        {
            for (var j = Right; j >= Left; j--) // przesiewanie od dołu
                if (T[j - 1] > T[j])
                {
                    var Buf = T[j - 1];
                    T[j - 1] = T[j];
                    T[j] = Buf;
                    k = j; // zamiana elementów i zapamiętanie indeksu
                }
            Left = k + 1; // zacieśnienie lewej granicy
            for (var j = Left; j <= Right; j++) // przesiewanie od góry
                if (T[j - 1] > T[j])
                {
                    var Buf = T[j - 1];
                    T[j - 1] = T[j];
                    T[j] = Buf;
                    k = j; // zamiana elementów i zapamiętanie indeksu
                }
            Right = k - 1; // zacieśnienie prawej granicy
        } while (Left <= Right);
        Stoper.Stop();
        ShowAfter(T);
        Console.WriteLine("Czas wynosi: {0}", Stoper.Elapsed);
        Stoper.Reset();
    }
//--------------- Dodatek do HeapSort
    private static void Heapify(int[] T, uint left, uint right)
    {
        // procedura budowania/naprawiania kopca
        uint i = left, j = 2 * i + 1;
        var buf = T[i]; // ojciec
        while (j <= right) // przesiewamy do dna stogu
        {
            if (j < right) // wybieramy większego syna
                if (T[j] < T[j + 1])
                    j++;
            if (buf >= T[j]) break;
            T[i] = T[j];
            i = j;
            j = 2 * i + 1; // przechodzimy do dzieci syna
        }
        T[i] = buf;
    }
//---------------
    private static void HeapSort(int[] T)
    {
        var Stoper = new Stopwatch();
        var left = (uint) T.Length / 2;
        var right = (uint) T.Length - 1;
        Stoper.Start();
        while (left > 0) // budujemy kopiec idąc od połowy tablicy
        {
            left--;
            Heapify(T, left, right);
        }
        while (right > 0) //rozbieramy kopiec
        {
            var buf = T[left];
            T[left] = T[right];
            T[right] = buf; // największy element
            right--; // kopiec jest mniejszy
            Heapify(T, left, right); // ale trzeba go naprawić
        }
        Stoper.Stop();
        ShowAfter(T);
        Console.WriteLine("Czas wynosi: {0}", Stoper.Elapsed);
        Stoper.Reset();
    }
//--------------- GENEROWANIE TABLIC
    private static void ShowBefore(int[] T)
    {
        var n = T.Length;
        var s = "";
        for (var i = 0; i < n; i++)
        {
            s += T[i];
            if (i < n - 1) s += ", ";
        }
        Console.Write("Tablica przed sortowaniem: ");
        Console.WriteLine(s);
    }
//---------------
    private static void ShowAfter(int[] T)
    {
        var n = T.Length;
        var s = "";
        for (var i = 0; i < n; i++)
        {
            s += T[i];
            if (i < n - 1) s += ", ";
        }
        Console.Write("Tablica po sortowaniu: ");
        Console.WriteLine(s);
    }
//---------------
    private static bool GenRozkladRosnacy(int[] T, int N1, int N2, Random rnd)
    {
        if (N2 <= N1) return false;
        var n = T.Length;
        var d = (N2 - N1 + 1) / n;

        if (d < 1) return false;

        var p1 = N1;
        var p2 = N1 + d - 1;
        for (var i = 0; i < n; i++)
        {
            if (i == n - 1) p2 = N2;
            //Console.WriteLine("{0}, {1}", p1, p2);
            T[i] = rnd.Next(p1, p2 + 1);
            p1 += d;
            p2 += d;
        }
        return true;
    }

//---------------
    private static bool GenRozkladRosnacy(int[] T, int i0, int n, int N1, int N2, Random rnd)
    {
        if (N2 <= N1) return false;
        if (i0 < 0 || i0 + n > T.Length) return false;

        var d = (N2 - N1 + 1) / n;

        if (d < 1) return false;

        var p1 = N1;
        var p2 = N1 + d - 1;
        for (var i = 0; i < n; i++)
        {
            if (i == n - 1) p2 = N2;

            T[i0 + i] = rnd.Next(p1, p2 + 1);
            p1 += d;
            p2 += d;
        }
        return true;
    }
//---------------
    private static void Odwroc(int[] T)
    {
        var n = T.Length;
        var m = n / 2;
        for (var i = 0; i < m; i++)
        {
            var buf = T[i];
            T[i] = T[n - 1 - i];
            T[n - 1 - i] = buf;
        }
    }
//---------------
    private static bool Odwroc(int[] T, int i0, int n)
    {
        if (i0 < 0 || i0 + n > T.Length) return false;
        var m = n / 2;
        for (var i = 0; i < m; i++)
        {
            var buf = T[i0 + i];
            T[i0 + i] = T[i0 + n - 1 - i];
            T[i0 + n - 1 - i] = buf;
        }
        return true;
    }

//---------------
    private static bool GenRozkladMalejacy(int[] T, int N1, int N2, Random rnd)
    {
        var res = GenRozkladRosnacy(T, N1, N2, rnd);
        if (!res) return false;

//Array.Reverse(T);
        Odwroc(T);
        return true;
    }
//---------------
    private static bool GenRozkladMalejacy(int[] T, int i0, int n, int N1, int N2, Random rnd)
    {
        var res = GenRozkladRosnacy(T, i0, n, N1, N2, rnd);
        if (!res) return false;

        if (!Odwroc(T, i0, n)) return false;
        return true;
    }
//---------------
    private static bool GenRozkladV(int[] T, int N1, int N2, Random rnd)
    {
        var n = T.Length;
        var m = n / 2;

        var res = GenRozkladMalejacy(T, 0, m, N1, N2, rnd);
        if (!res) return false;

        res = GenRozkladRosnacy(T, m, n - m, N1, N2, rnd);
        return res;
    }
//---------------
    private static void GenRozkladStaly(int[] T, Random rnd)
    {
        T[0] = rnd.Next(1, 200000);
        for (var i = 0; i < T.Length; i++) T[i] = T[0];
    }
//---------------
    private static void Main()
    {
        var rnd = new Random();
        var st = new Stopwatch();
        var N = 20;
        var N1 = 50000;
        var N2 = 200000;

        var T = new int[N];

        Console.WriteLine("Start...");
        if (!GenRozkladMalejacy(T, 1, 200000, rnd))
        {
            Console.WriteLine("Error..");
            return;
        }

        //Insertion Sort
        Console.WriteLine("Sortowanie rosnące, Insertion Sort:");
        ShowBefore(T); //Tablica przed sortowaniem
        GenRozkladRosnacy(T, N1, N2, rnd);
        InsertionSort(T);

        Console.WriteLine("Sortowanie malejące - Insertion Sort:");
        ShowBefore(T); //Tablica przed sortowaniem
        GenRozkladMalejacy(T, N1, N2, rnd);
        InsertionSort(T);

        Console.WriteLine("Rozkład Stały - Insertion Sort:");
        ShowBefore(T); //Tablica przed sortowaniem
        GenRozkladStaly(T, rnd);
        InsertionSort(T);

        Console.WriteLine("Rozkład V - Insertion Sort: ");
        ShowBefore(T); //Tablica przed sortowaniem
        GenRozkladV(T, N1, N2, rnd);
        InsertionSort(T);

        //Selection Sort
        Console.WriteLine("Sortowanie rosnące - Selection Sort:");
        ShowBefore(T); //Tablica przed sortowaniem
        GenRozkladRosnacy(T, N1, N2, rnd);
        SelectionSort(T);

        Console.WriteLine("Sortowanie malejące - Selection Sort:");
        ShowBefore(T); //Tablica przed sortowaniem
        GenRozkladMalejacy(T, N1, N2, rnd);
        SelectionSort(T);

        Console.WriteLine("Rozkład Stały - Selection Sort:");
        ShowBefore(T); //Tablica przed sortowaniem
        GenRozkladStaly(T, rnd);
        SelectionSort(T);

        Console.WriteLine("Rozkład V - Selection Sort:");
        ShowBefore(T); //Tablica przed sortowaniem
        GenRozkladV(T, N1, N2, rnd);
        SelectionSort(T);

        //Coctail Sort
        Console.WriteLine("Sortowanie rosnące - Coctail Sort:");
        ShowBefore(T); //Tablica przed sortowaniem
        GenRozkladRosnacy(T, N1, N2, rnd);
        CocktailSort(T);

        Console.WriteLine("Sortowanie malejące - Coctail Sort:");
        ShowBefore(T); //Tablica przed sortowaniem
        GenRozkladMalejacy(T, N1, N2, rnd);
        CocktailSort(T);

        Console.WriteLine("Rozkład Stały - Coctail Sort:");
        ShowBefore(T);
        GenRozkladStaly(T, rnd);
        CocktailSort(T);

        Console.WriteLine("Rozkład V - Coctail Sort:");
        ShowBefore(T); //Tablica przed sortowaniem
        GenRozkladV(T, N1, N2, rnd);
        CocktailSort(T);

        //Heap Sort
        Console.WriteLine("Sortowanie rosnące - Heap Sort:");
        ShowBefore(T); //Tablica przed sortowaniem
        GenRozkladRosnacy(T, N1, N2, rnd);
        HeapSort(T);

        Console.WriteLine("Sortowanie malejące - Heap Sort:");
        ShowBefore(T); //Tablica przed sortowaniem
        GenRozkladMalejacy(T, N1, N2, rnd);
        HeapSort(T);

        Console.WriteLine("Rozkład Stały - Heap Sort:");
        ShowBefore(T); //Tablica przed sortowaniem
        GenRozkladStaly(T, rnd);
        HeapSort(T);

        Console.WriteLine("Rozkład V - Heap Sort:");
        ShowBefore(T); //Tablica przed sortowaniem
        GenRozkladV(T, N1, N2, rnd);
        HeapSort(T);
    }
}
