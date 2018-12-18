using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace aoc2018
{
    class Day18
    {
        public static void Go()
        {
            string[] lines = File.ReadAllLines(@"C:\aoc2018\18\input.txt");
            int size = lines[0].Length;
            Acre[,] grid = new Acre[size, size];

            int x = 0;
            int y = 0;
            string tznd = "";
            int questionbsize = 1000000000;

            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    grid[x, y++] = new Acre(c);
                }
                x++;
                y = 0;
            }

            for (int i = 0; i < 1500; i++)
            {
                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    for (int k = 0; k < grid.GetLength(1); k++)
                    {
                        grid[j, k].setnextcontent (Itemcount(j, k, '|', grid), Itemcount(j, k, '#', grid));
                    }
                }

                foreach (Acre a in grid)
                    a.content = a.nextcontent;

                if (i == 1000)
                {
                    tznd = UniquePrint(grid);
                }
            
                if (i > 1000 && UniquePrint(grid) == tznd && questionbsize > 1500)
                {
                    while (questionbsize > 1200)
                    {
                        questionbsize = questionbsize - (i - 1000);
                    }
                }

                if (i == 9 || i == (questionbsize -1))
                {
                    Console.WriteLine("Resource value after {0} minutes: {1}", i + 1, Resourcevalue(grid));
                }
            }
        }

        private static int Resourcevalue(Acre[,] grid)
        {
            int nrTrees = 0;
            int nrLumberyard = 0;

            foreach (Acre a in grid)
            {
                if (a.content == '|')
                    nrTrees++;
                if (a.content == '#')
                    nrLumberyard++;
            }

            return nrTrees * nrLumberyard;
        }

        public static int Itemcount(int x, int y, char item, Acre[,] grid)
        {
            int result = 0;
            int size = grid.GetLength(0);

            //x-1
            if (x > 0 && y > 0 && grid[x - 1, y - 1].content == item) result++;
            if (x > 0 && grid[x - 1, y].content == item) result++;
            if (x > 0 && y < size - 1 && grid[x - 1, y + 1].content == item) result++;

            //x
            if (y > 0 && grid[x, y - 1].content == item) result++;
            if (y < size - 1 && grid[x, y + 1].content == item) result++;

            //x+1
            if (x < size - 1 && y > 0 && grid[x + 1, y - 1].content == item) result++;
            if (x < size - 1 && grid[x + 1, y].content == item) result++;
            if (x < size - 1 && y < size - 1 && grid[x + 1, y + 1].content == item) result++;
            return result;
        }

        private static void printGrid(Acre[,] grid)
        {
            //Console.Clear();
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                string help = string.Empty;
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    help = help + grid[x, y].content;
                }
                Console.WriteLine(help);
            }
        }

        private static string UniquePrint(Acre[,] grid)
        {
            StringBuilder result = new StringBuilder();
            foreach (Acre a in grid)
            {
                result.Append(a.content);
            }
            return result.ToString();
        }

        public class Acre
        {
            public Acre(char currentcontent)
            {
                content = currentcontent;
            }

            public void setnextcontent(int surroundingTrees, int surroundingLumberyards)
            {
                nextcontent = content;
                if (content == '.' && surroundingTrees >= 3)
                {
                    nextcontent = '|';
                }
                if (content == '|' && surroundingLumberyards >= 3)
                {
                    nextcontent = '#';
                }
                if (content == '#')
                {
                    if (surroundingLumberyards >= 1 && surroundingTrees >= 1)
                    {
                        nextcontent = '#';
                    }
                    else
                    {
                        nextcontent = '.';
                    }
                }
            }

            public char content { get; set; }
            public char nextcontent { get; set; }
        }
    }
}
