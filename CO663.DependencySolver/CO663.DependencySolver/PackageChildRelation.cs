using System;
using System.Collections.Generic;
using System.Text;

namespace CO663.DependencySolver
{
    public class PackageChildRelation
    {
        public string Name { get; }
        public string Version { get; }
        public VersionComparisonOperator Operator { get; }

        public PackageChildRelation(string name, string version, VersionComparisonOperator @operator)
        {
            Name = name;
            Version = version;
            Operator = @operator;
        }
    }

    public enum VersionComparisonOperator
    {
        GreaterThan,
        GreaterThanEqualTo,
        LessThan,
        LessThanEqualTo,
        EqualTo
    }
}
