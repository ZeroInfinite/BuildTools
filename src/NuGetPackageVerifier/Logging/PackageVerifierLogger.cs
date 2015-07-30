﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace NuGetPackageVerifier.Logging
{
    public class PackageVerifierLogger : IPackageVerifierLogger
    {
        public void Log(LogLevel logLevel, string message)
        {
            ConsoleColor foreColor;
            switch (logLevel)
            {
                case LogLevel.Error:
                    foreColor = ConsoleColor.Red;
                    break;

                case LogLevel.Warning:
                    foreColor = ConsoleColor.Yellow;
                    break;

                default:
                case LogLevel.Info:
                    foreColor = ConsoleColor.Gray;
                    break;
            }

            Console.ForegroundColor = foreColor;
            Console.WriteLine("{0}: {1}", logLevel.ToString().ToUpperInvariant(), message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
