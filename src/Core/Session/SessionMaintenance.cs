// 260729_code
// 260729_documentation

using System;
using System.IO;

namespace TingenWebService.Core.Session
{
    internal class SessionMaintenance
    {
        /// <summary>Session maintenance.</summary>
        /// <remarks>
        /// This maintenance is done for every session.
        /// </remarks>
        /// <param name="rtConfig">The runtime configuration.</param>
        /// <param name="twsFramework">The framework instance.</param>
        internal static void InitializeNewSession(RuntimeConfiguration rtConfig, Framework.FrameworkConfiguration twsFramework)
        {
            /* For debugging prior to logging functionality being initialized.
             * Disable in production.
             */
            //Logger.LogEvent.Primeval("SessionMaintenance");

            var dailyDate = DateTime.Now.ToString("yyMMdd");

            if (!File.Exists(Path.Combine(twsFramework.SysLogRoot, $"{dailyDate}.daily")))
            {
                Logger.LoggerMaintenance.ResetSyslogs(twsFramework.SysLogRoot);
                Framework.FrameworkMaintenance.VerifyComponents($"{dailyDate}.daily", rtConfig, twsFramework);
            }
        }
    }
}