using System;
using System.Collections.Generic;
using System.Text;

namespace aoc2018
{
    class Day11
    {
        public static void Go()
        {
            int serialnr = 6303;
            int[,] grid = new int[300, 300];

            for (int x = 1; x < 300; x++)
            {
                for (int y = 1; y < 300; y++)
                {
                    grid[x, y] = Powerlevel(x, y, serialnr);
                }
            }

            int maxx = 0;
            int maxy = 0;
            int maxtotal = 0;
            int maxsize = 0;
            for (int size = 1; size< 300; size++)
            {
                int[] res = total(grid, size);
                if (res[2] > maxtotal)
                {
                    maxx = res[0];
                    maxy = res[1];
                    maxtotal = res[2];
                    maxsize = size;
                }
            }
            Console.WriteLine("Answer b {0} -> {1},{2},{3}", maxtotal, maxx,maxy, maxsize);
        }

        private static int[] total(int[,] grid, int size)
        {
            int max = 0;
            int maxx = 0;
            int maxy = 0;

            for (int x = 1; x < 300-size; x++)
            {
                for (int y = 1; y < 300-size; y++)
                {
                    int res = 0;
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            res += grid[x + i, y + j];
                        }
                    }
                    if (res > max)
                    {
                        max = res;
                        maxx = x;
                        maxy = y;
                    }
                }
            }
            if (size == 3)
                Console.WriteLine("Answer a : max:{0}: coor: {1},{2}", max, maxx, maxy);

            int[] result = new int[3];
            result[0] = maxx;
            result[1] = maxy;
            result[2] = max;

            return result;
        }

        private static int Powerlevel(int x, int y, int serialnr)
        {
            int rackid = x + 10;
            int res = ((rackid * y) + serialnr) * rackid;
            int hundreddig = Math.Abs(res / 100 % 10);
            return hundreddig -5;
        }
    }
}
