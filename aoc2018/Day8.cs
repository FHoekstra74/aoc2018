using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace aoc2018
{
    class Day8
    {
        public static void Go()
        {
            string[] lines = File.ReadAllLines(@"C:\aoc2018\8\input.txt");
            string input = lines[0];
            //input = "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";

            List<int> thelist = input.Split(" ").Select(item => int.Parse(item)).ToList();
            int startpos = 0;
            Node startnode = readNodepart(thelist, ref startpos);

            Console.WriteLine("result a: {0}", startnode.sumMeta());
            Console.WriteLine("result b: {0}", startnode.value());
        }

        public static Node readNodepart(List<int> numbers, ref int pos)
        {
            Node node = new Node();
            int children = numbers[pos++];
            int metadata = numbers[pos++];
            for (int j = 0; j < children; j++)
            {
                node.children.Add(readNodepart(numbers, ref pos));
            }

            for (int j = 0; j < metadata; j++)
            {
                node.metadata.Add(numbers[pos++]);
            }

            return node;
        }
        
        public class Node
        {
            public Node ()
            {
                children = new List<Node>();
                metadata = new List<int>();
            }

            public List<Node> children { get; set; }
            public List<int> metadata { get; set; }

            public int sumMeta()
            {
                return children.Sum(c => c.sumMeta()) + metadata.Sum();
            }

            public int value ()
            {
                int res = 0;
                if (children.Count == 0)
                {
                    res = metadata.Sum();
                }
                else
                {
                    foreach (int metavalue in metadata)
                    {
                        if (metavalue <= children.Count)
                        {
                            res += children[metavalue-1].value();
                        }
                    }
                }
                return res;
            }
        }
    }
}
