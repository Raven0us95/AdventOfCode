using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzles.day8
{
    public class NodeMapBuilder
    {
        private Dictionary<string, (string, string)> nodes;
        
        public NodeMapBuilder(Dictionary<string, (string, string)> nodes) 
        {
            this.nodes = nodes;
        }

        public string[] BuildPath(char[] instructions, string startNode)
        {
            List<string> path = new List<string>();
            string currentNode = startNode;
            int i = 0;
            while (!currentNode.Contains('Z'))
            {
                path.Add(currentNode);
                if (instructions[i] == 'L')
                {
                    currentNode = nodes[currentNode].Item1;
                }
                else
                {
                    currentNode = nodes[currentNode].Item2;
                }
                i = (i + 1) % instructions.Length;
            }
                
            path.Add(currentNode);
            return path.ToArray();
        }
    }
}
