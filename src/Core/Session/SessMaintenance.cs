// 260807_code
// 260806_documentation

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
        /// <param name="twsConfig">The TWS configuration.</param>
        internal static void InitializeNewSession(RuntimeSetting runtimeSetting, Framework.FrameworkSetting frameworkSetting)
        {
            //LogEvent.Primeval("PRELOG-TRACE-SessMaint-InitializeNewSession");

            //var dailyDate = DateTime.Now.ToString("yyMMdd");

            //if (!File.Exists(Path.Combine(twsFramework.SysLogRoot, $"{dailyDate}.daily")))
            //{
            //    var dailyFilePath = Path.Combine(twsFramework.SysLogRoot, $"{dailyDate}.daily");

            //    Framework.FrameworkMaintenance.VerifyComponents($"{dailyDate}.daily", rtConfig, twsFramework);
            //}
        }
    }
}