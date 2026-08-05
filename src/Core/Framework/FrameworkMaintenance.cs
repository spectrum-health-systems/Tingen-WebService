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
        internal static void DailyVerify(RuntimeConfig runtimeConfig, FrameworkConfig frameworkConfig)
        {
            var dailyLogFileName = $"{DateTime.Now:yyMMdd}.daily";

            if (!File.Exists(Path.Combine(frameworkConfig.SysLogRoot, dailyLogFileName)))
            {
                VerifyComponents(dailyLogFileName, runtimeConfig, frameworkConfig);
            }
        }

        /// <summary>Verify the Tingen Web Service framework.</summary>
        /// <param name="frwkConfig">The framework instance containing the paths to verify.</param>
        /// <remarks>
        /// The reason why we hand this off is because while currently the configuration contains root paths, it may
        /// contain other data in the future.
        /// </remarks>
        internal static void VerifyStructure(FrameworkConfig frwkConfig)
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

        /// <summary>Verify the components for the session.</summary>
        /// <param name="systemLogFileName">The name of the start log file.</param>
        /// <param name="runtimeConfig">The runtime configuration.</param>
        /// <param name="frameworkConfig">The framework instance.</param>
        internal static void VerifyComponents(string dailyLogFileName, RuntimeConfig runtimeConfig, FrameworkConfig frameworkConfig)
        {
            //LogEvent.Primeval("PRELOG-TRACE-FrameworkMaintenance-VerifyComponents");

            var verifyStart   = DateTime.Now.ToString("HH:mm:ss");
            var verifyStartMs = DateTime.Now.ToString("fffffff");

            string runningDailyLog = SysMsg.DailyStart(runtimeConfig.ReleaseBuild);

            VerifyStructure(frameworkConfig);
            runningDailyLog += SysMsg.FrameworkVerified();

            LogMaintenance.ResetSystemLogs(frameworkConfig, runtimeConfig);
            runningDailyLog += SysMsg.SystemLogsReset();

            Blueprint.ExportBlueprints(frameworkConfig.BlueprintRoot);
            runningDailyLog += Redprint.BlueprintsExported();

            Translation.ExportTranslations(frameworkConfig.TranslationRoot);
            runningDailyLog += Catalog.TranslationFilesExported();

            var verifyEnd = DateTime.Now.ToString("HHmmss");
            var verifyEndMs = DateTime.Now.ToString("fffffff");

            // TODO - This is in a few places, and might be better in a common area.
            var verificationDurationTime = (DateTime.ParseExact(verifyEnd, "HHmmss", null) - DateTime.ParseExact(verifyStart, "HHmmss", null)).ToString(@"hh\:mm\:ss");
            var verificationDurationMilliseconds = (DateTime.ParseExact(verifyEndMs, "fffffff", null) - DateTime.ParseExact(verifyStartMs, "fffffff", null)).ToString("fffffff");

            runningDailyLog += $"[End] {verifyEnd}:{verifyEndMs}{Environment.NewLine}[Duration] {verificationDurationTime}:{verificationDurationMilliseconds}";

            LogEvent.SystemLog(frameworkConfig.SysLogRoot, dailyLogFileName, runningDailyLog);



        }
    }
}