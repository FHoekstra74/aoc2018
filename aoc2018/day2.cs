using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace aoc2018
{
    class day2
    {
        public void go1()
        {
            int twos = 0;
            int threes = 0;
            string[] lines = System.IO.File.ReadAllLines(@"C:\aoc2018\2\input.txt");

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

        public void go2()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\aoc2018\2\input.txt");
            string[] lines2 = lines;

            var combidict = new Dictionary<string, string>();

            foreach (string line in lines)
            {
                foreach (string line2 in lines2)
                {
                    if (!line.Equals(line2))
                    {
                        string combination = line + '-' + line2;

                        if (!combidict.ContainsKey(combination))
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

            int result = 0;
            string bestmacht = "";

            foreach (var item in combidict)
            {
                if (item.Value.Length > result)
                {
                    result = item.Value.Length;
                    bestmacht = item.Value;
                }
            }

            Console.WriteLine("Answer 1: {0}", bestmacht);
        }
    }
}
