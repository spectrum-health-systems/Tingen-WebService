// 251112_code
// 260515_documentation.

using System;
using System.Threading;

namespace TingenWebService.Core.Logger
{
    /// <summary>Provides helpers for writing development debug log files.</summary>
    internal static class DebugLog
    {
        /// <summary>Creates a debug log file for the supplied message and source location.</summary>
        /// <remarks>
        /// This method pauses briefly before writing the log entry and stores the file in the local development debug
        /// folder using a timestamp-based file name.
        /// </remarks>
        /// <param name="logMsg">The debug message to write to the log file.</param>
        /// <param name="classPath">The full class path used to derive the class name in the log file name.</param>
        /// <param name="methodName">The method name included in the log file name.</param>
        /// <param name="lineNumber">The source line number included in the log file name.</param>
        /// <example>
        /// <code>
        /// DebugLog.Create(
        ///     logMsg:     "Reached validation step with value = 42",
        ///     classPath:  "Core/Avatar/AvatarOptionObject.cs",
        ///     methodName: "ToReturn",
        ///     lineNumber: 57);
        /// // Writes a file similar to:
        /// //   C:\Tingen_Data\.development\debug\3045-12345-AvatarOptionObject-ToReturn-57.debug
        /// </code>
        /// </example>
        internal static void Create(string logMsg, string classPath, string methodName, int lineNumber)
        {
            Thread.Sleep(100);

            var logName = $"{DateTime.Now:ssff-fffff}-{LogUtility.GetClassName(classPath)}-{methodName}-{lineNumber}.debug";

            LogUtility.WriteLocal($@"C:\\Tingen_Data\\.development\\debug\\{logName}", logMsg);
        }
    }
}