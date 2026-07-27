// 260727_code
// 260727_documentation

using System;
using System.IO;
using TingenWebService.Configuration;
using TingenWebService.Logger;
using TingenWebService.Trove;

namespace TingenWebService.Core
{
    /// <summary>Maintenance operations.</summary>
    internal class Maintenance
    {
        /// <summary>Session maintenance.</summary>
        /// <remarks>
        /// This maintenance is done for every session.
        /// </remarks>
        /// <param name="rtConfig">The runtime configuration.</param>
        /// <param name="twsFramework">The framework instance.</param>
        internal static void SessionMaintenance(RuntimeConfig rtConfig, Framework twsFramework)
        {
            /* For debugging prior to logging functionality being initialized.
             * Disable in production.
             */
            //LogEvent.Primeval("SessionMaintenance");

            //var startDate      = DateTime.Now.ToString("yyMMdd");
            //var dailyStartFile = Path.Combine(twsFramework.SysLogRoot, $"{startDate}.start");

            //try
            //{
            //    DuDirectory.EnsureDirectoryExists(dailyLogFolder);
            //}
            //catch (Exception ex)
            //{
            //    /* Use a primeval log to log the error, since the logging functionality is not initialized yet.
            //     */
            //    LogEvent.Primeval($"[CR7516]Maintenance", ErrorMessage.Error7516(dailyLogFolder, ex.Message));
            //}

            //var dailyStartFile = Path.Combine(dailyLogFolder, $"{DateTime.Now:yyMMdd}.start");

            var startDate = DateTime.Now.ToString("yyMMdd");

            if (!File.Exists(Path.Combine(twsFramework.SysLogRoot, $"{startDate}.start")))
            {
                VerifyComponents($"{startDate}.start", rtConfig, twsFramework);
            }
        }

        /// <summary>Verify the components for the session.</summary>
        /// <param name="sysLogRoot">The root directory for system logs.</param>
        /// <param name="dailyStartFileName">The name of the start log file.</param>
        /// <param name="rtConfig">The runtime configuration.</param>
        /// <param name="twsFramework">The framework instance.</param>
        private static void VerifyComponents(string sysLogFileName, RuntimeConfig rtConfig, Framework twsFramework)
        {
            /* For debugging prior to logging functionality being initialized.
             * Disable in production.
             */
            //LogEvent.Primeval("VerifyComponents");

            var runningLog = string.Empty;

            //var dailyStartFile = Path.Combine(twsFramework.SysLogRoot, dailyStartFileName);

            //if (!File.Exists(Path.Combine(twsFramework.SysLogRoot, dailyStartFileName)))
            //{
            var verificationStart = DateTime.Now.ToString("HHmmss");

            //var logName = $"{verificationStart}-DailyLog.log";

            runningLog += Epistle.DailyStart(rtConfig.ReleaseBuild);
            Framework.Verify(twsFramework);
            runningLog += Epistle.FrameworkVerified();
            //LogEvent.SystemLog(twsFramework.SysLogRoot, sysLogFileName, Epistle.DailyStart(rtConfig.ReleaseBuild));
            //LogEvent.SystemLog(twsFramework.SysLogRoot, sysLogFileName, Epistle.DailyStart(Epistle.FrameworkVerified()));
            //DuFile.DeadDrop(dailyStartFile, Epistle.DailyStart(rtConfig.ReleaseBuild));
            //DuFile.DeadDropAppend(dailyStartFile, Epistle.FrameworkVerified());

            Framework.ExportBlueprints(twsFramework.BlueprintRoot);
            runningLog += Epistle.BlueprintsExported();
            //LogEvent.SystemLog(twsFramework.SysLogRoot, sysLogFileName, Epistle.DailyStart(Epistle.BlueprintsExported()));
            //DuFile.DeadDropAppend(dailyStartFile, Epistle.BlueprintsExported());

            var verificationEnd = DateTime.Now.ToString("HHmmss");

            var verificationDuration = (DateTime.ParseExact(verificationEnd, "HHmmss", null) - DateTime.ParseExact(verificationStart, "HHmmss", null)).ToString(@"hh\:mm\:ss");

            runningLog += $"Start: {verificationStart} | End: {verificationEnd} | Duration: {verificationDuration}";

            LogEvent.SystemLog(twsFramework.SysLogRoot, sysLogFileName, runningLog);
            //DuFile.DeadDropAppend(dailyStartFile, runningLog);
            //}
        }
    }
}