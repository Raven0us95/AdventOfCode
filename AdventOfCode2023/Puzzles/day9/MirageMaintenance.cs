using AdventOfCode2023.models.abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day9
{
    public class MirageMaintenance : PuzzleBase
    {
        public MirageMaintenance(string input) : base(input)
        {
        }

        public override void Solve()
        {
            int sum = 0;
            var input = GetInputStringArray();
            foreach (var history in input)
            {
                int nextValue = FindNextValue(history, true);
                sum += nextValue;
            }
        }

        private int FindNextValue(string history, bool findPreviousValue)
        {
            int nextValue = 0;
            // find changes in the history until there are no more changes
            List<string> changeList = new List<string>();
            changeList.Add(history);

            string changes = GetChanges(history);
            changeList.Add(changes);

            while (changes.Split().All(x => x == "0") == false)
            {
                changes = GetChanges(changes);
                changeList.Add(changes);
            }

            // add new values in reverse Order
            changeList.Reverse();

            for (int i = 1; i < changeList.Count; i++)
            {
                var split = changeList[i].Split();
                var split2 = changeList[i - 1].Split();
                int value1;
                int value2;
                if (findPreviousValue)
                {
                    value1 = int.Parse(split[0]);
                    value2 = int.Parse(split2[0]);
                    nextValue = value1 - value2;
                    changeList[i] = string.Join(" ", nextValue, changeList[i]);
                }
                else
                {
                    value1 = int.Parse(split[split.Length - 1]);
                    value2 = int.Parse(split2[split2.Length - 1]);
                    nextValue = value1 + value2;
                    changeList[i] = string.Join(" ", changeList[i], nextValue);
                }
            }
            
            return nextValue;
        }

        private string GetChanges(string history)
        {
            var changes = new List<int>();
            List<int> values = history.Split().Select(c => int.Parse(c.ToString())).ToList();
            for (int i = 0; i < values.Count; i++)
            {
                if (i + 1 < values.Count)
                {
                    var difference = values[i + 1] - values[i];
                    changes.Add(difference);
                }
            }
            var output = string.Join(' ', changes);
            return output;
        }

        protected override string GetDefaultInputFromDerived()
        {
            return "0 3 6 9 12 15\r\n1 3 6 10 15 21\r\n10 13 16 21 30 45";
        }
    }
}
