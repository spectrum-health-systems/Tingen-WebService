// 260729_code
// 260729_documentation

using System;
using System.IO;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Trove;
using TingenWebService.Du;

namespace TingenWebService.Core.Framework
{
    internal class FrwkMaint
    {
        /// <summary>Verify the Tingen Web Service framework.</summary>
        /// <param name="twsFramework">The framework instance containing the paths to verify.</param>
        /// <remarks>
        /// The reason why we hand this off is because while currently the configuration contains root paths, it may
        /// contain other data in the future.
        /// </remarks>
        internal static void Verify(FrwkConfig frwkConfig)
        {
            /* For debugging prior to logging functionality being initialized.
             * Disable in production.
             */
            //Logger.LogEvent.Primeval("VerifyFrameworkComponents");

            foreach (var path in Catalog.RequiredFrameworkFolders(frwkConfig))
            {
                try
                {
                    DuDirectory.EnsureDirectoryExists(path);
                }
                catch (Exception ex)
                {
                    /* Use a primeval log to log the error, since the logging functionality is not initialized yet.
                     */
                    Logger.LogEvent.Primeval("ERR1140-FrameworkVerfication", SysMsg.ERR1140(path, ex.Message)[1]);
                    //TODO - Should probably send an email notification.
                }
            }
        }

        /// <summary>Exports all blueprint templates to the specified host directory.</summary>
        /// <param name="blueprintRoot">The root directory where the blueprints will be exported.</param>
        internal static void ExportBlueprints(string blueprintRoot)
        {
            /* For debugging prior to logging functionality being initialized.
             * Disable in production.
             */
            //Logger.LogEvent.Primeval("ExportBlueprints");

            // TODO - So ugly!
            /* This code is terrible and needs to be refactored.
             */

            foreach (var blueprintFileName in Catalog.BlueprintFileNames())
            {
                var blueprintPath = Path.Combine(blueprintRoot, $"{blueprintFileName}.bp");

                if (blueprintFileName == "ErrorLogTxt" || !File.Exists(Path.Combine(blueprintRoot, $"{blueprintFileName}.bp")))
                {
                    DuFile.DeadDrop(blueprintPath, BluePrint.ErrorLogTxtBP());
                }

                if (blueprintFileName == "SessLogTxt" || !File.Exists(Path.Combine(blueprintRoot, $"{blueprintFileName}.bp")))
                {
                    DuFile.DeadDrop(blueprintPath, BluePrint.SessLogTxtBP());
                }

                if (blueprintFileName == "SessLogMd" || !File.Exists(Path.Combine(blueprintRoot, $"{blueprintFileName}.bp")))
                {
                    DuFile.DeadDrop(blueprintPath, BluePrint.SessLogMdBP());
                }

                if (blueprintFileName == "SessLogHtml" || !File.Exists(Path.Combine(blueprintRoot, $"{blueprintFileName}.bp")))
                {
                    DuFile.DeadDrop(blueprintPath, BluePrint.SessLogHtmlBP());
                }
            }
        }

        /// <summary>Verify the components for the session.</summary>
        /// <param name="sysLogRoot">The root directory for system logs.</param>
        /// <param name="dailyStartFileName">The name of the start log file.</param>
        /// <param name="rtConfig">The runtime configuration.</param>
        /// <param name="twsFramework">The framework instance.</param>
        internal static void VerifyComponents(string systemLogFileName, RuntimeConfig rtConfig, FrwkConfig twsFramework)
        {
            /* For debugging prior to logging functionality being initialized.
             * Disable in production.
             */
            //Logger.LogEvent.Primeval("VerifyComponents");

            var verificationLog = string.Empty;

            var verificationStartTime = DateTime.Now.ToString("HHmmss");
            var verificationStartMilliseconds = DateTime.Now.ToString("fffffff");

            verificationLog += $"[Start] {verificationStartTime}:{verificationStartMilliseconds}{Environment.NewLine}";
            verificationLog += RedPrint.DailyStart(rtConfig.ReleaseBuild);

            Framework.FrwkMaint.Verify(twsFramework);
            verificationLog += RedPrint.FrameworkVerified();

            Framework.FrwkMaint.ExportBlueprints(twsFramework.BlueprintRoot);
            verificationLog += RedPrint.BlueprintsExported();

            var verificationEndTime = DateTime.Now.ToString("HHmmss");
            var verificationEndMilliseconds = DateTime.Now.ToString("fffffff");

            // TODO - This is in a few places, and might be better in a common area.
            var verificationDurationTime = (DateTime.ParseExact(verificationEndTime, "HHmmss", null) - DateTime.ParseExact(verificationStartTime, "HHmmss", null)).ToString(@"hh\:mm\:ss");
            var verificationDurationMilliseconds = (DateTime.ParseExact(verificationEndMilliseconds, "fffffff", null) - DateTime.ParseExact(verificationStartMilliseconds, "fffffff", null)).ToString("fffffff");

            verificationLog += $"[End] {verificationEndTime}:{verificationEndMilliseconds}{Environment.NewLine}[Duration] {verificationDurationTime}:{verificationDurationMilliseconds}";

            LogEvent.SystemLog(twsFramework.SysLogRoot, systemLogFileName, verificationLog);
        }
    }
}