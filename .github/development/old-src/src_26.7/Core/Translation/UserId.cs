// 251112_code
// 260515_documentation

using System;
using System.IO;
using System.Reflection;
using TingenWebService.Core.Logger;

namespace TingenWebService.Core.Translation
{
    /// <summary>Translates Avatar user identifiers into human-readable user descriptions.</summary>
    /// <remarks>
    /// <para>
    /// <c>UserId</c> performs lookups against the <c>USERID_UserDescription_Active.translation</c>
    /// file, which maps Avatar user names to friendly user descriptions. The file is generated from
    /// a source listing by <see cref="CreateTranslationFile(string, string, int, string)"/> during
    /// daily maintenance, and is then read at request time by
    /// <see cref="GetUserDescription(string, int, string, string)"/>.
    /// </para>
    /// <para>
    /// Each entry in the translation file uses the caret character (<c>^</c>) as a delimiter between
    /// the user name and the user description.
    /// </para>
    /// </remarks>
    internal static class UserId
    {
        /// <include file='AppData/XmlDocumentation/TngnWsvc.xml' path='TngnWsvc/Class[@name="CommonDefinition"]/ExecutingAssembly/*'/>
        public static string ExeAsm { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;


        /// <summary>Returns the user description associated with the supplied Avatar user name.</summary>
        /// <param name="avatarUserName">The Avatar user name to look up; leading and trailing whitespace is ignored.</param>
        /// <param name="traceLogLimit">The maximum number of trace log files retained for the active session.</param>
        /// <param name="currentSessionFolder">The absolute path to the current Tingen Web Service session folder.</param>
        /// <param name="translationPath">The absolute path to the folder that contains <c>USERID_UserDescription_Active.translation</c>.</param>
        /// <returns>The user description that matches <paramref name="avatarUserName"/>, or the literal error code <c>"WSVC4274"</c> when no match is found.</returns>
        /// <remarks>
        /// <para>
        /// Each line in the translation file is split on the caret character (<c>^</c>); the portion
        /// to the left is the Avatar user name and the portion to the right is the user description.
        /// The first matching entry wins. If no entry matches, the error code <c>"WSVC4274"</c> is
        /// returned to indicate that the user name is not present in the translation file.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// string description = UserId.GetUserDescription(
        ///     avatarUserName:       "JDOE",
        ///     traceLogLimit:        tngnWsvcSession.LogSetting.TraceLogLimit,
        ///     currentSessionFolder: tngnWsvcSession.Framework.TngnWsvcDataFolder.Session,
        ///     translationPath:      tngnWsvcSession.Framework.TngnWsvcDataFolder.Translation);
        /// </code>
        /// </example>
        internal static string GetUserDescription(string avatarUserName, int traceLogLimit, string currentSessionFolder, string translationPath)
        {
            LogEvent.Trace(1, traceLogLimit, currentSessionFolder, ExeAsm);

            foreach (string line in File.ReadLines($@"{translationPath}\USERID_UserDescription_Active.translation"))
            {
                // split into two parts

                var fileUserName = line.Split('^')[0];

                if (fileUserName == avatarUserName.Trim())
                {
                    LogEvent.Trace(4, traceLogLimit, currentSessionFolder, ExeAsm);

                    return line.Split('^')[1];
                }
            }

            return "WSVC4274"; // Your username was not found in the USERID_User Description.txt file.
        }

        /// <summary>Generates the active user-description translation file from a source listing.</summary>
        /// <param name="originalFilePath">The absolute path to the source file that contains the raw user-description entries.</param>
        /// <param name="translationPath">The absolute path to the folder where <c>USERID_UserDescription_Active.translation</c> should be written.</param>
        /// <param name="traceLogLimit">The maximum number of trace log files retained for the active session.</param>
        /// <param name="currentSessionFolder">The absolute path to the current Tingen Web Service session folder.</param>
        /// <remarks>
        /// <para>
        /// The source file is read line by line; blank lines are skipped, and each remaining line is
        /// trimmed and appended to the output buffer with a trailing newline. The accumulated content
        /// is then written to <c>USERID_UserDescription_Active.translation</c> under
        /// <paramref name="translationPath"/>.
        /// </para>
        /// <para>
        /// Trace logging inside the loop uses a high limit (<c>9</c>) so that high-volume per-line
        /// trace entries do not flood the session trace log unless trace logging has been explicitly
        /// raised to that level.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// UserId.CreateTranslationFile(
        ///     originalFilePath:     @"C:\Tingen_Data\Cache\USERID_UserDescription.txt",
        ///     translationPath:      tngnWsvcSession.Framework.TngnWsvcDataFolder.Translation,
        ///     traceLogLimit:        tngnWsvcSession.LogSetting.TraceLogLimit,
        ///     currentSessionFolder: tngnWsvcSession.Framework.TngnWsvcDataFolder.Session);
        /// </code>
        /// </example>
        internal static void CreateTranslationFile(string originalFilePath, string translationPath, int traceLogLimit, string currentSessionFolder)
        {
            /* This trace log has a limit of 9 to avoid excessive logging in loops.
             */
            LogEvent.Trace(9, traceLogLimit, currentSessionFolder, ExeAsm);

            // Put another type of log here

            var line = string.Empty;

            foreach (string fileLine in File.ReadLines(originalFilePath))
            {
                /* This trace log has a limit of 9 to avoid excessive logging in loops.
                 */
                LogEvent.Trace(9, traceLogLimit, currentSessionFolder, ExeAsm);

                if (!string.IsNullOrWhiteSpace(fileLine))
                {
                    /* This trace log has a limit of 9 to avoid excessive logging in loops.
                     */
                    LogEvent.Trace(9, traceLogLimit, currentSessionFolder, ExeAsm);

                    line += fileLine.Trim() + Environment.NewLine;
                }
            }

            File.WriteAllText($@"{translationPath}\USERID_UserDescription_Active.translation", line);
        }
    }
}