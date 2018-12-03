using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;

namespace aoc2018
{
    public class day2
    {
        public static void Go1()
        {
            int twos = 0;
            int threes = 0;
            string[] lines = File.ReadAllLines(@"C:\aoc2018\2\input.txt");

            foreach (string line in lines)
            {
                var dict = new Dictionary<int, int>();

                foreach (char c in line)
                {
                    if (dict.ContainsKey(c))
                    {
                        dict[c] = dict[c] + 1;
                    }
                    else
                        dict.Add(c, 1);
                }

                bool check2 = false;
                bool check3 = false;

                foreach (var item in dict)
                {
                    if (item.Value.Equals(2) && !check2)
                    {
                        twos += 1;
                        check2 = true;
                    }
                    if (item.Value.Equals(3) && !check3)
                    {
                        threes += 1;
                        check3 = true;
                    }
                }
            }

            int result = twos * threes;
            Console.WriteLine("Answer 1: {0}", result.ToString());
        }

        public static void Go2()
        {
            string[] lines = File.ReadAllLines(@"C:\aoc2018\2\input.txt");
            var combidict = new Dictionary<string, string>();

            foreach (string line in lines)
            {
                foreach (string line2 in lines)
                {
                    if (!line.Equals(line2))
                    {
                        string combination = line + '-' + line2;

                        if (!combidict.ContainsKey(combination) && !combidict.ContainsKey(line2 + '-' + line))
                        {
                            string thisres = string.Empty;

                            for (int i = 0; i < line.Length; i++)
                            {
                                if (line[i].Equals(line2[i]))
                                    thisres += line[i].ToString();
                            }
                            combidict.Add(combination, thisres);
                        }
                    }
                }
            }

            string result = combidict.Values.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);
            Console.WriteLine("Answer 2: {0}", result);
        }
    }
}
