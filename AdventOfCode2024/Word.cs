using AdventOfCode2023.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Word
    {
        Position startingPosition;
        private char[] letters;
        public Word(char[] letters, Position? position)
        {
            this.letters = letters ?? Array.Empty<char>();
            Found = new char[letters.Length];
            startingPosition = position ?? new Position(0,0);
        }
        public char[] Letters => letters;
        public char[] Found;
        public string Read()
        {
            return letters?.ToString() ?? "";
        }
    }
}
