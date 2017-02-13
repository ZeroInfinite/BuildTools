using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NugetReferenceResolver
{
    public class PackageAssembly
    {
        public PackageAssembly(string relativePath, string resolvedPath)
        {
            RelativePath = relativePath;
            ResolvedPath = resolvedPath;
        }

        public string FileName => Path.GetFileName(ResolvedPath);
        public string RelativePath { get; set; }
        public string ResolvedPath { get; set; }
    }
}
