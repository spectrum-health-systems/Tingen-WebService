// 260811_code
// 260811_documentation

using System;
using System.IO;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Trove;
using TingenWebService.Du;

namespace TingenWebService.Core.Framework
{
    /// <summary>Provides methods for maintaining the Tingen Web Service framework.</summary>
    /// <remarks>
    /// The Tingen Web Service framework consists of required files, folders, and other data that are necessary for the Tingen
    /// Web Service to function properly.
    /// </remarks>
    internal static class FrameworkMaintenance
    {
        /// <summary>Validate the Tingen Web Service framework daily.</summary>
        /// <param name="runtimeSetting">The runtime configuration.</param>
        /// <param name="frameworkSetting">The framework instance.</param>
        internal static void DailyValidation(RuntimeSetting runtimeSetting, FrameworkSetting frameworkSetting)
        {
            //LogEvent.Primeval("PRELOG-TRACE-FrameworkMaintenance-DailyValidation");

            var todayDate = DateTime.Now.ToString("yyyyMMdd");

            if (!File.Exists(Path.Combine(frameworkSetting.SysLogRoot, $"{todayDate}.daily")))
            {
                VerifyComponents(todayDate, runtimeSetting, frameworkSetting);
            }
        }

        /// <summary>Verify the components for the session.</summary>
        /// <param name="dailyLogFileName">The name of the daily log file.</param>
        /// <param name="runtimeSetting">The runtime configuration.</param>
        /// <param name="frameworkSetting">The framework instance.</param>
        private static void VerifyComponents(string todayDate, RuntimeSetting runtimeSetting, FrameworkSetting frameworkSetting)
        {
            //LogEvent.Primeval("PRELOG-TRACE-FrameworkMaintenance-VerifyComponents");

            var startTime = DateTime.Now.ToString("HHmmss");
            var startMs   = DateTime.Now.ToString("fffffff");

            VerifyStructure(frameworkSetting);
            string runningDailyLog = $"Framework: OK{Environment.NewLine}";

            LogMaintenance.RecreateSystemLogs(frameworkSetting, runtimeSetting);
            runningDailyLog += $"System logs: OK{Environment.NewLine}";

            Blueprint.ExportBlueprints(frameworkSetting.BlueprintRoot);
            runningDailyLog += $"Blueprints: OK{Environment.NewLine}";

            Translation.ExportTranslations(frameworkSetting.TranslationRoot);
            runningDailyLog += $"Translation files: OK{Environment.NewLine}";

            var endTime   = DateTime.Now.ToString("HHmmss");
            var endMs     = DateTime.Now.ToString("fffffff");

            var duration = Utility.TimeDuration.GetDuration(startTime, startMs, endTime, endMs);

            LogEvent.Daily(frameworkSetting.SysLogRoot, todayDate, startTime, runtimeSetting.VersionBuild, runningDailyLog, duration);
        }

        /// <summary>Verify the Tingen Web Service framework.</summary>
        /// <param name="frwkConfig">The framework instance containing the paths to verify.</param>
        /// <remarks>
        /// The reason why we hand this off is because while currently the configuration contains root paths, it may
        /// contain other data in the future.
        /// </remarks>
        private static void VerifyStructure(FrameworkSetting frameworkSetting)
        {
            //LogEvent.Primeval("PRELOG-TRACE-FrameworkMaintenance-VerifyStructure");

            foreach (var path in Catalog.RequiredFolders(frameworkSetting))
            {
                try
                {
                    DuDirectory.ForceExist(path);
                }
                catch (Exception ex)
                {
                    LogEvent.Primeval("ERR1000-FrameworkValidationFailed", ErrorMessage.ERR1000(ex.Message));
                    //TODO - Should probably send an email notification.
                }
            }
        }
    }
}