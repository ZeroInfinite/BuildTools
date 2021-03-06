﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mono.Cecil;
using NuGet.Versioning;

namespace NuGetPackageVerifier.Rules
{
    public class PackageVersionMatchesAssemblyVersionRule : AssemblyHasAttributeRuleBase
    {
        private NuGetVersion _packageVersion;
        private string _packageId;

        public override IEnumerable<PackageVerifierIssue> Validate(PackageAnalysisContext context)
        {
            _packageVersion = context.Metadata.Version;
            _packageId = context.Metadata.Id;
            return base.Validate(context);
        }

        public override IEnumerable<PackageVerifierIssue> ValidateAttribute(
            string currentFilePath,
            AssemblyDefinition assembly,
            Mono.Collections.Generic.Collection<CustomAttribute> assemblyAttributes)
        {
            var assemblyInformationalVersionAttribute = assemblyAttributes.SingleOrDefault(a =>
                a.AttributeType.FullName.Equals(
                    typeof(AssemblyInformationalVersionAttribute).FullName,
                    StringComparison.Ordinal));

            var assemblyInformationalNuGetVersion = new NuGetVersion(assemblyInformationalVersionAttribute.ConstructorArguments[0].Value.ToString());
            if (!VersionEquals(_packageVersion, assemblyInformationalNuGetVersion))
            {
                yield return PackageIssueFactory.AssemblyInformationalVersionDoesNotMatchPackageVersion(
                    currentFilePath,
                    assemblyInformationalNuGetVersion,
                    _packageVersion,
                    _packageId);
            }

            var assemblyFileVersionAttribute = assemblyAttributes.SingleOrDefault(a =>
                a.AttributeType.FullName.Equals(
                    typeof(AssemblyFileVersionAttribute).FullName,
                    StringComparison.Ordinal));

            var assemblyFileNuGetVersion = new NuGetVersion(assemblyFileVersionAttribute.ConstructorArguments[0].Value.ToString());
            if (!VersionEquals(_packageVersion, assemblyFileNuGetVersion))
            {
                yield return PackageIssueFactory.AssemblyFileVersionDoesNotMatchPackageVersion(
                    currentFilePath,
                    assemblyFileNuGetVersion,
                    _packageVersion,
                    _packageId);
            }

            var assemblyVersion = assembly.Name.Version;
            if (!_packageVersion.Version.Equals(assemblyVersion))
            {
                yield return PackageIssueFactory.AssemblyVersionDoesNotMatchPackageVersion(
                    currentFilePath,
                    assemblyVersion,
                    _packageVersion.Version,
                    _packageId);
            }
        }

        private bool VersionEquals(NuGetVersion packageVersion, NuGetVersion assemblyNuGetVersion)
        {
            if (packageVersion == null)
            {
                throw new ArgumentNullException(nameof(packageVersion));
            }

            if (assemblyNuGetVersion == null)
            {
                throw new ArgumentNullException(nameof(assemblyNuGetVersion));
            }

            // Pre-release and build metadata does not need to match
            if (packageVersion.Major == assemblyNuGetVersion.Major &&
                packageVersion.Minor == assemblyNuGetVersion.Minor &&
                packageVersion.Patch == assemblyNuGetVersion.Patch)
            {
                return true;
            }

            return false;
        }
    }
}
