using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc2018
{
    class Day9
    {
        public static void Go()
        {
            int players = 400;
            int marbles = 71864;
            naivesolution(players, marbles);
            lessnaivesolution(players, marbles);
            lessnaivesolution(players, marbles * 100 );
        }

        private static void lessnaivesolution(int players, int marbles)
        {
            LinkedList<int> playfield = new LinkedList<int>();
            Dictionary<int, long> score = new Dictionary<int, long>();

            LinkedListNode<int> current = playfield.AddLast(0);
            int currentmable = 1;
            int currentplayer = 1;

            while (currentmable <= marbles)
            {
                if ((currentmable % 23) == 0)
                {
                    if (!score.ContainsKey(currentplayer))
                        score.Add(currentplayer, 0);

                    for (int i = 0; i < 7; i++)
                    {
                        if (current.Previous == null)
                            current = playfield.Last;
                        else
                            current = current.Previous;
                    }

                    //Remove current
                    score[currentplayer] += currentmable;
                    score[currentplayer] += current.Value;

                    LinkedListNode<int> newcurrent = current;
                    if (newcurrent.Next == null)
                        newcurrent = playfield.First;
                    else
                        newcurrent = current.Next;

                    playfield.Remove(current);
                    current = newcurrent;
                } 
                else
                {
 
                    if (current.Next == null)
                        current = playfield.First;
                    else
                        current = current.Next;

                    current = playfield.AddAfter(current, currentmable);
                }

                if (marbles < 30)
                {
                    string help = "[" + currentplayer + "]  ";
                    help = help.Substring(0, 4);
                    foreach (int m in playfield)
                    {
                        if (m == current.Value)
                            help = help + ("(" + m + ")    ").Substring(0, 4);
                        else
                            help = help + (" " + m + "     ").Substring(0, 4);
                    }
                    Console.WriteLine(help);
                }

                currentmable++;
                currentplayer++;
                if (currentplayer > players)
                    currentplayer = 1;
            }

            long maxvalue = score.Values.Max();
            Console.WriteLine("Max value a {0}", maxvalue);

        }

        private static void naivesolution(int players, int marbles)
        {
            List<int> playfield = new List<int>();

            Dictionary<int, int> score = new Dictionary<int, int>();

            playfield.Add(0);
            int currentmable = 1;
            int currentpos = 0;
            int currentplayer = 1;
 
            while (currentmable <= marbles)
            {
                if (currentmable < 2)
                {
                    playfield.Add(currentmable);
                    currentpos = currentmable;
                }
                else
                {
                    if ((currentmable % 23) == 0)
                    {
                        if (!score.ContainsKey(currentplayer))
                            score.Add(currentplayer, 0);

                        int pos = 0;
                        pos = currentpos - 7;
                        if (pos < 0)
                        {
                            pos = playfield.Count + pos;
                        }

                        int val = playfield[pos];
                        playfield.Remove(val);
                        currentpos = pos;

                        score[currentplayer] += currentmable;
                        score[currentplayer] += val;
                    }
                    else
                    {
                        int newpos = 0;
                        newpos = currentpos + 2;
                        if (newpos > playfield.Count)
                        {
                            newpos = newpos - playfield.Count;
                        }

                        playfield.Insert(newpos, currentmable);
                        currentpos = newpos;
                    }

                }
                if (marbles < 30)
                {
                    int j = 0;
                    string help = "[" + currentplayer + "]  ";
                    help = help.Substring(0, 4);
                    foreach (int m in playfield)
                    {
                        if (j == currentpos)
                            help = help + ("(" + m + ")    ").Substring(0, 4);
                        else
                            help = help + (" " + m + "     ").Substring(0, 4);
                        j++;
                    }
                    Console.WriteLine(help);
                }

                currentmable++;
                currentplayer++;
                if (currentplayer > players)
                    currentplayer = 1;
            }

            int maxvalue = score.Values.Max();
            Console.WriteLine("Max value a {0}", maxvalue);
        }
    }
}
