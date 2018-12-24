using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;

namespace aoc2018
{
    class Day17
    {
        public static void Go()
        {
            string[] lines = File.ReadAllLines(@"C:\aoc2018\17\input.txt");
      
            int size = 2000;
            char[,] grid = new char[size, size];

            int minx = int.MaxValue;
            int maxx = -1;
            int maxy = -1;
            int miny = int.MaxValue;

            foreach (string line in lines)
            {
                string[] split = line.Split(',');
                char single = split[0][0];
                int leftvalue = Convert.ToInt32(split[0].Split('=')[1].Trim());
                int firstrightvalue = Convert.ToInt32(split[1].Split('=')[1].Split('.')[0].Trim());
                int secondrightvalue = Convert.ToInt32(split[1].Split('=')[1].Split('.')[2].Trim());

                for (int i = firstrightvalue; i <= secondrightvalue; i++)
                {
                    if (single == 'x')
                    {
                        if (leftvalue < minx)
                            minx = leftvalue;
                        if (leftvalue > maxx)
                            maxx = leftvalue;
                        if (i > maxy)
                            maxy = i;
                        if (i < miny)
                            miny = i;
                        grid[leftvalue, i] = '#';
                    }
                    else
                    {
                        if (i < minx)
                            minx = i;
                        if (i > maxx)
                            maxx = i;
                        if (leftvalue > maxy)
                            maxy = leftvalue;
                        if (leftvalue < miny)
                            miny = leftvalue;
                        grid[i, leftvalue] = '#';
                    }
                }

                grid[500, 0] = '+';
            }

            Queue<Point> waterspring = new Queue<Point>();
            waterspring.Enqueue(new Point(500, 0));

            bool cont = true;
            while (cont)
            {
                if (waterspring.Count == 0)
                    cont = false;
                else
                {
                    spring(grid, waterspring.Dequeue(), waterspring, maxx, maxy);
                }
            }

            printGrid(grid, minx,maxx,maxy);

            int result = 0;
            int resultb = 0;
            foreach (char c in grid)
            {
                if (c == '~' || c == '|')
                    result += 1;
                if (c == '~')
                    resultb += 1;
            }
            result = result - miny + 1;
            Console.WriteLine("Anser a: {0}", result);
            Console.WriteLine("Anser b: {0}", resultb);
        }

        static void spring(char[,] grid, Point springpoint, Queue<Point> waterspring, int maxx, int maxy)
        {
            int x = springpoint.X;
            int y = springpoint.Y + 1;
            bool cont = true;
            while (cont)
            {
                if (y > maxy)
                {
                    cont = false;
                }
                else
                {
                    if (grid[x, y] == '#')
                    {
                        fill(grid, new Point(x, y - 1), waterspring, maxx);
                        cont = false;
                    }
                    else
                    {
                        if (grid[x, y] == '|')
                        {
                            cont = false;
                        }
                        else
                        {
                            grid[x, y] = '|';
                            y++;
                        }
                    }
                }
            }
        }

        static void fill(char[,] grid, Point startpoint, Queue<Point> waterspring, int maxx)
        {
            bool cont = true;
            int x = startpoint.X;
            int y = startpoint.Y;
            while (cont)
            {

                bool leftfound = false;
                bool rightfound = false;
                bool fill = true;
                int leftx = x;
                int rightx = x;

                while (!leftfound)
                {
                    if (leftx < 0)
                    {
                        leftfound = true;
                        fill = false;
                    }
                    else

                    {
                        if (grid[leftx, y] == '#')
                        {
                            leftfound = true;
                        }
                        else
                        {
                            leftx--;
                        }
                    }
                }
                while (!rightfound)
                {
                    if (rightx > maxx)
                    {
                        rightfound = true;
                        fill = false;
                    }
                    else
                    {
                        if (grid[rightx, y] == '#')
                        {
                            rightfound = true;
                        }
                        else
                        {
                            rightx++;
                        }
                    }
                }

                for (int i = leftx + 1; i < rightx; i++)
                {
                    if (grid[i, y +1] == '\0')
                    { fill = false; }
                }

                if (fill)
                {
                    for (int i = leftx + 1; i < rightx; i++)
                    {
                        grid[i, y] = '~';
                    }
                }
                else
                {
                    leftfound = false;
                    rightfound = false;
                    leftx = x;
                    rightx = x;

                    grid[x, y] = '|';

                    while (!leftfound)
                    {
                        if (grid[leftx -1, y + 1] == '\0')
                        {
                            waterspring.Enqueue(new Point(leftx - 1, y));
                            grid[leftx - 1, y] = '|';
                            leftfound = true;
                        }
                        else
                        {
                            if (grid[leftx - 1, y] == '#'|| (grid[leftx - 1, y] == '|' && grid[leftx - 1, y+1] == '|'))
                            { leftfound = true; }
                            else
                            {
                                grid[leftx - 1, y] = '|';
                                leftx--;
                            }
                        }
                    }

                    while (!rightfound)
                    {
                        if (grid[rightx + 1, y + 1] == '\0')
                        {
                            waterspring.Enqueue(new Point(rightx + 1, y));
                            grid[rightx + 1, y] = '|';
                            rightfound = true;
                        }
                        else
                        {
                            if (grid[rightx + 1, y] == '#' || (grid[rightx + 1, y] == '|' && grid[rightx + 1, y+1] == '|'))
                            { rightfound = true; }
                            else
                            {
                                grid[rightx + 1, y] = '|';
                                rightx++;
                            }
                        }
                    }
                    cont = false;
                }
                y--;
            }
        }

        static void printGrid(char[,] grid, int minx, int maxx, int maxy)
        {
            using (StreamWriter outputFile = new StreamWriter(@"C:\aoc2018\17\output.txt"))
            {
                for (int y = 0; y < maxy + 6; y++)
                {
                    string help = string.Empty;
                    for (int x = minx -2; x <= maxx + 2; x++)
                    {
                        if (grid[x, y] == '\0')
                            help = help + '.';
                        else
                        {
                            help = help + grid[x, y];
                        }
                    }
                    outputFile.WriteLine(help);
                  //  Console.WriteLine(help);
                }
            }
        }

    }
}
