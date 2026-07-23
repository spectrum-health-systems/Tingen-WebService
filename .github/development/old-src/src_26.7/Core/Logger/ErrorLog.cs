// 251112_code
// 260515_documentation.

using System;
using System.IO;

namespace TingenWebService.Core.Logger
{
    /// <summary>Provides helpers for creating error log files.</summary>
    internal static class ErrorLog
    {
        /// <summary>Creates error log files for the current user session and the shared log folder.</summary>
        /// <remarks>
        /// This method loads the error blueprint, formats the log content, and writes the resulting log file to both
        /// the session-specific log folder and the shared log folder.
        /// </remarks>
        /// <param name="avatarUserId">The Avatar user identifier used when building the log file name.</param>
        /// <param name="tngnWsvcDataFolder">The data folder object that provides the blueprint, session, and log paths.</param>
        /// <param name="exeAsm">The assembly name associated with the error entry.</param>
        /// <param name="errCode">The application-specific error code used when building the log file name.</param>
        /// <param name="errMsg">The error message to include in the log entry.</param>
        /// <param name="classPath">The full class path associated with the error entry.</param>
        /// <param name="methodName">The method name associated with the error entry.</param>
        /// <param name="lineNumber">The source line number associated with the error entry.</param>
        /// <example>
        /// <code>
        /// ErrorLog.Create(
        ///     avatarUserId:       "jsmith",
        ///     tngnWsvcDataFolder: tngnWsvcSession.Framework.TngnWsvcDataFolder,
        ///     exeAsm:             AvatarOptionObject.ExeAsm,
        ///     errCode:            "WSVC2491",
        ///     errMsg:             "The form ID was not found in the translation table.",
        ///     classPath:          "Core/Avatar/AvatarScriptParameter.cs",
        ///     methodName:         "SpecificFormRequest",
        ///     lineNumber:         62);
        /// // Writes:
        /// //   {Session}\WSVC2491-jsmith.error
        /// //   {Log}\WSVC2491-jsmith.error
        /// </code>
        /// </example>
        internal static void Create(string avatarUserId, dynamic tngnWsvcDataFolder, string exeAsm, string errCode, string errMsg, string classPath, string methodName, int lineNumber)
        {
            var logName      = $"{errCode}-{avatarUserId}.error";
            var logBlueprint = File.ReadAllText($@"{tngnWsvcDataFolder.Blueprint}\error.blueprint");
            var logContent   = LogContent(logBlueprint, errCode, errMsg, exeAsm, classPath, methodName, lineNumber);

            LogUtility.WriteLocal($@"{tngnWsvcDataFolder.Session}\{logName}", logContent);
            LogUtility.WriteLocal($@"{tngnWsvcDataFolder.Log}\{logName}", logContent);
        }

        /// <summary>Builds the error log content by replacing blueprint tokens with runtime values.</summary>
        /// <remarks>
        /// The returned string is created from <paramref name="logBlueprint"/> by replacing the standard log
        /// placeholders with the provided error, assembly, class, method, and line values.
        /// </remarks>
        /// <param name="logBlueprint">The blueprint template containing the placeholders to replace.</param>
        /// <param name="errCode">The error code to insert into the log content.</param>
        /// <param name="errMsg">The error message to insert into the log content.</param>
        /// <param name="exeAsm">The assembly name to insert into the log content.</param>
        /// <param name="classPath">The full class path used to derive the class name for the log content.</param>
        /// <param name="methodName">The method name to insert into the log content.</param>
        /// <param name="lineNumber">The source line number to insert into the log content.</param>
        /// <returns>A formatted error log entry string.</returns>
        /// <example>
        /// <code>
        /// var blueprint =
        ///     "[~SESSION~DATE~TIME~] [~ERROR~CODE~] ~ASSEMBLY~::~CLASS~.~METHOD~ (line ~LINE~): ~ERROR~MESSAGE~";
        ///
        /// var content = ErrorLog.LogContent(
        ///     logBlueprint: blueprint,
        ///     errCode:      "WSVC2491",
        ///     errMsg:       "The form ID was not found in the translation table.",
        ///     exeAsm:       "TingenWebService",
        ///     classPath:    "Core/Avatar/AvatarScriptParameter.cs",
        ///     methodName:   "SpecificFormRequest",
        ///     lineNumber:   62);
        ///
        /// Console.WriteLine(content);
        /// // Example output:
        /// // [05/15/2026-10:30:45] [WSVC2491] TingenWebService::AvatarScriptParameter.SpecificFormRequest (line 62): The form ID was not found in the translation table.
        /// </code>
        /// </example>
        internal static string LogContent(string logBlueprint, string errCode, string errMsg, string exeAsm, string classPath, string methodName, int lineNumber) =>
            logBlueprint.Replace("~SESSION~DATE~TIME~", DateTime.Now.ToString("MM/dd/yyyy-HH:mm:ss"))
                        .Replace("~ERROR~CODE~", errCode)
                        .Replace("~ERROR~MESSAGE~", errMsg)
                        .Replace("~ASSEMBLY~", exeAsm)
                        .Replace("~CLASS~", LogUtility.GetClassName(classPath))
                        .Replace("~METHOD~", methodName)
                        .Replace("~LINE~", lineNumber.ToString());
    }
}