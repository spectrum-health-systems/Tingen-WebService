// 260805_code
// 260805_documentation
using System;
using System.IO;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Trove;
using TingenWebService.Du;

namespace TingenWebService.Core.Framework
{
    /// <summary>Provides methods for maintaining the Tingen Web Service framework.</summary>
    internal static class FrameworkMaintenance
    {
        internal static void DailyVerify(RuntimeSetting runtimeConfig, FrameworkSetting frameworkConfig)
        {
            //LogEvent.Primeval("PRELOG-TRACE-FrameworkMaintenance-DailyVerify");

            var dailyLogFileName = $"{DateTime.Now:yyyyMMdd}.daily";

            if (!File.Exists(Path.Combine(frameworkConfig.SysLogRoot, dailyLogFileName)))
            {
                VerifyComponents(dailyLogFileName, runtimeConfig, frameworkConfig);
            }
        }

        /// <summary>Verify the components for the session.</summary>
        /// <param name="systemLogFileName">The name of the start log file.</param>
        /// <param name="runtimeSetting">The runtime configuration.</param>
        /// <param name="frameworkSetting">The framework instance.</param>
        internal static void VerifyComponents(string dailyLogFileName, RuntimeSetting runtimeSetting, FrameworkSetting frameworkSetting)
        {
            //LogEvent.Primeval("PRELOG-TRACE-FrameworkMaintenance-VerifyComponents");

            var todayDate = DateTime.Now.ToString("yyyyMMdd");
            var startTime = DateTime.Now.ToString("HHmmss");
            var startMs   = DateTime.Now.ToString("fffffff");

            VerifyStructure(frameworkSetting);
            string runningDailyLog = $"Framework: OK{Environment.NewLine}";

            LogMaintenance.ResetSystemLogs(frameworkSetting, runtimeSetting);
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
        private static void VerifyStructure(FrameworkSetting frwkConfig)
        {
            //LogEvent.Primeval("PRELOG-TRACE-FrameworkMaintenance-VerifyStructure");

            foreach (var path in Catalog.RequiredFrameworkFolders(frwkConfig))
            {
                try
                {
                    DuDirectory.ForceExist(path);
                }
                catch (Exception ex)
                {
                    LogEvent.Primeval("ERR1140-FrameworkValidationFailed", SysMsg.ERR1140(path, ex.Message)[1]);
                    //TODO - Should probably send an email notification.
                }
            }
        }
    }
}