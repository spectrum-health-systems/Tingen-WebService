// 251112_code
// 260515_documentation.

using System;

namespace TingenWebService.Core.Logger
{
    /// <summary>Provides helpers for writing history log entries.</summary>
    internal static class HistoryLog
    {
        /// <summary>Appends a history log entry to the session's history log file.</summary>
        /// <remarks>
        /// This method builds the history log file path from <paramref name="logFolder"/> and
        /// <paramref name="sessionDate"/>, then appends <paramref name="logMsg"/> to that file.
        /// </remarks>
        /// <param name="logFolder">The folder that contains the history log file.</param>
        /// <param name="sessionDate">The session date portion used to build the history log file name.</param>
        /// <param name="logMsg">The message to append to the history log file.</param>
        /// <example>
        /// <code>
        /// HistoryLog.Create(
        ///     logFolder:   @"C:\Tingen_Data\LIVE\AppData\History",
        ///     sessionDate: "2026-05-15",
        ///     logMsg:      $"[{HistoryLog.Timestamp()}] Service started.{Environment.NewLine}");
        /// // Appends to:
        /// //   C:\Tingen_Data\LIVE\AppData\History\2026-05-15.history
        /// </code>
        /// </example>
        internal static void Create(string logFolder, string sessionDate, string logMsg)
        {
            var historyLogPath = $@"{logFolder}\{sessionDate}.history";

            LogUtility.AppendLocal(historyLogPath, logMsg);
        }

        /// <summary>Returns the current timestamp formatted for history log output.</summary>
        /// <remarks>This method formats the current local date and time using the history log timestamp pattern.</remarks>
        /// <returns>A timestamp string in the format <c>yyyy-MM-dd HH:mm:ss.fffff</c>.</returns>
        /// <example>
        /// <code>
        /// var timestamp = HistoryLog.Timestamp();
        /// Console.WriteLine(timestamp);
        /// // Example output: 2026-05-15 10:30:45.12345
        /// </code>
        /// </example>
        internal static string Timestamp() =>
            $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fffff}";
    }
}