// 251112_code
// 260515_documentation.

using System;
using System.IO;

namespace TingenWebService.Core.Logger
{
    /// <summary>Provides helpers for building and writing session log files.</summary>
    internal static class SessionLog
    {
        /// <summary>Creates the session log file for the current Tingen Web Service session.</summary>
        /// <remarks>
        /// Loads the session log blueprint, fills it with values from <paramref name="wsvcSession"/>, and writes the
        /// resulting content to the session-specific folder.
        /// </remarks>
        /// <param name="wsvcSession">The current Tingen Web Service session object that supplies runtime, framework, and script parameter data.</param>
        /// <example>
        /// <code>
        /// SessionLog.Create(tngnWsvcSession);
        /// // Writes:
        /// //   {Session}\jsmith.session
        /// </code>
        /// </example>
        internal static void Create(dynamic wsvcSession)
        {
            var logName      = $"{wsvcSession.Runtime.AvatarUserId}.session";
            var logBlueprint = File.ReadAllText($@"{wsvcSession.Framework.TngnWsvcDataFolder.Blueprint}\session-log.blueprint");
            var logContent   = Outline(logBlueprint, wsvcSession.Runtime, wsvcSession.ScriptParameter.OriginalScriptParameter);

            LogUtility.WriteLocal($@"{wsvcSession.Framework.TngnWsvcDataFolder.Session}\{logName}", logContent);
        }

        /// <summary>Builds the session log content by replacing blueprint tokens with runtime session values.</summary>
        /// <remarks>
        /// Replaces the standard session placeholders, including session date/start/end/duration, Avatar system,
        /// script parameter, and the accumulated running log.
        /// </remarks>
        /// <param name="logBlueprint">The blueprint template containing the session placeholders to replace.</param>
        /// <param name="sessionDetail">The runtime session details, exposing <c>SessionDate</c>, <c>SessionTime</c>, <c>AvatarUserId</c>, <c>AvatarSystem</c>, and <c>RunningLog</c>.</param>
        /// <param name="sessionScriptParameter">The original script parameter for the current session.</param>
        /// <returns>A formatted session log entry string.</returns>
        /// <example>
        /// <code>
        /// var blueprint = File.ReadAllText(blueprintPath);
        /// var content   = SessionLog.Outline(
        ///     logBlueprint:           blueprint,
        ///     sessionDetail:          tngnWsvcSession.Runtime,
        ///     sessionScriptParameter: tngnWsvcSession.ScriptParameter.OriginalScriptParameter);
        /// </code>
        /// </example>
        internal static string Outline(string logBlueprint, dynamic sessionDetail, string sessionScriptParameter)
        {
            var endTime  = DateTime.Now.ToString("HHmmss");
            var duration = (DateTime.ParseExact(endTime, "HHmmss", null) - DateTime.ParseExact(sessionDetail.SessionTime, "HHmmss", null)).ToString(@"hh\:mm\:ss");

            return logBlueprint.Replace("~SESSION~DATE~", sessionDetail.SessionDate)
                               .Replace("~SESSION~START~", sessionDetail.SessionTime)
                               .Replace("~SESSION~END~", endTime)
                               .Replace("~SESSION~DURATION~", duration)
                               .Replace("~OPTIONID~", sessionDetail.AvatarUserId.ToUpper())
                               .Replace("~AVATAR~SYSTEM~", sessionDetail.AvatarSystem.ToUpper())
                               .Replace("~SCRIPT~PARAMETER~", sessionScriptParameter)
                               .Replace("~RUNNING~LOG~", sessionDetail.RunningLog);
        }

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