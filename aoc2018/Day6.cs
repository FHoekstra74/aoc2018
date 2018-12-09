using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

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
            Random rnd = new Random();
            char id = 'a';
            foreach (Point point in points)
            {
                point.id = id++;
                point.color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                grid[point.x, point.y] = char.ToUpper(point.id);
            }

            int answer2count = 0;
            List<Point> savePoints = new List<Point>();
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
                    {
                        answer2count += 1;
                        savePoints.Add(new Point(string.Format ("{0}, {1}",x,y)));
                    }

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
            for (int x = 0; x < maxx; x++)
            {
                if (!edges.Contains(grid[x, 0]))
                    edges.Add(grid[x, 0]);
                if (!edges.Contains(grid[x, maxy]))
                    edges.Add(grid[x, maxy]);
            }
            for (int y = 0; y < maxy; y++)
            {
                if (!edges.Contains(grid[0, y]))
                    edges.Add(grid[0, y]);
                if (!edges.Contains(grid[maxx, y]))
                    edges.Add(grid[maxx, y]);
            }

            foreach (char c in edges)
            {
                if (c != '.')
                {
                    points.First(p => p.id == c).isedge = true;
                }
            }

            int max = 0;
            foreach (Point point in points)
            {
                if (!point.isedge && point.count > max)
                {
                    max = point.count;
                }
            }

            int minx = points.Min(c => c.x) -1;
            int miny = points.Min(c => c.y) -1;

            using (Bitmap bitmap = new Bitmap(maxx - minx + 2, maxy - miny + 2))
            {
                for (int x = minx; x < maxx; x++)
                {
                    for (int y = miny; y < maxy; y++)
                    {
                        char val = grid[x, y];
                        if (val != '.')
                        {
                            Point p = points.First(p1 => p1.id == val);
                            if (!p.isedge)
                            {
                                if (savePoints.Any(p1 => p1.x==x && p1.y==y))
                                    bitmap.SetPixel(x - minx, y-miny, ChangeColorBrightness( p.color, (float)-0.5));
                                else
                                    bitmap.SetPixel(x - minx, y - miny, p.color);
                            }
                            else
                            {
                                if (savePoints.Any(p1 => p1.x == x && p1.y == y))
                                    bitmap.SetPixel(x-minx , y-miny, ChangeColorBrightness(p.color,(float)-0.5));
                                else
                                    bitmap.SetPixel(x - minx, y - miny, ChangeColorBrightness(p.color, (float)0.85));
                            }
                        }
                        else
                            bitmap.SetPixel(x-minx, y-miny, Color.White);
                    }
                }
                foreach (Point point in points)
                {
                    bitmap.SetPixel(point.x -minx, point.y -miny, Color.Black);
                }

                bitmap.Save(@"C:\aoc2018\6\image.bmp", ImageFormat.Bmp);
                bitmap.Save(@"C:\aoc2018\6\image.jpg", ImageFormat.Jpeg);
            }

            Console.WriteLine("Answer1: {0}", max);
            Console.WriteLine("Answer2: {0}", answer2count);
        }

        public static Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
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
            public Color color { get; set; }
        }
    }
}
