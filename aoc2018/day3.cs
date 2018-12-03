using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace aoc2018
{
    class day3
    {
        const int maxsize = 1000;

        public static void Go()
        {
            //string[] testrun = new string[] { "#1 @ 1,3: 4x4", "#2 @ 3,1: 4x4", "#3 @ 5,5: 2x2"};
            //var claims = testrun.Select(line => new Claim(line));

            var claims = File.ReadAllLines(@"C:\aoc2018\3\input.txt").Select(line => new Claim(line));

            int[,] playfield = Createfield(claims);
            int result = 0;

            for (int i = 0; i < maxsize; i++)
            {
                for (int j = 0; j < maxsize; j++)
                {
                    if (playfield[i, j] > 1)
                        result += 1;
                }
            }
            Console.WriteLine("Result part 1: {0}", result);

            foreach (Claim myclaim in claims)
            {
                bool allok = true;
                for (int i = myclaim.X; i < myclaim.X + myclaim.Width; i++)
                {
                    for (int j = myclaim.Y; j < myclaim.Y + myclaim.Height; j++)
                    {
                        if (playfield[i, j] > 1)
                        {
                            allok = false;
                        }
                    }
                }

                if (allok)
                    Console.WriteLine("Result part 2: {0}", myclaim.Id);
            }

            //In case of a limited testset write playground to console:
            if (claims.ToList().Count < 10)
            {
                for (int i = 0; i < 25; i++)
                {
                    string res = string.Empty;
                    for (int j = 0; j < 25; j++)
                    {
                        if (playfield[i, j] == 0)
                            res = res + " .";
                        else
                            res = res + " " + playfield[i, j];
                    }
                    Console.WriteLine(res);
                }
            }
        }

        private static int[,] Createfield(IEnumerable<Claim> claims)
        {
            int[,] playfield = new int[maxsize, maxsize];

            foreach (Claim myclaim in claims)
            {
                for (int i = myclaim.X; i < myclaim.X + myclaim.Width; i++)
                {
                    for (int j = myclaim.Y; j < myclaim.Y + myclaim.Height; j++)
                    {
                        playfield[i, j]++;
                    }
                }
            }
            return playfield;
        }

        private class Claim
        {
            public Claim(string input)
            {
                Id = input.Split('@')[0];
                input = input.Split('@')[1];

                string coor = input.Split(':')[0];
                string size = input.Split(':')[1];

                X = Convert.ToInt32(coor.Split(',')[0]);
                Y = Convert.ToInt32(coor.Split(',')[1]);

                Width = Convert.ToInt32(size.Split('x')[0]);
                Height = Convert.ToInt32(size.Split('x')[1]);
            }

            public string Id { get; }
            public int X { get; }
            public int Y { get; }
            public int Width { get; }
            public int Height { get; }
        }
    }
}
