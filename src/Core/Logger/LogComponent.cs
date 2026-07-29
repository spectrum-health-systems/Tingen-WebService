// 260729_code
// 260729_documentation

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace TingenWebService.Core.Logger
{
    /// <summary>Provides logging-related helper methods.</summary>
    internal static class LogComponents
    {
        /// <summary>Returns a formatted caller information string built from the calling source location.</summary>
        /// <remarks>
        /// Uses caller-info attributes to capture the source file, member name, and line number, then returns them in
        /// the form <c>{class}-{method}-{line}</c>.
        /// </remarks>
        /// <param name="className">The full source file path of the caller, supplied automatically by the compiler.</param>
        /// <param name="methodName">The name of the calling member, supplied automatically by the compiler.</param>
        /// <param name="lineNumber">The source line number of the caller, supplied automatically by the compiler.</param>
        /// <returns>A string in the form <c>{class}-{method}-{line}</c>.</returns>
        /// <example>
        /// <code>
        /// var info = LogComponents.GetCallerInfo();
        /// Console.WriteLine(info);
        /// // Output: AvatarOptionObject-ToReturn-57
        /// </code>
        /// </example>
        internal static string GetCallerInfo([CallerFilePath] string className = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
        {
            var classOnly = Path.GetFileNameWithoutExtension(className);

            return $"{classOnly}-{methodName}-{lineNumber}";
        }

        /// <summary>Formats raw XML content into a Markdown-style XML code block for log output.</summary>
        /// <param name="rawXml">The raw XML string to format.</param>
        /// <returns>A Markdown-formatted XML code block representing the supplied XML.</returns>
        /// <example>
        /// <code>
        /// var rawXml    = "&lt;Settings&gt;&lt;Mode&gt;enabled&lt;/Mode&gt;&lt;/Settings&gt;";
        /// var formatted = LogComponents.FormatXml(rawXml);
        /// Console.WriteLine(formatted);
        /// // Output:
        /// // ```xml
        /// //     &lt;Settings&gt;&lt;Mode&gt;enabled&lt;/Mode&gt;&lt;/Settings&gt;
        /// // ```
        /// </code>
        /// </example>
        internal static string FormatXml(string rawXml)
        {
            return $"```xml{Environment.NewLine}" +
                   $"    {XDocument.Parse(rawXml)}{Environment.NewLine}" +
                   $"```{Environment.NewLine}";
        }
    }
}