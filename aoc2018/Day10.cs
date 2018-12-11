using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace aoc2018
{
    public class Day10
    {
        public static void Go()
        {
            var points = File.ReadAllLines(@"C:\aoc2018\10\input.txt").Select(line => new Point(line)).ToList() ;

            bool cont = true;
            int step = 0;

            while (cont)
            {
                int maxx = points.Max(p => p.Positionx);
                int minx = points.Min(p => p.Positionx);

                if ((maxx - minx) < Console.WindowWidth)
                {
                    int maxy = points.Max(p => p.Positiony);
                    int miny = points.Min(p => p.Positiony);

                    Console.Clear();
                    for (int y = 0; y <= maxy - miny; y++)
                    {
                        string output = string.Empty;
                        for (int x = 0; x <= maxx - minx; x++)
                        {
                            if (points.Any(p => (p.Positionx - minx) == x && (p.Positiony - miny) == y))
                                output += "*";
                            else
                                output += " ";
                        }
                        Console.WriteLine(output);
                    }
                }

                points.ForEach(p => p.Nextpos());
                
                int maxxnew = points.Max(p => p.Positionx);
                int minxnew = points.Min(p => p.Positionx);

                if ((maxxnew - minxnew) > (maxx - minx))
                {
                    cont = false;
                    Console.WriteLine("Done in {0} steps" , step);
                }
                step++;
            }
        }

        public class Point
        {
            public Point(string input)
            {
                string[] ugly = input.Split(',');
                Positionx = Convert.ToInt32(ugly[0].Split('<')[1]);
                Positiony = Convert.ToInt32(ugly[1].Split('>')[0]);
                Velocityx = Convert.ToInt32(ugly[1].Split('<')[1]);
                Velocityy = Convert.ToInt32(ugly[2].Split('>')[0]);
            }

            public void Nextpos()
            {
                Positionx += Velocityx;
                Positiony += Velocityy;
            }

            public int Positionx { get; set; }
            public int Positiony { get; set; }
            public int Velocityx { get; }
            public int Velocityy { get; }
        }
    }
}
