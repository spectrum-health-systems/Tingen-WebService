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
            Logger.LogEvent.Primeval("SessionMaintenance");

            var dailyDate = DateTime.Now.ToString("yyMMdd");

            if (!File.Exists(Path.Combine(twsFramework.SysLogRoot, $"{dailyDate}.daily")))
            {
                ResetSyslogs(twsFramework.SysLogRoot);
                VerifyComponents($"{dailyDate}.daily", rtConfig, twsFramework);
            }
        }

        internal static void ResetSyslogs(string sysLogPath)
        {
            // TODO - Clean this up, there is a better way to do this.

            if (File.Exists(Path.Combine(sysLogPath, "Configuration.current")))
            {
                File.Delete(Path.Combine(sysLogPath, "Configuration.current"));
            }

            if (File.Exists(Path.Combine(sysLogPath, "Framework.current")))
            {
                File.Delete(Path.Combine(sysLogPath, "Framework.current"));
            }

            if (File.Exists(Path.Combine(sysLogPath, "Runtime.current")))
            {
                File.Delete(Path.Combine(sysLogPath, "Runtime.current"));
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
            Logger.LogEvent.Primeval("VerifyComponents");

            var verificationLog = string.Empty;

            var verificationStartTime = DateTime.Now.ToString("HHmmss");
            var verificationStartMilliseconds = DateTime.Now.ToString("fffffff");

            verificationLog += $"[Start] {verificationStartTime}:{verificationStartMilliseconds}{Environment.NewLine}";
            verificationLog += Epistle.DailyStart(rtConfig.ReleaseBuild);

            Framework.Verify(twsFramework);
            verificationLog += Epistle.FrameworkVerified();

            Framework.ExportBlueprints(twsFramework.BlueprintRoot);
            verificationLog += Epistle.BlueprintsExported();

            var verificationEndTime = DateTime.Now.ToString("HHmmss");
            var verificationEndMilliseconds = DateTime.Now.ToString("fffffff");

            // TODO - This is in a few places, and might be better in a common area.
            var verificationDurationTime = (DateTime.ParseExact(verificationEndTime, "HHmmss", null) - DateTime.ParseExact(verificationStartTime, "HHmmss", null)).ToString(@"hh\:mm\:ss");
            var verificationDurationMilliseconds = (DateTime.ParseExact(verificationEndMilliseconds, "fffffff", null) - DateTime.ParseExact(verificationStartMilliseconds, "fffffff", null)).ToString("fffffff");

            verificationLog += $"[End] {verificationEndTime}:{verificationEndMilliseconds}{Environment.NewLine}[Duration] {verificationDurationTime}:{verificationDurationMilliseconds}";

            LogEvent.SystemLog(twsFramework.SysLogRoot, sysLogFileName, verificationLog);
        }
    }
}