using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace aoc2018
{
    class Day6
    {
        public static void Go()
        {
            List<Point> points = File.ReadAllLines(@"C:\aoc2018\6\input.txt").Select(line => new Point(line)).ToList();

            int maxx = points.Max(c => c.x);
            int maxy = points.Max(c => c.y);

            char[,] grid = new char[maxx + 1, maxy + 1];

            char id = 'a';
            foreach (Point point in points)
            {
                point.id = id++;
                grid[point.x, point.y] = char.ToUpper(point.id);
            }

            int answer2count = 0;

            for (int x = 0; x <= maxx; x++)
            {

                for (int y = 0; y <= maxy; y++)
                {
                    var dist = new Dictionary<char, int>();
                    int total = 0;

                    foreach (Point point in points)
                    {
                        dist[point.id] = CalculateManhattanDistance(point.x, x, point.y, y);
                        total += dist[point.id];
                    }

                    if (total < 10000)
                        answer2count += 1;

                    var closest = dist.Where(p => p.Value == dist.Values.Min());
                    if (closest.Count() == 1)
                    {
                        grid[x, y] = closest.FirstOrDefault().Key;
                        points.First(p => p.id == closest.FirstOrDefault().Key).count++;
                    }
                    else
                        grid[x, y] = '.';
                }
            }

            List<char> edges = new List<char>();
            for (var x = 0; x < maxx; x++)
            {
                if (!edges.Contains(grid[x, 0]))
                {
                    edges.Add(grid[x, 0]);
                }
                if (!edges.Contains(grid[x, maxy]))
                {
                    edges.Add(grid[x, maxy]);
                }
            }
            for (var y = 0; y < maxy; y++)
            {
                if (!edges.Contains(grid[0, y]))
                {
                    edges.Add(grid[0, y]);
                }
                if (!edges.Contains(grid[maxx, y]))
                {
                    edges.Add(grid[maxx, y]);
                }
            }

            foreach (char c in edges)
            {
                //Console.WriteLine(c.ToString());
                if (c !=  '.')
                {
                    points.First(p => p.id == c).isedge = true;
                }
            }

            int max = 0;
            foreach (Point point in points)
            {
//                Console.WriteLine("id: {0} count: {1}", point.id, point.count);
                if (!point.isedge)
                {
                    if (point.count > max)
                        max = point.count;
                }
            }
            
            Console.WriteLine("Answer1: {0}", max);
            Console.WriteLine("Answer2: {0}", answer2count);
        }

        public static int CalculateManhattanDistance(int x1, int x2, int y1, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        private class Point
        {
            public Point(string input)
            {
                x = Convert.ToInt16(input.Split(',')[0]);
                y = Convert.ToInt16(input.Split(',')[1]);
                count = 0;
                isedge = false;
            }

            public int x { get; }

            public int y { get; }
            public char id { get; set; }
            public int count { get; set; }
            public bool isedge { get; set; }
        }
    }
}
