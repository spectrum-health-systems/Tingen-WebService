// 260725_code
// 260725_documentation

using System;
using System.IO;
using TingenWebService.Configuration;
using TingenWebService.Du;
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

            var dailyLogPath = Path.Combine(twsFramework.SysLogRoot, $"{DateTime.Now:yyMMdd}");

            try
            {
                DuDirectory.EnsureDirectoryExists(dailyLogPath);
            }
            catch (Exception ex)
            {
                /* Use a primeval log to log the error, since the logging functionality is not initialized yet.
                 */
                LogEvent.Primeval($"[CR7516]Maintenance", ErrorMessage.Error7516(dailyLogPath, ex.Message));
            }

            var dailyStartFile = Path.Combine(dailyLogPath, $"{DateTime.Now:yyMMdd}.start");

            if (!File.Exists(dailyStartFile))
            {
                VerifyComponents(dailyStartFile, rtConfig, twsFramework);
            }
        }

        /// <summary>Verify the components for the session.</summary>
        /// <param name="dailyStartFile">The path to the start log file.</param>
        /// <param name="rtConfig">The runtime configuration.</param>
        /// <param name="twsFramework">The framework instance.</param>
        private static void VerifyComponents(string dailyStartFile, RuntimeConfig rtConfig, Framework twsFramework)
        {
            /* For debugging prior to logging functionality being initialized.
             * Disable in production.
             */
            //LogEvent.Primeval("VerifyComponents");

            if (!File.Exists(dailyStartFile))
            {
                Framework.Verify(twsFramework);
                DuFile.DeadDrop(dailyStartFile, Epistle.DailyStart(rtConfig.ReleaseBuild));
                DuFile.DeadDropAppend(dailyStartFile, Epistle.FrameworkVerified());

                Framework.ExportBlueprints(twsFramework.BlueprintRoot);
                DuFile.DeadDropAppend(dailyStartFile, Epistle.BlueprintsExported());
            }
        }
    }
}