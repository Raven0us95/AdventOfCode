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
        public PuzzleBase(string input)
        {
            Input = input ?? GetDefaultInputFromDerived();
            Solve();
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
        public abstract void Solve();
        protected abstract string GetDefaultInputFromDerived();
    }
}
