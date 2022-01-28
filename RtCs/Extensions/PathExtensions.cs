using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace RtCs
{
    public static class PathExtensions
    {
        public static bool IsAbsolutePath(this string inPath)
            => Uri.TryCreate(inPath, UriKind.Absolute, out var _);

        public static string UnifyPathSeparator(this string inPath)
            => inPath.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
    }
}
