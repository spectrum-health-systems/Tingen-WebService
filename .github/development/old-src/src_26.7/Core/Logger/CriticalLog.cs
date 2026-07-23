// 251112_code
// 260515_documentation.

using System;
using System.IO;

namespace TingenWebService.Core.Logger
{
    /// <summary>Provides helpers for creating critical error log files.</summary>
    internal static class CriticalLog
    {
        /// <summary>Creates critical log files for the current user session and the shared log folder.</summary>
        /// <remarks>
        /// This method loads the critical error blueprint, formats the log content, and writes the resulting log file
        /// to both the session-specific log folder and the shared log folder.
        /// </remarks>
        /// <param name="avatarUserId">The Avatar user identifier used when building the log file name.</param>
        /// <param name="tngnWsvcDataFolder">The data folder object that provides the blueprint, session, and log paths.</param>
        /// <param name="logTitle">The title prefix used when building the log file name.</param>
        /// <param name="logMsg">The message to include in the critical log entry.</param>
        /// <param name="exeAsm">The assembly name associated with the log entry.</param>
        /// <param name="classPath">The full class path associated with the log entry.</param>
        /// <param name="methodName">The method name associated with the log entry.</param>
        /// <param name="lineNumber">The source line number associated with the log entry.</param>
        /// <example>
        /// <code>
        /// CriticalLog.Create(
        ///     avatarUserId:       "jsmith",
        ///     tngnWsvcDataFolder: tngnWsvcSession.Framework.TngnWsvcDataFolder,
        ///     logTitle:           "WSVC9999",
        ///     logMsg:             "Unhandled exception while processing request.",
        ///     exeAsm:             AvatarOptionObject.ExeAsm,
        ///     classPath:          "Core/Avatar/AvatarOptionObject.cs",
        ///     methodName:         "ToReturn",
        ///     lineNumber:         57);
        /// // Writes:
        /// //   {Session}\WSVC9999-jsmith.critical
        /// //   {Log}\WSVC9999-jsmith.critical
        /// </code>
        /// </example>
        internal static void Create(string avatarUserId, dynamic tngnWsvcDataFolder, string logTitle, string logMsg, string exeAsm, string classPath, string methodName, int lineNumber)
        {
            var logName      = $"{logTitle}-{avatarUserId}.critical";
            var logBlueprint = File.ReadAllText($@"{tngnWsvcDataFolder.Blueprint}\critical-error.blueprint");
            var logContent   = LogContent(logBlueprint, logMsg, exeAsm, classPath, methodName, lineNumber);

            LogUtility.WriteLocal($@"{tngnWsvcDataFolder.Session}\{logName}", logContent);
            LogUtility.WriteLocal($@"{tngnWsvcDataFolder.Log}\{logName}", logContent);
        }

        /// <summary>Builds the critical log content by replacing blueprint tokens with runtime values.</summary>
        /// <remarks>
        /// The returned string is created from <paramref name="logBlueprint"/> by replacing the standard log
        /// placeholders with the provided message, assembly, class, method, and line values.
        /// </remarks>
        /// <param name="logBlueprint">The blueprint template containing the placeholders to replace.</param>
        /// <param name="logMsg">The message to insert into the log content.</param>
        /// <param name="exeAsm">The assembly name to insert into the log content.</param>
        /// <param name="classPath">The full class path used to derive the class name for the log content.</param>
        /// <param name="methodName">The method name to insert into the log content.</param>
        /// <param name="lineNumber">The source line number to insert into the log content.</param>
        /// <returns>A formatted critical log entry string.</returns>
        /// <example>
        /// <code>
        /// var blueprint =
        ///     "[~SESSION~DATE~TIME~] ~ASSEMBLY~::~CLASS~.~METHOD~ (line ~LINE~): ~LOG~MESSAGE~";
        ///
        /// var content = CriticalLog.LogContent(
        ///     logBlueprint: blueprint,
        ///     logMsg:       "Database connection failed.",
        ///     exeAsm:       "TingenWebService",
        ///     classPath:    "Core/Avatar/AvatarOptionObject.cs",
        ///     methodName:   "ToReturn",
        ///     lineNumber:   57);
        ///
        /// Console.WriteLine(content);
        /// // Example output:
        /// // [05/15/2026-10:30:45] TingenWebService::AvatarOptionObject.ToReturn (line 57): Database connection failed.
        /// </code>
        /// </example>
        internal static string LogContent(string logBlueprint, string logMsg, string exeAsm, string classPath, string methodName, int lineNumber) =>
           logBlueprint.Replace("~SESSION~DATE~TIME~", DateTime.Now.ToString("MM/dd/yyyy-HH:mm:ss"))
                       .Replace("~LOG~MESSAGE~", logMsg)
                       .Replace("~ASSEMBLY~", exeAsm)
                       .Replace("~CLASS~", LogUtility.GetClassName(classPath))
                       .Replace("~METHOD~", methodName)
                       .Replace("~LINE~", lineNumber.ToString());
    }
}