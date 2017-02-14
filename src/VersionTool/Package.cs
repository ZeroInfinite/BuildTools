using System;

namespace VersionTool
{
    public class Package : IComparable<Package>
    {
        public string Name { get; set; }
        public string CurrentVersion { get; set; }
        public string NewVersion { get; set; }

        public int CompareTo(Package other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
