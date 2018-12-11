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
            Day11.Go();
            watch.Stop();

            Console.WriteLine("Time taken: {0} ms ({1} minutes)", watch.ElapsedMilliseconds, watch.Elapsed.Minutes);
            Console.ReadKey();
        }
    }
}
