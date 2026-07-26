// 260725_code
// 260725_documentation

using System;
using System.Xml.Linq;

namespace TingenWebService.Logger
{
    internal class LogComponents
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
        internal static string GetCallerInfo([System.Runtime.CompilerServices.CallerFilePath] string className = "", [System.Runtime.CompilerServices.CallerMemberName] string methodName = "", [System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0)
        {
            var classOnly = System.IO.Path.GetFileNameWithoutExtension(className);
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