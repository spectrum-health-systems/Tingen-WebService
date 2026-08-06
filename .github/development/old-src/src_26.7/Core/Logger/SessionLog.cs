// 251112_code
// 260515_documentation.

using System;
using System.IO;

namespace TingenWebService.Core.Logger
{
    /// <summary>Provides helpers for building and writing session log files.</summary>
    internal static class SessionLog
    {

        /// <summary>Appends a timestamped, free-form entry to the session's running log.</summary>
        /// <param name="wsvcSession">The current Tingen Web Service session whose <c>Runtime.RunningLog</c> is appended to.</param>
        /// <param name="callerInfo">A short identifier for the calling source location, typically from <see cref="LogComponents.GetCallerInfo"/>.</param>
        /// <param name="runningTitle">The title line for the running log entry.</param>
        /// <param name="runningBody">The body text for the running log entry.</param>
        /// <example>
        /// <code>
        /// SessionLog.AddToRunningLog(
        ///     tngnWsvcSession,
        ///     LogComponents.GetCallerInfo(),
        ///     "Validation",
        ///     "All required fields are present.");
        /// </code>
        /// </example>
        internal static void AddToRunningLog(dynamic wsvcSession, string callerInfo, string runningTitle, string runningBody) =>
            wsvcSession.Runtime.RunningLog += $"[{DateTime.Now:mmssff}] [{callerInfo}]{Environment.NewLine}" +
                                              $"{runningTitle}{Environment.NewLine}" +
                                              $"{runningBody}{Environment.NewLine}" +
                                              $"{Environment.NewLine}";

        /// <summary>Appends a structured catalog entry to the session's running log.</summary>
        /// <remarks>
        /// Expects <paramref name="logContents"/> to contain three elements: caller info, title, and body —  matching
        /// the convention produced by message catalog helpers.
        /// </remarks>
        /// <param name="wsvcSession">The current Tingen Web Service session whose <c>Runtime.RunningLog</c> is appended to.</param>
        /// <param name="logContents">A three-element array containing caller info, title, and body for the log entry.</param>
        /// <example>
        /// <code>
        /// var entry = log_DoseChangeEval.PrescriberIsAuthorizing(
        ///     LogComponents.GetCallerInfo(),
        ///     staffMemberIdQuery,
        ///     physicianApprover);
        ///
        /// SessionLog.AddToSessionLog(tngnWsvcSession, entry);
        /// </code>
        /// </example>
        internal static void AddToSessionLog(dynamic wsvcSession, string[] logContents) =>
            wsvcSession.Runtime.RunningLog += $"[{DateTime.Now:mmssff}] [{logContents[0]}]{Environment.NewLine}" +
                                              $"{logContents[1]}{Environment.NewLine}" +
                                              $"{logContents[2]}{Environment.NewLine}" +
                                              $"{Environment.NewLine}";
    }
}