using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.helper
{
    public class InputFactory
    {
        private string path = $@"{AppDomain.CurrentDomain.BaseDirectory}";
        private static InputFactory instance = new InputFactory();

        static InputFactory()
        {

        }
        private InputFactory()
        {

        }

        public static InputFactory Instance => instance;

        public string[] CreateInputArray(string input)
        {
            // for each line ending add a string to the array
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            return lines;
        }

        public string GetInputString(string path)
        {
            string output = "";
            Stream stream = File.OpenRead(path);
            using (stream)
            {
                try
                {
                    var input = new StreamReader(stream).ReadToEnd();
                    output = input;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return output;
        }
    }
}
