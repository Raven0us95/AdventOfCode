using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Factories
{
    public class InputFactory
    {
        private string path = $@"{AppDomain.CurrentDomain.BaseDirectory}";
        private static InputFactory instance = new InputFactory();

        private InputFactory()
        {

        }

        public static InputFactory Instance => instance;

        public string[] CreateInputStringArray(string input)
        {
            // for each line ending add a string to the array
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            return lines;
        }

        public char[,] CreateInput2DCharArray(string input)
        {
            // Split the input string by lines
            string[] lines = CreateInputStringArray(input);

            // Create a 2D char array
            char[,] charArray = new char[lines.Length, lines.Max(line => line.Length)];

            // Fill the char array
            for (int i = 0; i < lines.Length; i++)
            {
                char[] chars = lines[i].ToCharArray();
                for (int j = 0; j < chars.Length; j++)
                {
                    charArray[i, j] = chars[j];
                }
            }

            return charArray;
        }

        public string GetInputString(string path)
        {
            try
            {
                using var streamReader = new StreamReader(path);
                return streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
