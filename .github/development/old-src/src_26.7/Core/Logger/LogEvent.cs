// 251112_code
// 260515_documentation.

using System.Runtime.CompilerServices;

namespace TingenWebService.Core.Logger
{
    /// <summary>Provides a unified facade for raising log events of different severities and types.</summary>
    public static class LogEvent
    {
        /// <summary>Writes a debug log entry that captures the supplied message and the calling source location.</summary>
        /// <remarks>
        /// The <paramref name="classPath"/>, <paramref name="methodName"/>, and <paramref name="lineNumber"/>
        /// parameters are populated automatically by the compiler via caller-info attributes.
        /// </remarks>
        /// <param name="logMsg">The debug message to record.</param>
        /// <param name="classPath">The full source file path of the caller, supplied automatically by the compiler.</param>
        /// <param name="methodName">The name of the calling member, supplied automatically by the compiler.</param>
        /// <param name="lineNumber">The source line number of the caller, supplied automatically by the compiler.</param>
        /// <example>
        /// <code>
        /// LogEvent.Debug("Reached validation step.");
        /// </code>
        /// </example>
        public static void Debug(string logMsg = "", [CallerFilePath] string classPath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0) =>
            DebugLog.Create(logMsg, classPath, methodName, lineNumber);

        /// <summary>Writes a critical log entry to the session and shared log folders.</summary>
        /// <remarks>
        /// The <paramref name="classPath"/>, <paramref name="methodName"/>, and <paramref name="lineNumber"/>
        /// parameters are populated automatically by the compiler via caller-info attributes.
        /// </remarks>
        /// <param name="avatarUserId">The Avatar user identifier used when building the log file name.</param>
        /// <param name="tngnWsvcDataFolder">The data folder object that provides the blueprint, session, and log paths.</param>
        /// <param name="exeAsm">The assembly name associated with the log entry.</param>
        /// <param name="logTitle">The title prefix used when building the log file name.</param>
        /// <param name="logMsg">The critical message to record.</param>
        /// <param name="classPath">The full source file path of the caller, supplied automatically by the compiler.</param>
        /// <param name="methodName">The name of the calling member, supplied automatically by the compiler.</param>
        /// <param name="lineNumber">The source line number of the caller, supplied automatically by the compiler.</param>
        /// <example>
        /// <code>
        /// LogEvent.Critical(
        ///     avatarUserId:       "jsmith",
        ///     tngnWsvcDataFolder: tngnWsvcSession.Framework.TngnWsvcDataFolder,
        ///     exeAsm:             AvatarOptionObject.ExeAsm,
        ///     logTitle:           "WSVC9999",
        ///     logMsg:             "Unhandled exception while processing request.");
        /// </code>
        /// </example>
        internal static void Critical(string avatarUserId, dynamic tngnWsvcDataFolder, string exeAsm, string logTitle = "Unknown", string logMsg = "Unknown", [CallerFilePath] string classPath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0) =>
            CriticalLog.Create(avatarUserId, tngnWsvcDataFolder, logTitle, logMsg, exeAsm, classPath, methodName, lineNumber);

        /// <summary>Writes an error log entry to the session and shared log folders.</summary>
        /// <remarks>
        /// The <paramref name="classPath"/>, <paramref name="methodName"/>, and <paramref name="lineNumber"/>
        /// parameters are populated automatically by the compiler via caller-info attributes.
        /// </remarks>
        /// <param name="avatarUserId">The Avatar user identifier used when building the log file name.</param>
        /// <param name="tngnWsvcDataFolder">The data folder object that provides the blueprint, session, and log paths.</param>
        /// <param name="exeAsm">The assembly name associated with the error entry.</param>
        /// <param name="errCode">The application-specific error code used when building the log file name.</param>
        /// <param name="errMsg">The error message to record.</param>
        /// <param name="classPath">The full source file path of the caller, supplied automatically by the compiler.</param>
        /// <param name="methodName">The name of the calling member, supplied automatically by the compiler.</param>
        /// <param name="lineNumber">The source line number of the caller, supplied automatically by the compiler.</param>
        /// <example>
        /// <code>
        /// LogEvent.Error(
        ///     avatarUserId:       "jsmith",
        ///     tngnWsvcDataFolder: tngnWsvcSession.Framework.TngnWsvcDataFolder,
        ///     exeAsm:             AvatarOptionObject.ExeAsm,
        ///     errCode:            "WSVC2491",
        ///     errMsg:             "The form ID was not found in the translation table.");
        /// </code>
        /// </example>
        internal static void Error(string avatarUserId, dynamic tngnWsvcDataFolder, string exeAsm, string errCode = "E###", string errMsg = "Unknown error.", [CallerFilePath] string classPath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0) =>
            ErrorLog.Create(avatarUserId, tngnWsvcDataFolder, exeAsm, errCode, errMsg, classPath, methodName, lineNumber);

        /// <summary>Writes a trace log entry when the supplied trace level is within the configured limit.</summary>
        /// <remarks>
        /// The <paramref name="classPath"/>, <paramref name="methodName"/>, and <paramref name="lineNumber"/>
        /// parameters are populated automatically by the compiler via caller-info attributes.
        /// </remarks>
        /// <param name="traceLevel">The trace level for this log entry.</param>
        /// <param name="traceLogLimit">The configured maximum trace level that should be written.</param>
        /// <param name="sessionFolder">The session folder where the trace log file will be written.</param>
        /// <param name="exeAsm">The assembly name associated with the trace entry.</param>
        /// <param name="classPath">The full source file path of the caller, supplied automatically by the compiler.</param>
        /// <param name="methodName">The name of the calling member, supplied automatically by the compiler.</param>
        /// <param name="lineNumber">The source line number of the caller, supplied automatically by the compiler.</param>
        /// <example>
        /// <code>
        /// LogEvent.Trace(
        ///     traceLevel:    1,
        ///     traceLogLimit: tngnWsvcSession.LogSetting.TraceLogLimit,
        ///     sessionFolder: tngnWsvcSession.Framework.TngnWsvcDataFolder.Session,
        ///     exeAsm:        AvatarOptionObject.ExeAsm);
        /// </code>
        /// </example>
        internal static void Trace(int traceLevel, int traceLogLimit, string sessionFolder, string exeAsm, [CallerFilePath] string classPath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0) =>
            TraceLog.Create(traceLevel, traceLogLimit, sessionFolder, exeAsm, classPath, methodName, lineNumber);

        /// <summary>Appends a history log entry to the session's history log file.</summary>
        /// <param name="logFolder">The folder that contains the history log file.</param>
        /// <param name="sessionDate">The session date portion used to build the history log file name.</param>
        /// <param name="logMsg">The message to append to the history log file.</param>
        /// <example>
        /// <code>
        /// LogEvent.History(
        ///     logFolder:   tngnWsvcSession.Framework.TngnWsvcDataFolder.History,
        ///     sessionDate: "2026-05-15",
        ///     logMsg:      $"[{HistoryLog.Timestamp()}] Service started.{System.Environment.NewLine}");
        /// </code>
        /// </example>
        internal static void History(string logFolder, string sessionDate, string logMsg) =>
            HistoryLog.Create(logFolder, sessionDate, logMsg);
    }
}