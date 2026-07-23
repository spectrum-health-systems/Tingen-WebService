// 251112_code
// 260515_documentation.

using System;
using System.IO;
using System.Linq;

namespace TingenWebService.Core.Logger
{
    /// <summary>Provides shared file and path helpers used by the logging components.</summary>
    internal static class LogUtility
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
            string[] fullClassPath = classPath.Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries);

            return fullClassPath.Last().Replace(".cs", "");
        }

        /// <summary>Writes the supplied content to a local file, overwriting any existing content.</summary>
        /// <param name="filePath">The absolute path of the file to write.</param>
        /// <param name="fileContent">The content to write to the file.</param>
        /// <example>
        /// <code>
        /// LogUtility.WriteLocal(@"C:\Tingen_Data\LIVE\AppData\Log\example.log", "Service started.");
        /// </code>
        /// </example>
        internal static void WriteLocal(string filePath, string fileContent) =>
            File.WriteAllText(filePath, fileContent);

        /// <summary>Appends the supplied content to a local file, creating it if it does not exist.</summary>
        /// <param name="filePath">The absolute path of the file to append to.</param>
        /// <param name="fileContent">The content to append to the file.</param>
        /// <example>
        /// <code>
        /// LogUtility.AppendLocal(
        ///     @"C:\Tingen_Data\LIVE\AppData\History\2026-05-15.history",
        ///     $"[{HistoryLog.Timestamp()}] Service started.{Environment.NewLine}");
        /// </code>
        /// </example>
        internal static void AppendLocal(string filePath, string fileContent) =>
            File.AppendAllText(filePath, fileContent);
    }
}