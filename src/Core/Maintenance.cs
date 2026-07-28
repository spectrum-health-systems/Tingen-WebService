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

            var verificationLog = string.Empty;

            var verificationStart = DateTime.Now.ToString("HHmmss");

            verificationLog += $"> Start: {verificationStart}";
            verificationLog += Epistle.DailyStart(rtConfig.ReleaseBuild);
            Framework.Verify(twsFramework);
            verificationLog += Epistle.FrameworkVerified();

            Framework.ExportBlueprints(twsFramework.BlueprintRoot);
            verificationLog += Epistle.BlueprintsExported();

            var verificationEnd = DateTime.Now.ToString("HHmmss");

            var verificationDuration = (DateTime.ParseExact(verificationEnd, "HHmmss", null) - DateTime.ParseExact(verificationStart, "HHmmss", null)).ToString(@"hh\:mm\:ss");

            verificationLog += $"> End: {verificationEnd}{Environment.NewLine}> Duration: {verificationDuration}";

            LogEvent.SystemLog(twsFramework.SysLogRoot, sysLogFileName, verificationLog);
        }
    }
}