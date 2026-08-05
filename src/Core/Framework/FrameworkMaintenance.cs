// 260804_code
// 260730_documentation

using System;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Trove;
using TingenWebService.Du;

namespace TingenWebService.Core.Framework
{
    /// <summary>Provides methods for maintaining the Tingen Web Service framework.</summary>
    internal static class FrameworkMaintenance
    {
        /// <summary>Verify the Tingen Web Service framework.</summary>
        /// <param name="frwkConfig">The framework instance containing the paths to verify.</param>
        /// <remarks>
        /// The reason why we hand this off is because while currently the configuration contains root paths, it may
        /// contain other data in the future.
        /// </remarks>
        internal static void Verify(FrameworkConfig frwkConfig)
        {
            //LogEvent.Primeval("PRELOG-TRACE-FrwkMaint-Verify");

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
        /// <param name="rtConfig">The runtime configuration.</param>
        /// <param name="twsFramework">The framework instance.</param>
        internal static void VerifyComponents(string systemLogFileName, RuntimeConfig rtConfig, FrameworkConfig twsFramework)
        {
            //LogEvent.Primeval("PRELOG-TRACE-FrwkMaint-VerifyComponents");

            // TODO - Clean this up

            var dailyLog = string.Empty;

            var verificationStartTime = DateTime.Now.ToString("HHmmss");
            var verificationStartMilliseconds = DateTime.Now.ToString("fffffff");

            dailyLog += $"[Start] {verificationStartTime}:{verificationStartMilliseconds}{Environment.NewLine}";
            dailyLog += SysMsg.DailyStart(rtConfig.ReleaseBuild);

            Verify(twsFramework);
            dailyLog += SysMsg.FrameworkVerified();

            FrameworkExport.ExportBlueprints(twsFramework.BlueprintRoot);
            dailyLog += Redprint.BlueprintsExported();

            FrameworkExport.ExportTranslations(twsFramework.TranslationTableRoot);
            dailyLog += Catalog.TranslationFilesBuilt();

            var verificationEndTime = DateTime.Now.ToString("HHmmss");
            var verificationEndMilliseconds = DateTime.Now.ToString("fffffff");

            // TODO - This is in a few places, and might be better in a common area.
            var verificationDurationTime = (DateTime.ParseExact(verificationEndTime, "HHmmss", null) - DateTime.ParseExact(verificationStartTime, "HHmmss", null)).ToString(@"hh\:mm\:ss");
            var verificationDurationMilliseconds = (DateTime.ParseExact(verificationEndMilliseconds, "fffffff", null) - DateTime.ParseExact(verificationStartMilliseconds, "fffffff", null)).ToString("fffffff");

            dailyLog += $"[End] {verificationEndTime}:{verificationEndMilliseconds}{Environment.NewLine}[Duration] {verificationDurationTime}:{verificationDurationMilliseconds}";

            LogEvent.SystemLog(twsFramework.SysLogRoot, systemLogFileName, dailyLog);
        }
    }
}