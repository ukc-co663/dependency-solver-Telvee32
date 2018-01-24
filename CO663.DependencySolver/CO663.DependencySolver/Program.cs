using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace CO663.DependencySolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var repositoryPath = args[0];
            var initialPath = args[1];
            var constraintsPath = args[2];

            Console.WriteLine(args[0]);
            Console.WriteLine(args[1]);
            Console.WriteLine(args[2]);

            Console.WriteLine($"Repository JSON: {Path.GetFullPath(repositoryPath)}");
            Console.WriteLine($"Initial JSON: {Path.GetFullPath(initialPath)}");
            Console.WriteLine($"Constraints JSON: {Path.GetFullPath(constraintsPath)}");

            var repo = new Repository(File.ReadAllText(repositoryPath));

            Console.WriteLine("====== Packages ======");
            foreach (var package in repo.Packages)
            {
                Console.WriteLine($"Name: {package.Name}");
                Console.WriteLine($"Version: {package.Version}");
                string depends = null;
                if (package.Depends != null && package.Depends.Any())
                {
                    foreach (var depend in package.Depends)
                    {
                        if (depend.Count == 1)
                        {
                            depends += $"[{depend[0]}]";
                        }
                        else // many
                        {
                            depends += "[";
                            foreach (var dep in depend)
                            {
                                depends += $"{dep},";
                            }
                            depends += "]";
                        }
                        depends += ",";
                    }
                }
                Console.WriteLine($"Depends: {depends ?? "none"}");

                Console.WriteLine("---------");
            }

            List<string> initial = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(initialPath));
            List<string> constraints = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(constraintsPath));
        }
    }
}
