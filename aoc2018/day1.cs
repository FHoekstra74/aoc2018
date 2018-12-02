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
        public void go()
        {
            int result = 0;
            var values = File.ReadAllLines(@"C:\aoc2018\1\input.txt").Select(line => int.Parse(line));

            Console.WriteLine("Answer 1: {0}", values.Sum());

            var dict = new Dictionary<int, int>();
            bool ready = false;

            while (!ready)
            {
                foreach (int val in values)
                {
                    result += val;

                    if (dict.ContainsKey(result))
                    {
                        if (!ready)
                        {
                            Console.WriteLine("Answer 2: {0}", result.ToString());
                            ready = true;
                        }
                    }
                    else
                        dict.Add(result, 1);
                }
            }
        }
    }
 }

