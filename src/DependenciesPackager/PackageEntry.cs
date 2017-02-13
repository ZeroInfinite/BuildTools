using System.Collections.Generic;
using NugetReferenceResolver;

namespace DependenciesPackager
{
    internal class PackageEntry
    {
        public IDictionary<PackageAssembly, int> CrossGenExitCode { get; set; } = new Dictionary<PackageAssembly, int>();

        public Package Library { get; set; }

        public IReadOnlyList<PackageAssembly> Assets { get; set; }

        public IDictionary<PackageAssembly, IList<string>> CrossGenOutput { get; } =
            new Dictionary<PackageAssembly, IList<string>>();
    }
}
