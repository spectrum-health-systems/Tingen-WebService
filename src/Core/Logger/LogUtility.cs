// 260812_code
// 260812_documentation
using System;
using System.IO;
using System.Linq;
using TingenWebService.Core.Framework;
using TingenWebService.Core.Trove;
using TingenWebService.Du;

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

        /// <summary>Recreates the system log files.</summary>
        /// <param name="frameworkSetting">The framework settings.</param>
        /// <param name="runtimeSetting">The runtime settings.</param>
        internal static void RecreateSystemLogs(FrameworkSetting frameworkSetting, RuntimeSetting runtimeSetting)
        {
            //LogEvent.Primeval("PRELOG-TRACE-LogMaintenance-RecreateSystemLogs");

            LogEvent.SystemLog(frameworkSetting.SysLogRoot, "Runtime.details", Redprint.RuntimeDetails(runtimeSetting));
            LogEvent.SystemLog(frameworkSetting.SysLogRoot, "Framework.settings", Redprint.FrameworkSettings(frameworkSetting));
        }
    }
}