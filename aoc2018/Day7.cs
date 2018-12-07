using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace aoc2018
{
    class Day7
    {
        public static void Go()
        {
            var rules = File.ReadAllLines(@"C:\aoc2018\7\input.txt").Select(line => new Rule(line));

            Dictionary<string, Node> thedict = new Dictionary<string, Node>();

            foreach (Rule rule in rules)
            {
                if (!thedict.Keys.Contains(rule.mustBeFinished))
                    thedict.Add(rule.mustBeFinished, new Node(rule.mustBeFinished));
                if (!thedict.Keys.Contains(rule.beforeBegin))
                    thedict.Add(rule.beforeBegin, new Node(rule.beforeBegin));

                thedict[rule.mustBeFinished].next.Add(rule.beforeBegin);
                thedict[rule.beforeBegin].previous.Add(rule.mustBeFinished);
            }

            var firstnodes = thedict.Values.Where(n => n.previous.Count == 0).OrderBy(n => n.id);

            string result = firstnodes.First().id;
            string first = result;
            thedict[result].done = true;
            while (result.Length < thedict.Keys.Count)
            {
                List<string> test = new List<string>();
                addnextnode(test, thedict, first);
                foreach(Node node in firstnodes)
                {
                    if (!node.done)
                    {
                        test.Add(node.id);
                    }
                }

                test.Sort();
                string next = test[0];
                result += next;
                thedict[next].done = true;
            }
            Console.WriteLine("result a {0}", result);

            //Reset all for step 2:
            foreach (Node n in thedict.Values)
            {
                n.done = false;
            }

            int nrHelpers = 5;
            string[] helpers = new string[nrHelpers];
            firstnodes = thedict.Values.Where(n => n.previous.Count == 0).OrderBy(n => n.id);

            for (int i = 0; i < nrHelpers; i++)
            {
                if (i < firstnodes.Count())
                {
                    helpers[i] = firstnodes.ToList()[i].id;
                }
                else
                {
                    helpers[i] = string.Empty;
                }
            }

            result = string.Empty;
            first = result;
            int secs = 0;

            while (result.Length < thedict.Keys.Count)
            {
                string help = secs + "   ";

                for (int i = 0; i < nrHelpers; i++)
                {
                    if (!string.IsNullOrEmpty(helpers[i]))
                    {
                        help += i + ":" + helpers[i] + " ";
                        thedict[helpers[i]].DoNextStep();
                        if (thedict[helpers[i]].done)
                        {
                            result += thedict[helpers[i]].id;
                            helpers[i] = string.Empty;
                        }
                    }
                }

                Console.WriteLine(help);
                List<string> test = new List<string>();

                foreach (Node node in firstnodes)
                {
                    addnextnode(test, thedict, node.id);
                }

                int j = 0;
                List<string> started = new List<string>();
                if (test.Count > 0)
                {
                    test.Sort();
                    for (int i = 0; i < nrHelpers; i++)
                    {
                        if (string.IsNullOrEmpty(helpers[i]))
                        {
                            if (j < test.Count)
                            {
                                if (!started.Contains(test[j]))
                                {
                                    helpers[i] = test[j++];
                                    started.Add(helpers[i]);
                                }
                                else
                                {
                                    j++;
                                }
                            }
                        }
                    }
                }

                secs++;
            }
            Console.WriteLine("result b {0}" ,secs -1);
        }

        private static void addnextnode(List<string> result, Dictionary<string, Node> thedict, string id)
        {
            foreach (string s in thedict[id].next)
            {
                if (thedict[s].done)
                {
                    addnextnode(result, thedict, s);
                }
                else
                {
                    if (!thedict[s].busy)
                    {
                        bool allpreviousdone = true;
                        foreach (string t in thedict[s].previous)
                        {
                            if (!thedict[t].done)
                            {
                                allpreviousdone = false;
                            }
                        }
                        if (allpreviousdone)
                            result.Add(s);
                    }
                }
            }
        }

        public class Rule
        {
            public Rule (string input)
            {
                mustBeFinished = input.Substring(5,1);
                beforeBegin = input.Substring(36,1);
            }
            public string mustBeFinished { get; }
            public string beforeBegin { get; }
        }

        public class Node
        {
            public Node (string id)
            {
                this.id = id;
                next = new List<string>();
                previous = new List<string>();
                done = false;
                busy = false;
                secstogo = (int)id[0] - 64 + 60;
            }

            public void DoNextStep()
            {
                secstogo -= 1;
                busy = true;
                if (secstogo ==0)
                {
                    done = true;
                }
            }

            public string id { get; }

            public List<string> next;
            public List<string> previous;
            
            public bool done { get; set; }
            public int secstogo { get; set;}
            public bool busy { get; set; }
        }
    }
}
