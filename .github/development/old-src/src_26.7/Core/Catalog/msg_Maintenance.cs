// 251112_code
// 260603_documentation.

using System;
using TingenWebService.Core.Logger;

namespace TingenWebService.Core.Catalog
{
    /// <summary>User message catalog for Maintenance functionality.</summary>
    internal static class msg_Maintenance
    {
        /// <summary>Build the user message for refreshing translation tables.</summary>
        /// <remarks>
        /// Returns a "start" message when <paramref name="tableName"/> is supplied, or a "complete" message when it is
        /// omitted or empty.
        /// </remarks>
        /// <param name="tableName">The name of the translation table being refreshed, or empty to indicate completion.</param>
        /// <returns>A timestamped user message describing the translation refresh status.</returns>
        /// <example>
        /// <code>
        /// // Start message:
        /// Console.WriteLine(msg_Maintenance.RefreshTranslations("FormId"));
        /// // Output: [2026-05-15 10:30:45.12345] Refreshing the FormId translation table...
        ///
        /// // Completion message:
        /// Console.WriteLine(msg_Maintenance.RefreshTranslations());
        /// // Output: complete. [2026-05-15 10:30:46.54321]
        /// </code>
        /// </example>
        internal static string RefreshTranslations(string tableName = "")
        {
            return string.IsNullOrWhiteSpace(tableName)
                ? $"complete. [{HistoryLog.Timestamp()}]{Environment.NewLine}"
                : $"[{HistoryLog.Timestamp()}] Refreshing the {tableName} translation table...";
        }

        /* TODO
         * Should this be "blueprintName" instead of "tableName"?
         */
        /// <summary>Build the user message emitted while refreshing blueprints.</summary>
        /// <remarks>
        /// Returns a "start" message when <paramref name="tableName"/> is supplied, or a "complete" message when it is
        /// omitted or empty.
        /// </remarks>
        /// <param name="tableName">The name of the blueprint table being refreshed, or empty to indicate completion.</param>
        /// <returns>A timestamped user message describing the blueprint refresh status.</returns>
        /// <example>
        /// <code>
        /// // Start message:
        /// Console.WriteLine(msg_Maintenance.RefreshBlueprints("ErrorMessages"));
        /// // Output: [2026-05-15 10:30:45.12345] Refreshing the ErrorMessages translation table...
        ///
        /// // Completion message:
        /// Console.WriteLine(msg_Maintenance.RefreshBlueprints());
        /// // Output: complete. [2026-05-15 10:30:46.54321]
        /// </code>
        /// </example>
        internal static string RefreshBlueprints(string tableName = "")
        {
            return string.IsNullOrWhiteSpace(tableName)
                ? $"complete. [{HistoryLog.Timestamp()}]{Environment.NewLine}"
                : $"[{HistoryLog.Timestamp()}] Refreshing the {tableName} translation table...";
        }
    }
}