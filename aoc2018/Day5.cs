using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace aoc2018
{
    class Day5
    {
        public static void Go()
        {
            string[] lines = File.ReadAllLines(@"C:\aoc2018\5\input.txt");
            string s = lines[0];
            //s = "dabAcCaCBAcCcaDA";
            Console.WriteLine("Answer a: {0}", Calulate(s));

            int lowest = 500000;
            for (char c = 'A'; c <= 'Z'; c++)
            {
                string stest = s.Replace(c.ToString(), "").Replace(c.ToString().ToLower(), "");
                int answer = Calulate(stest);
                Console.WriteLine("Answer b {0}: {1}", c, answer);

                if (answer < lowest)
                    lowest = answer;
            } 
            Console.WriteLine("Result b: {0}", lowest);
        }

        private static int Calulate(string s)
        {
            StringBuilder newstring = new StringBuilder();
            bool onefound = true;
            bool skipcurrent = false;
            while (onefound)
            {
                onefound = false;
                skipcurrent = false;
                for (int i = 1; i < s.Length; i++)
                {
                    string c = s[i].ToString();
                    string previous = s[i - 1].ToString();
                    if (c.ToUpper().Equals(previous.ToUpper()) && c != previous && !skipcurrent)
                    {
                        onefound = true;
                        skipcurrent = true;
                    }
                    else
                    {
                        if (!skipcurrent)
                        {
                            newstring.Append(previous);
                        }
                        skipcurrent = false;
                    }
                }

                if (!skipcurrent)
                {
                    newstring.Append(s[s.Length-1].ToString());
                }

                if (onefound)
                {
                    s = newstring.ToString();
                    newstring = new StringBuilder();
                }
            }
            return newstring.Length;
        }
    }
}
