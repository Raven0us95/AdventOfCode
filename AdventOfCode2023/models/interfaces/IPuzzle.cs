﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.models
{
    public interface IPuzzle
    {
        string Input { get; set; }
        void SolvePart1();
        void SolvePart2();
    }
}
