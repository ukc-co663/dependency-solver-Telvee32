using System;
using System.IO;

namespace CO663.DependencySolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var filepath = args[0];

            Console.WriteLine(Path.GetFullPath(filepath));
            Console.WriteLine(File.ReadAllText(filepath));
        }
    }
}
