using System;
using System.Collections.Generic;
using System.Text;

namespace CO663.DependencySolver
{
    public class Package
    {
        public string Name { get; }
        public Version Version { get; }
        public int Size { get; }
        public List<List<string>> Depends { get; }
        public List<string> Conflicts { get; }

        // graph structure
        // outer list elements are treated as AND, inner list elements are treated as OR
        public List<List<Package>> PackageDependencies { get; set; }
        public List<Package> PackageConflicts { get; set; }

        public Package(string name, string version, int size, List<List<string>> depends, List<string> conflicts)
        {
            Name = name;
            Version = new Version(version);
            Size = size;
            Depends = depends ?? new List<List<string>>();
            Conflicts = conflicts ?? new List<string>();
            PackageDependencies = new List<List<Package>>();
            PackageConflicts = new List<Package>();
        }
    }
}
