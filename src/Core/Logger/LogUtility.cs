// 260812_code
// 260812_documentation
using System;
using System.Linq;

namespace TingenWebService.Core.Logger
{
    internal class LogUtility
    {
        /// <summary>Extracts the class name (without extension) from a full source file path.</summary>
        /// <remarks>
        /// Splits <paramref name="classPath"/> on path separators, takes the final segment, and removes the
        /// trailing <c>.cs</c> extension.
        /// </remarks>
        /// <param name="classPath">The full source file path of the class.</param>
        /// <returns>The class name with the <c>.cs</c> extension removed.</returns>
        /// <example>
        /// <code>
        /// var name = LogUtility.GetClassName(@"C:\src\Core\Avatar\AvatarOptionObject.cs");
        /// Console.WriteLine(name);
        /// // Output: AvatarOptionObject
        /// </code>
        /// </example>
        internal static string GetClassName(string classPath)
        {
            /* DEVNOTE: Do not put logger functionality here, it will cause havoc!
             */

            string[] fullClassPath = classPath.Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries);

            return fullClassPath.Last().Replace(".cs", "");
        }
    }
}