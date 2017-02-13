using System;
using System.Collections.Generic;
using System.Text;

namespace NugetReferenceResolver
{
    public class RuntimeDefinition
    {
        public string Name { get; set; }
        public IEnumerable<RuntimeDefinition> Fallbacks { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
