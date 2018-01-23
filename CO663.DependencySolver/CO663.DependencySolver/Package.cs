using System;
using System.Collections.Generic;
using System.Text;

namespace CO663.DependencySolver
{
    public class Package
    {
        public string Name { get; }
        public string Version { get; } // TODO - replace with version object - list of int with comparison methods
        public int Size { get; }
        public List<List<string>> Depends { get; }
        public List<string> Conflicts { get; }

        public Package(string name, string version, int size, List<List<string>> depends, List<string> conflicts)
        {
            Name = name;
            Version = version;
            Size = size;
            Depends = depends;
            Conflicts = conflicts;
        }
    }
}
