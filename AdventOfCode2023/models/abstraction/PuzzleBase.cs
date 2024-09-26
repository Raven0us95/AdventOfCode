using AdventOfCode2023.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.models.abstraction
{
    public abstract class PuzzleBase : IPuzzle
    {
        public PuzzleBase(string input, bool isPart2)
        {
            Input = input ?? GetDefaultInputFromDerived();
            if (isPart2)
            {
                SolvePart2();
            }
            else
            {
                SolvePart1();
            }
        }
        public string Input { get; set; }

        public string[] GetInputStringArray()
        {
            return InputFactory.Instance.CreateInputStringArray(Input);
        }
        public char[,] GetInput2DCharArray()
        {
            return InputFactory.Instance.CreateInput2DCharArray(Input);
        }
        public abstract void SolvePart1();
        public abstract void SolvePart2();
        protected abstract string GetDefaultInputFromDerived();
    }
}
