using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace aoc2018
{
    public class day1
    {
        public static void Go()
        {
            int result = 0;
            var values = File.ReadAllLines(@"C:\aoc2018\1\input.txt").Select(line => int.Parse(line));

            Console.WriteLine("Answer 1: {0}", values.Sum());

            var dict = new Dictionary<int, int>();

            while (true)
            {
                foreach (int val in values)
                {
                    result += val;

                    if (dict.ContainsKey(result))
                    {
                        Console.WriteLine("Answer 2: {0}", result.ToString());
                        return;
                    }
                    else
                        dict.Add(result, 1);
                }
            }
        }
    }
 }

