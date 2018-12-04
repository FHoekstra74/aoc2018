using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace aoc2018
{
    class Day4
    {
        enum Action { BeginShift , FallAsleep, WakeUp}

        public static void Go()
        {
            var observations = File.ReadAllLines(@"C:\aoc2018\4\input.txt").Select(line => new Observation(line)).OrderBy(o => o.date);

            var guartTotalMinutesAsleep = new Dictionary<string, int>();
            var guartAsleepPerMinute = new Dictionary<string, int>();

            int startmMinute = 0;
            string currentGuard = string.Empty;

            foreach (Observation observation in observations)
            {
                if (observation.action == Action.BeginShift)
                {
                    currentGuard = observation.guard;
                }
                if (observation.action == Action.FallAsleep)
                {
                    startmMinute = observation.date.Minute;
                }
                if (observation.action == Action.WakeUp)
                {
                    if (!guartTotalMinutesAsleep.ContainsKey(currentGuard))
                        guartTotalMinutesAsleep.Add(currentGuard, 0);

                    guartTotalMinutesAsleep[currentGuard] += (observation.date.Minute - startmMinute);
                    for (int i = startmMinute; i < observation.date.Minute; i++)
                    {
                        string key = currentGuard + '-' + i.ToString();
                        if (!guartAsleepPerMinute.ContainsKey(key))
                            guartAsleepPerMinute.Add(key, 0);

                        guartAsleepPerMinute[key] += 1;
                    }
                }
            }

            var guardMostAsleep = guartTotalMinutesAsleep.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

            int mostSleepyMinute = 0;
            int sleep = 0;

            for (int i = 0; i < 59; i++)
            {
                string key = guardMostAsleep + "-" + i;
                if (guartAsleepPerMinute[key] > sleep)
                {
                    mostSleepyMinute = i;
                    sleep = guartAsleepPerMinute[key];
                }
            }

            Console.WriteLine("Answer 1: {0}", Convert.ToInt32(guardMostAsleep) * mostSleepyMinute);
            var keyMostAsleep = guartAsleepPerMinute.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
            Console.WriteLine("Answer 2: {0}", Convert.ToInt16(keyMostAsleep.Split('-')[0]) * Convert.ToInt16(keyMostAsleep.Split('-')[1]));
        }

        private class Observation
        {
            public Observation(string input)
            {
                rawdata = input;
                date = new DateTime(Convert.ToInt16(input.Substring(1, 4)) , Convert.ToInt16(input.Substring(6, 2)), Convert.ToInt16( input.Substring(9, 2)), Convert.ToInt16(input.Substring(12, 2)), Convert.ToInt16(input.Substring(15, 2)),0);

                if (rawdata.Contains('#'))
                {
                    guard = rawdata.Split('#')[1].Split(' ')[0];
                    action = Action.BeginShift;
                }
                else
                {
                    guard = string.Empty;
                    if (rawdata.Contains("falls asleep"))
                        action = Action.FallAsleep;
                    else
                        action = Action.WakeUp;
                }
            }

            public string rawdata { get; }
            public DateTime date { get; }
            public string guard { get; }
            public Action action { get; }
        }
    }
}
