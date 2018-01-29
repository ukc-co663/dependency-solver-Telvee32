using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CO663.DependencySolver
{
    public class Repository
    {
        public List<Package> Packages { get; set; }

        public Repository(string json)
        {
            // parse JSON and construct package list
            Packages = JsonConvert.DeserializeObject<List<Package>>(json);

            // build package dependency and conflict graph
            foreach (var package in Packages)
            {
                BuildDependencies(package);
            }
        }

        private void BuildDependencies(Package package)
        {
            // check that we haven't already populated this package
            if (package.Depends.Any() && !package.PackageDependencies.Any())
            {
                foreach (var packageDepend in package.Depends)
                {
                    var list = new List<Package>();
                    package.PackageDependencies.Add(list);
                    foreach (var depend in packageDepend)
                    {
                        var constraint = GetVersionConstraint(depend);
                        switch (constraint.Item2)
                        {
                            case ComparisonOperator.Default:
                            {
                                var dependencies = Packages.Where(p => p.Name == constraint.Item1).ToList();
                                list.AddRange(dependencies);
                                break;
                            }
                            case ComparisonOperator.GreaterThan:
                            {
                                var dependencies = Packages.Where(p =>
                                        p.Name == constraint.Item1 && p.Version > constraint.Item3)
                                    .ToList();
                                list.AddRange(dependencies);
                                break;
                            }
                            case ComparisonOperator.GreaterThanEqualTo:
                            {
                                var dependencies = Packages.Where(p =>
                                        p.Name == constraint.Item1 && p.Version >= constraint.Item3)
                                    .ToList();
                                list.AddRange(dependencies);
                                break;
                            }
                            case ComparisonOperator.LessThan:
                            {
                                var dependencies = Packages.Where(p =>
                                        p.Name == constraint.Item1 && p.Version < constraint.Item3)
                                    .ToList();
                                list.AddRange(dependencies);
                                break;
                            }
                            case ComparisonOperator.LessThanEqualTo:
                            {
                                var dependencies = Packages.Where(p =>
                                    p.Name == constraint.Item1 && p.Version <= constraint.Item3)
                                    .ToList();
                                list.AddRange(dependencies);
                                break;
                            }
                            case ComparisonOperator.EqualTo:
                            {
                                var dependency = Packages.FirstOrDefault(p =>
                                    p.Name == constraint.Item1 && p.Version == constraint.Item3);
                                if(dependency != null) list.Add(dependency);
                                break;
                            }
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }
        }

        private (string, ComparisonOperator, Version) GetVersionConstraint(string input)
        {
            string name = "";
            ComparisonOperator @operator = ComparisonOperator.Default;
            int operatorIndex = -1;
            Version version = null;
            int versionIndex = -1;

            // check for <
            if (operatorIndex == -1)
            {
                operatorIndex = input.IndexOf('<');
                if (operatorIndex != -1)
                {
                    if (input[operatorIndex + 1] == '=')
                    {
                        @operator = ComparisonOperator.LessThanEqualTo;
                        versionIndex = operatorIndex + 2;
                    }
                    else
                    {
                        @operator = ComparisonOperator.LessThan;
                        versionIndex = operatorIndex + 1;
                    }
                }
            }

            // check for >
            if (operatorIndex == -1)
            {
                operatorIndex = input.IndexOf('>');
                if (operatorIndex != -1)
                {
                    if (input[operatorIndex + 1] == '=')
                    {
                        @operator = ComparisonOperator.GreaterThanEqualTo;
                        versionIndex = operatorIndex + 2;
                    }
                    else
                    {
                        @operator = ComparisonOperator.GreaterThan;
                        versionIndex = operatorIndex + 1;
                    }
                }
            }

            // check for =
            if (operatorIndex == -1)
            {
                operatorIndex = input.IndexOf('=');
                if (operatorIndex != -1)
                {
                    @operator = ComparisonOperator.EqualTo;
                    versionIndex = operatorIndex + 1;
                }
            }

            name = operatorIndex == -1 ? 
                input : // no operator found, input is name only
                input.Substring(0, operatorIndex);
            if (versionIndex != -1)
            {
                version = new Version(input.Substring(versionIndex));
            }

            return (name, @operator, version);
        }
    }
}
