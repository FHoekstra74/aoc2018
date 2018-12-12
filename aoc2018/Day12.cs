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
            Dictionary<string, string> notes = lines.Where((x, idx) => idx>1).Select(l => new Note(l)).ToDictionary(v => v.Left, v => v.Right);
            int addedleft = 0;
            initialstate = Extendifneeded(initialstate, ref addedleft);
            int[] answers = new int[101];
            for (int i = 0 ; i < 100; i++)
            {
                string test = string.Empty;
                string newlist = "..";
                for (int j = 2; j < initialstate.Length -2; j++)
                {
                    test = initialstate.Substring(j - 2, 5);

                    string newval;
                    if (notes.TryGetValue(test, out newval))
                        newlist += newval;
                    else
                        newlist += '.';
                }
                initialstate = Extendifneeded(newlist, ref addedleft);

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
                    answers[i + 1] = count;
                }
            }

            Console.WriteLine("Answer a: {0}", answers[20]);

            //Saw that the string didnt change anymore after just less then 100 iterations, only shifted, so can extend answer from 98 to 50000000000
            int stablepoint = 98;
            long answerb = answers[stablepoint] + ((50000000000 - stablepoint) * (answers[stablepoint + 1] - answers[stablepoint]));
            Console.WriteLine("Answer b: {0}", answerb);
        }

        private static string Extendifneeded(string thelist, ref int added)
        {
            while (!thelist.StartsWith("..."))
            {
                thelist = "." + thelist;
                added++;
            }
            while (!thelist.EndsWith("..."))
            {
                thelist = thelist + ".";
            }
            return thelist;
        }

        public class Note
        {
            public Note(string input)
            {
                Left = input.Split("=>")[0].Trim();
                Right = input.Split("=>")[1].Trim();
            }

            public string Left { get; }
            public string Right { get; }
        }
    }
}
