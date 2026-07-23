// 251112_code
// 260603_documentation.

using System;
using TingenWebService.Core.Logger;

namespace TingenWebService.Core.Catalog
{
    /// <summary>User message catalog for Framework functionality.</summary>
    internal class msg_Framework
    {
        /// <summary>Build the user message for when a framework-related folder is created.</summary>
        /// <param name="folderName">The name of the folder that was created.</param>
        /// <returns>A formatted, timestamped user message describing the folder creation.</returns>
        /// <example>
        /// <code>
        /// var userMessage = msg_Framework.CreateFolder(@"C:\Tingen_Data\Logs");
        /// Console.WriteLine(userMessage);
        /// // Output:
        /// // [2026-05-15 10:30:45.12345] Folder "C:\Tingen_Data\Logs" did not exist, and was created...
        /// </code>
        /// </example>
        internal static string CreateFolder(string folderName) =>
            Environment.NewLine +
            $"[{HistoryLog.Timestamp()}] Folder \"{folderName}\" did not exist, and was created..." +
            Environment.NewLine;
    }
}