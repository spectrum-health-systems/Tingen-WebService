// 260729_code
// 260729_documentation

using System;
using System.IO;

namespace TingenWebService.Core.Session
{
    /// <summary>Session maintenance logic.</summary>
    internal class SessMaint
    {
        /// <summary>Session maintenance.</summary>
        /// <remarks>
        /// This maintenance is done for every session.
        /// </remarks>
        /// <param name="rtConfig">The runtime configuration.</param>
        /// <param name="twsFramework">The framework instance.</param>
        internal static void InitializeNewSession(RuntimeConfig rtConfig, Framework.FrwkConfig twsFramework)
        {
            /* For debugging prior to logging functionality being initialized.
             * Disable in production.
             */
            //Logger.LogEvent.Primeval($"{DateTime.Now:yyMMdd-HHmmss-fffffff}-DEBUG-SessMaint.InitializeNewSession");

            var dailyDate = DateTime.Now.ToString("yyMMdd");

            if (!File.Exists(Path.Combine(twsFramework.SysLogRoot, $"{dailyDate}.daily")))
            {
                Framework.FrwkMaint.VerifyComponents($"{dailyDate}.daily", rtConfig, twsFramework);
            }
        }
    }
}