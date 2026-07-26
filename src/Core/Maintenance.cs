// 260725_code
// 260725_documentation

using System;
using System.IO;
using TingenWebService.Configuration;
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
            var dailyLogPath = Path.Combine(rtConfig.DataRoot, "WebService", rtConfig.AvatarSystem, "Log", $"{DateTime.Now:yyMMdd}");

            if (!Directory.Exists(dailyLogPath))
            {
                Directory.CreateDirectory(dailyLogPath);
            }

            var dailyStartFile = Path.Combine(dailyLogPath, $"{DateTime.Now:HHmmss}.start");

            if (!File.Exists(dailyStartFile))
            {
                VerifyComponents(dailyStartFile, rtConfig, twsFramework);
            }
        }

        /// <summary>Verify the components for the session.</summary>
        /// <param name="startLogFile">The path to the start log file.</param>
        /// <param name="rtConfig">The runtime configuration.</param>
        /// <param name="twsFramework">The framework instance.</param>
        private static void VerifyComponents(string dailyStartFile, RuntimeConfig rtConfig, Framework twsFramework)
        {
            if (!File.Exists(dailyStartFile))
            {
                Framework.Verify(twsFramework);
                Du.DuFile.DeadDrop(dailyStartFile, Epistle.HistoryStart(rtConfig.WsvcRelease, rtConfig.WsvcBuild));
                Du.DuFile.DeadDropAppend(dailyStartFile, Epistle.FrameworkVerified());
                Framework.ExportBlueprints(twsFramework.BlueprintRoot);
                Du.DuFile.DeadDropAppend(dailyStartFile, Epistle.BlueprintsExported());
            }
        }
    }
}