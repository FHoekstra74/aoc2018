using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace aoc2018
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();
            Day8.Go();
            watch.Stop();

            Console.WriteLine("Time taken: {0} ms", watch.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
