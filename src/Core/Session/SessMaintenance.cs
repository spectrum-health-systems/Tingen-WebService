// 260804_code
// 260729_documentation

using System;
using System.IO;
using TingenWebService.Core.Logger;

namespace TingenWebService.Core.Session
{
    /// <summary>Session maintenance logic.</summary>
    internal class SessMaintenance
    {
        /// <summary>Session maintenance.</summary>
        /// <remarks>
        /// This maintenance is done for every session.
        /// </remarks>
        /// <param name="rtConfig">The runtime configuration.</param>
        /// <param name="twsFramework">The framework instance.</param>
        internal static void InitializeNewSession(RuntimeConfig rtConfig, Framework.FrameworkConfig twsFramework)
        {
            LogEvent.Primeval("PRELOG-TRACE_SessMaint-InitializeNewSession");

            var dailyDate = DateTime.Now.ToString("yyMMdd");

            if (!File.Exists(Path.Combine(twsFramework.SysLogRoot, $"{dailyDate}.daily")))
            {
                Framework.FrameworkMaintenance.VerifyComponents($"{dailyDate}.daily", rtConfig, twsFramework);
            }
        }
    }
}