using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace aoc2018
{
    public class Day12
    {
        public static void Go()
        {
            var lines = File.ReadAllLines(@"C:\aoc2018\12\input.txt");
            string initialstate = lines[0].Split(':')[1].Trim();
            Dictionary<string, string> notes = lines.Where((x, idx) => idx>1).Select(l => new note(l)).ToDictionary(v => v.left, v => v.right);
            int addedleft = 0;
            initialstate = extendifneeded(initialstate, ref addedleft);

            for (int i = 0 ; i < 100; i++)
            {
                string test = string.Empty;
                string newlist = "..";
                for (int j = 2; j < initialstate.Length -2; j++)
                {
                    test = initialstate[j - 2].ToString() + initialstate[j - 1].ToString() + initialstate[j].ToString() + initialstate[j + 1].ToString() + initialstate[j + 2].ToString();

                    string newval;
                    if (notes.TryGetValue(test, out newval))
                        newlist += newval;
                    else
                        newlist += '.';

                }
                initialstate = extendifneeded(newlist, ref addedleft);

                if (i == 19 || i >= 97)
                {
                    int count = 0;
                    for (int j = 0; j < initialstate.Length; j++)
                    {
                        if (initialstate[j].Equals('#'))
                        {
                            count += (j - addedleft);
                        }
                    }
                    Console.WriteLine("After {0} iterations: {1}", i+1 , count);
                }
            }

            //Saw that the string didnt change anymore after just less then 100 iterations, only shifted, so can extend answer from 98 to 50000000000
            long answerb = 5604 + ((50000000000 - 98) * 40);
            Console.WriteLine("Answer b: {0}", answerb);
        }

        private static string extendifneeded(string thelist, ref int added)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!thelist.StartsWith("..."))
                {
                    thelist = "." + thelist;
                    added++;
                }
                if (!thelist.EndsWith("..."))
                {
                    thelist = thelist + ".";
                }
            }
            return thelist;
        }

        public class note
        {
            public note(string input)
            {
                left = input.Split("=>")[0].Trim();
                right = input.Split("=>")[1].Trim();
            }

            public string left { get; }
            public string right { get; }
        }
    }
}
