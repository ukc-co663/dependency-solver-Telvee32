using System;
using System.Linq;

namespace CO663.DependencySolver
{
    public class Version
    {
        public int[] VersionNumbers { get; }
        public Version(string versionString)
        {
            VersionNumbers = versionString.Split('.').Select(s => int.Parse(s)).ToArray();
        }

        public static bool operator ==(Version x, Version y)
        {
            if (x is null && y is null) return true;
            if (x is null || y is null) return false; // if only one is null

            if (ReferenceEquals(x, y)) return true;

            if (x.VersionNumbers.Length != y.VersionNumbers.Length) return false;
            if (!x.VersionNumbers.Any() && !y.VersionNumbers.Any()) return true;

            for (int i = 0; i < x.VersionNumbers.Length; i++)
            {
                if (x.VersionNumbers[i] != y.VersionNumbers[i]) return false;
            }

            return true;
        }

        public static bool operator !=(Version x, Version y)
        {
            if (x is null && y is null) return false;
            if (x is null || y is null) return true; // if only one is null

            if (ReferenceEquals(x, y)) return false;

            if (x.VersionNumbers.Length != y.VersionNumbers.Length) return true;
            if (!x.VersionNumbers.Any() && !y.VersionNumbers.Any()) return false;

            for (int i = 0; i < x.VersionNumbers.Length; i++)
            {
                if (x.VersionNumbers[i] != y.VersionNumbers[i]) return true;
            }

            return false;
        }

        public static bool operator <=(Version x, Version y)
        {
            if (x is null && y is null) return true;
            if (x is null && !(y is null)) return true;
            if (!(x is null) && y is null) return false;

            if (ReferenceEquals(x, y)) return true;

            if (!x.VersionNumbers.Any() && !y.VersionNumbers.Any()) return true;
            if (!x.VersionNumbers.Any() && y.VersionNumbers.Any()) return true;
            if (x.VersionNumbers.Any() && !y.VersionNumbers.Any()) return false;

            // both are fully populated - at least one element
            // iterating up to the length of the shortest will be enough - ordering
            // is done lexicographically, so if they are the same up to this point
            // then the longer one is considered to be higher
            int shortest = Math.Min(x.VersionNumbers.Length, y.VersionNumbers.Length);
            for (int i = 0; i < shortest; i++)
            {
                if (x.VersionNumbers[i] > y.VersionNumbers[i]) return false;
            }

            // if we get here, then they are either equal
            // or they are equal up to the shortest of the two,
            // and one is longer (and thus greater)
            if (x.VersionNumbers.Length == y.VersionNumbers.Length) return true; // equal
            return y.VersionNumbers.Length > x.VersionNumbers.Length;
        }

        public static bool operator >=(Version x, Version y)
        {
            if (x is null && y is null) return true;
            if (x is null && !(y is null)) return false;
            if (!(x is null) && y is null) return true;

            if (ReferenceEquals(x, y)) return true;

            if (!x.VersionNumbers.Any() && !y.VersionNumbers.Any()) return true;
            if (!x.VersionNumbers.Any() && y.VersionNumbers.Any()) return false;
            if (x.VersionNumbers.Any() && !y.VersionNumbers.Any()) return true;

            // both are fully populated - at least one element
            // iterating up to the length of the shortest will be enough - ordering
            // is done lexicographically, so if they are the same up to this point
            // then the longer one is considered to be higher
            int shortest = Math.Min(x.VersionNumbers.Length, y.VersionNumbers.Length);
            for (int i = 0; i < shortest; i++)
            {
                if (x.VersionNumbers[i] < y.VersionNumbers[i]) return false;
            }

            // if we get here, then they are either equal
            // or they are equal up to the shortest of the two,
            // and one is longer (and thus greater)
            if (x.VersionNumbers.Length == y.VersionNumbers.Length) return true; // equal
            return y.VersionNumbers.Length < x.VersionNumbers.Length;
        }

        public static bool operator <(Version x, Version y)
        {
            if (x is null && y is null) return false;
            if (x is null && !(y is null)) return true;
            if (!(x is null) && y is null) return false;

            if (ReferenceEquals(x, y)) return false;

            if (!x.VersionNumbers.Any() && !y.VersionNumbers.Any()) return false;
            if (!x.VersionNumbers.Any() && y.VersionNumbers.Any()) return true;
            if (x.VersionNumbers.Any() && !y.VersionNumbers.Any()) return false;

            // both are fully populated - at least one element
            // iterating up to the length of the shortest will be enough - ordering
            // is done lexicographically, so if they are the same up to this point
            // then the longer one is considered to be higher
            int shortest = Math.Min(x.VersionNumbers.Length, y.VersionNumbers.Length);
            for (int i = 0; i < shortest; i++)
            {
                if (x.VersionNumbers[i] > y.VersionNumbers[i]) return false;
            }

            // if we get here, then they are either equal
            // or they are equal up to the shortest of the two,
            // and one is longer (and thus greater)
            if (x.VersionNumbers.Length == y.VersionNumbers.Length) return false; // equal
            return y.VersionNumbers.Length > x.VersionNumbers.Length;
        }

        public static bool operator >(Version x, Version y)
        {
            if (x is null && y is null) return false;
            if (x is null && !(y is null)) return false;
            if (!(x is null) && y is null) return true;

            if (ReferenceEquals(x, y)) return false;

            if (!x.VersionNumbers.Any() && !y.VersionNumbers.Any()) return false;
            if (!x.VersionNumbers.Any() && y.VersionNumbers.Any()) return false;
            if (x.VersionNumbers.Any() && !y.VersionNumbers.Any()) return true;

            // both are fully populated - at least one element
            // iterating up to the length of the shortest will be enough - ordering
            // is done lexicographically, so if they are the same up to this point
            // then the longer one is considered to be higher
            int shortest = Math.Min(x.VersionNumbers.Length, y.VersionNumbers.Length);
            for (int i = 0; i < shortest; i++)
            {
                if (x.VersionNumbers[i] < y.VersionNumbers[i]) return false;
            }

            // if we get here, then they are either equal
            // or they are equal up to the shortest of the two,
            // and one is longer (and thus greater)
            if (x.VersionNumbers.Length == y.VersionNumbers.Length) return false; // equal
            return y.VersionNumbers.Length < x.VersionNumbers.Length;
        }

        protected bool Equals(Version other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            if (this.VersionNumbers.Length != other.VersionNumbers.Length) return false;
            if (!this.VersionNumbers.Any() && !other.VersionNumbers.Any()) return true;

            for (int i = 0; i < this.VersionNumbers.Length; i++)
            {
                if (this.VersionNumbers[i] != other.VersionNumbers[i]) return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return this == obj as Version;
        }

        public override int GetHashCode()
        {
            return (VersionNumbers != null ? VersionNumbers.GetHashCode() : 0);
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < VersionNumbers.Length; i++)
            {
                if (i == VersionNumbers.Length - 1)
                {
                    // last component of number
                    s += VersionNumbers[i];
                }
                else
                {
                    s += $"{VersionNumbers[i]}.";
                }
            }

            return s;
        }
    }
}
