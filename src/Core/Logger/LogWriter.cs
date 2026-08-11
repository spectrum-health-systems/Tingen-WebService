// 260811_code
// 260811_documentation

using System.IO;
using TingenWebService.Du;

namespace TingenWebService.Core.Logger
{
    /// <summary>Provides methods for writing log messages to local files.</summary>
    internal static class LogWriter
    {
        /// <summary>Writes the supplied content to a local file, overwriting any existing content.</summary>
        /// <param name="fileFolder">The folder where the file is located.</param>
        /// <param name="fileName">The name of the file to write.</param>
        /// <param name="fileContent">The content to write to the file.</param>
        /// <example>
        /// <code>
        /// LogUtility.WriteLocal(@"C:\Tingen_Data\LIVE\AppData\Log", "example.log", "Service started.");
        /// </code>
        /// </example>
        internal static void WriteLocal(string fileFolder, string fileName, string fileContent = null)
        {
            /* DEVNOTE: Do not put logger functionality here, it will cause havoc!
             */

            DuDirectory.ForceExist(fileFolder);

            File.WriteAllText(Path.Combine(fileFolder, fileName), fileContent);
        }

        /// <summary>Appends the supplied content to a local file, creating it if it does not exist.</summary>
        /// <param name="fileFolder">The folder where the file is located.</param>
        /// <param name="fileName">The name of the file to append to.</param>
        /// <param name="fileContent">The content to append to the file.</param>
        /// <example>
        /// <code>
        /// LogUtility.AppendLocal(
        ///     @"C:\Tingen_Data\LIVE\AppData\History",
        ///     "2026-05-15.history",
        ///     $"[{HistoryLog.Timestamp()}] Service started.{Environment.NewLine}");
        /// </code>
        /// </example>
        internal static void AppendLocal(string fileFolder, string fileName, string fileContent)
        {
            /* DEVNOTE: Do not put logger functionality here, it will cause havoc!
             */

            DuDirectory.ForceExist(fileFolder);

            File.AppendAllText(Path.Combine(fileFolder, fileName), fileContent);
        }
    }
}