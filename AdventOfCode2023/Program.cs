using AdventOfCode2023.Helper;
using AdventOfCode2023.Puzzles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AdventOfCode2023
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = $@"{AppDomain.CurrentDomain.BaseDirectory}\input.txt";
            var input = InputFactory.Instance.GetInputString(path);
            //Puzzle1 puzzle1 = new Puzzle1(input);
            //puzzle1.Solve();
            Puzzle2 puzzle2 = new Puzzle2(input);
            puzzle2.Solve();
            Console.ReadLine();
        }
    }
}
