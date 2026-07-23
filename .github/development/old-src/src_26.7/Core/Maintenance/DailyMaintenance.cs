// 251112_code
// 260515_documentation.

using System;
using System.IO;
using System.Reflection;
using TingenWebService.Core.Catalog;
using TingenWebService.Core.Framework;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Query;
using TingenWebService.Core.TingenWsvcSession;

namespace TingenWebService.Core.Maintenance
{
    /// <summary>Performs daily maintenance tasks for the Tingen Web Service.</summary>
    internal static class DailyMaintenance
    {
        /// <include file='AppData/XmlDocumentation/TngnWsvc.xml' path='TngnWsvc/Class[@name="CommonDefinition"]/ExecutingAssembly/*'/>
        public static string ExeAsm { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>Performs a quick daily-maintenance check for the current session.</summary>
        /// <remarks>
        /// Runs the full <see cref="VerifyTingenWebService"/> sequence when the day's history file does not
        /// yet exist; otherwise returns without performing maintenance.
        /// </remarks>
        /// <param name="tngnWsvcSession">The Tingen Web Service session that exposes runtime, framework, and log settings.</param>
        /// <example>
        /// <code>
        /// DailyMaintenance.QuickCheck(tngnWsvcSession);
        /// </code>
        /// </example>
        internal static void QuickCheck(TngnWsvcSession tngnWsvcSession)
        {
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

            var pathFour        = Path.Combine(tngnWsvcSession.Framework.TngnWsvcDataFolder.Config, "pathFour.txt");
            File.WriteAllText(pathFour, "Path Four");

            if (!File.Exists($@"{tngnWsvcSession.Framework.TngnWsvcDataFolder.History}\{tngnWsvcSession.Runtime.SessionDate}.history"))
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                var pathFive        = Path.Combine(tngnWsvcSession.Framework.TngnWsvcDataFolder.Config, "pathFive.txt");
                File.WriteAllText(pathFive, "Path Five");

                VerifyTingenWebService(tngnWsvcSession);

                var pathSix       = Path.Combine(tngnWsvcSession.Framework.TngnWsvcDataFolder.Config, "pathSix.txt");
                File.WriteAllText(pathSix, "Path Six");
            }

            var pathSeven             = Path.Combine(tngnWsvcSession.Framework.TngnWsvcDataFolder.Config, "pathSeven.txt");
            File.WriteAllText(pathSeven, "Path Seven");
        }

        /// <summary>Verifies the Tingen Web Service framework and refreshes its supporting data.</summary>
        /// <remarks>
        /// Verifies the framework folder structure, then refreshes the translation tables and blueprints
        /// for the current session.
        /// </remarks>
        /// <param name="tngnWsvcSession">The Tingen Web Service session that exposes framework, runtime, and log settings.</param>
        /// <example>
        /// <code>
        /// DailyMaintenance.VerifyTingenWebService(tngnWsvcSession);
        /// </code>
        /// </example>
        // Should also have a "reset"
        internal static void VerifyTingenWebService(TngnWsvcSession tngnWsvcSession)
        {
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

            TngnWsvcFramework.Verify(tngnWsvcSession.Framework, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Runtime.AvatarSystem, tngnWsvcSession.Runtime.SessionDate);

            //Core.Framework.AppDataFolders.CreateFramework(tngnWsvcSession.Folder);

            //CreateHistoryPath(tngnWsvcSession.Folder.History, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.SessionFolder);

            RefreshTranslationTables(tngnWsvcSession.Framework.TngnWsvcDataFolder.AvatarGeneratedData, tngnWsvcSession.Framework.TngnWsvcDataFolder.TranslationTable, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, tngnWsvcSession.Framework.TngnWsvcDataFolder.History, tngnWsvcSession.Runtime.SessionDate);
            RefreshBlueprints(tngnWsvcSession.Framework.TngnWsvcWwwFolder.Blueprint, tngnWsvcSession.Framework.TngnWsvcDataFolder.Blueprint, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, tngnWsvcSession.Framework.TngnWsvcDataFolder.History, tngnWsvcSession.Runtime.SessionDate);
        }

        /// <summary>Ensures the history folder exists, creating it when missing.</summary>
        /// <param name="historyPath">The absolute path to the history folder.</param>
        /// <param name="traceLogLimit">The configured trace log limit used while tracing this operation.</param>
        /// <param name="sessionFolder">The current session folder used for trace output.</param>
        /// <example>
        /// <code>
        /// DailyMaintenance.CreateHistoryPath(
        ///     historyPath:   tngnWsvcSession.Framework.TngnWsvcDataFolder.History,
        ///     traceLogLimit: tngnWsvcSession.LogSetting.TraceLogLimit,
        ///     sessionFolder: tngnWsvcSession.Framework.TngnWsvcDataFolder.Session);
        /// </code>
        /// </example>
        internal static void CreateHistoryPath(string historyPath, int traceLogLimit, string sessionFolder)
        {
            LogEvent.Trace(1, traceLogLimit, sessionFolder, ExeAsm);

            if (!Directory.Exists(historyPath))
            {
                Directory.CreateDirectory(historyPath);
            }
        }

        /// <summary>Determines whether the daily log path exists.</summary>
        /// <param name="dailyLogPath">The absolute path to the daily logs folder.</param>
        /// <returns><c>true</c> if the daily log path exists; otherwise, <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// if (!DailyMaintenance.DailyLogPathExists(@"C:\Tingen_Data\LIVE\AppData\Log\Daily"))
        /// {
        ///     // create the folder, etc.
        /// }
        /// </code>
        /// </example>
        internal static bool DailyLogPathExists(string dailyLogPath) => Directory.Exists(dailyLogPath);

        /// <summary>Creates the daily log folder when it does not already exist.</summary>
        /// <param name="dailyLogPath">The absolute path to the daily logs folder.</param>
        /// <param name="traceLogLimit">The configured trace log limit used while tracing this operation.</param>
        /// <param name="sessionFolder">The current session folder used for trace output.</param>
        /// <example>
        /// <code>
        /// DailyMaintenance.CreateDailyLogPath(
        ///     dailyLogPath:  @"C:\Tingen_Data\LIVE\AppData\Log\Daily",
        ///     traceLogLimit: tngnWsvcSession.LogSetting.TraceLogLimit,
        ///     sessionFolder: tngnWsvcSession.Framework.TngnWsvcDataFolder.Session);
        /// </code>
        /// </example>
        internal static void CreateDailyLogPath(string dailyLogPath, int traceLogLimit, string sessionFolder)
        {
            LogEvent.Trace(1, traceLogLimit, sessionFolder, ExeAsm);

            if (!Directory.Exists(dailyLogPath))
            {
                Directory.CreateDirectory(dailyLogPath);
            }
        }

        /// <summary>Creates the daily log file for the supplied date when it does not already exist.</summary>
        /// <param name="dailyLogPath">The absolute path to the daily logs folder.</param>
        /// <param name="currentDate">The current date string used to build the daily log file name.</param>
        /// <param name="traceLogLimit">The configured trace log limit used while tracing this operation.</param>
        /// <param name="sessionFolder">The current session folder used for trace output.</param>
        /// <example>
        /// <code>
        /// DailyMaintenance.CreateDailyLog(
        ///     dailyLogPath:  @"C:\Tingen_Data\LIVE\AppData\Log\Daily",
        ///     currentDate:   "2026-05-15",
        ///     traceLogLimit: tngnWsvcSession.LogSetting.TraceLogLimit,
        ///     sessionFolder: tngnWsvcSession.Framework.TngnWsvcDataFolder.Session);
        /// </code>
        /// </example>
        internal static void CreateDailyLog(string dailyLogPath, string currentDate, int traceLogLimit, string sessionFolder)
        {
            LogEvent.Trace(1, traceLogLimit, sessionFolder, ExeAsm);

            if (!File.Exists($@"{dailyLogPath}\daily.{currentDate}"))
            {
                LogEvent.Trace(2, traceLogLimit, sessionFolder, ExeAsm);

                File.Create($@"{dailyLogPath}\daily.{currentDate}").Dispose();
            }
        }

        /// <summary>Refreshes the translation tables by recreating files from the latest generated data.</summary>
        /// <remarks>
        /// Deletes the existing <c>USERID_UserDescription_Active.translation</c> file when present, then
        /// regenerates it from the current Avatar-generated data and records history entries describing the
        /// refresh.
        /// </remarks>
        /// <param name="generatedDataPath">The directory containing the Avatar-generated source data files.</param>
        /// <param name="translationPath">The directory where translation files are stored.</param>
        /// <param name="traceLogLimit">The configured trace log limit used while tracing this operation.</param>
        /// <param name="sessionFolder">The current session folder used for trace output.</param>
        /// <param name="historyFolder">The history folder where refresh events are appended.</param>
        /// <param name="sessionDate">The session date used when building the history log file name.</param>
        /// <example>
        /// <code>
        /// DailyMaintenance.RefreshTranslationTables(
        ///     generatedDataPath: tngnWsvcSession.Framework.TngnWsvcDataFolder.AvatarGeneratedData,
        ///     translationPath:   tngnWsvcSession.Framework.TngnWsvcDataFolder.TranslationTable,
        ///     traceLogLimit:     tngnWsvcSession.LogSetting.TraceLogLimit,
        ///     sessionFolder:     tngnWsvcSession.Framework.TngnWsvcDataFolder.Session,
        ///     historyFolder:     tngnWsvcSession.Framework.TngnWsvcDataFolder.History,
        ///     sessionDate:       tngnWsvcSession.Runtime.SessionDate);
        /// </code>
        /// </example>
        internal static void RefreshTranslationTables(string generatedDataPath, string translationPath, int traceLogLimit, string sessionFolder, string historyFolder, string sessionDate)
        {
            // Move this to the translations tables themselves.

            LogEvent.Trace(1, traceLogLimit, sessionFolder, ExeAsm);

            if (File.Exists($@"{translationPath}\USERID_UserDescription_Active.translation"))
            {
                LogEvent.Trace(2, traceLogLimit, sessionFolder, ExeAsm);
                LogEvent.History(historyFolder, sessionDate, msg_Maintenance.RefreshTranslations("USERID_UserDescription_Active.translation"));

                File.Delete($@"{translationPath}\USERID_UserDescription_Active.translation");
            }

            var generatedUserIdPath = $@"{generatedDataPath}\USERID_UserDescription_Active.txt";

            QueryUserId.CreateTranslationFile(generatedUserIdPath, translationPath, traceLogLimit, sessionFolder);

            LogEvent.History(historyFolder, sessionDate, msg_Maintenance.RefreshTranslations());
        }

        /// <summary>Refreshes blueprint files in the target folder from the source folder.</summary>
        /// <remarks>
        /// Copies each blueprint from <paramref name="blueprintSource"/> to <paramref name="blueprintTarget"/>
        /// when the target file is missing or older than the source, and appends a history entry for each
        /// refreshed blueprint.<br/>
        /// <br/>
        /// Returns immediately if either folder does not exist.
        /// </remarks>
        /// <param name="blueprintSource">The source folder that contains the master blueprint files.</param>
        /// <param name="blueprintTarget">The target folder that receives the refreshed blueprint files.</param>
        /// <param name="traceLogLimit">The configured trace log limit used while tracing this operation.</param>
        /// <param name="sessionFolder">The current session folder used for trace output.</param>
        /// <param name="historyFolder">The history folder where refresh events are appended.</param>
        /// <param name="sessionDate">The session date used when building the history log file name.</param>
        /// <example>
        /// <code>
        /// DailyMaintenance.RefreshBlueprints(
        ///     blueprintSource: tngnWsvcSession.Framework.TngnWsvcWwwFolder.Blueprint,
        ///     blueprintTarget: tngnWsvcSession.Framework.TngnWsvcDataFolder.Blueprint,
        ///     traceLogLimit:   tngnWsvcSession.LogSetting.TraceLogLimit,
        ///     sessionFolder:   tngnWsvcSession.Framework.TngnWsvcDataFolder.Session,
        ///     historyFolder:   tngnWsvcSession.Framework.TngnWsvcDataFolder.History,
        ///     sessionDate:     tngnWsvcSession.Runtime.SessionDate);
        /// </code>
        /// </example>
        internal static void RefreshBlueprints(string blueprintSource, string blueprintTarget, int traceLogLimit, string sessionFolder, string historyFolder, string sessionDate)
        {
            LogEvent.Trace(1, traceLogLimit, sessionFolder, ExeAsm);
            LogEvent.Debug($"{blueprintSource} - {blueprintTarget}");


            if (!Directory.Exists(blueprintSource) || !Directory.Exists(blueprintTarget))
                return;

            LogEvent.Debug();

            var sourceFiles = Directory.GetFiles(blueprintSource);

            foreach (var sourceFile in sourceFiles)
            {
                LogEvent.Trace(2, traceLogLimit, sessionFolder, ExeAsm);

                var fileName   = Path.GetFileName(sourceFile);
                var targetFile = Path.Combine(blueprintTarget, fileName);

                bool shouldCopy = !File.Exists(targetFile) || File.GetLastWriteTimeUtc(sourceFile) > File.GetLastWriteTimeUtc(targetFile);

                if (shouldCopy)
                {
                    LogEvent.Trace(2, traceLogLimit, sessionFolder, ExeAsm);

                    File.Copy(sourceFile, targetFile, true);
                    LogEvent.History(historyFolder, sessionDate, $"[{HistoryLog.Timestamp()}] Blueprint '{fileName}' refreshed.{Environment.NewLine}");
                }
            }
        }
    }
}