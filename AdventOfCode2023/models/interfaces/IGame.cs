using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.models.interfaces
{
    public interface IGame
    {
        int Id { get; set; }
        List<string> Sets { get; set; }
        void GetSumOfNeccessaryCubes();
    }
}
