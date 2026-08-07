// 260807_code
// 260806_documentation

using TingenWebService.Core.Framework;
using TingenWebService.Core.Trove;

namespace TingenWebService.Core.Logger
{
    /// <summary>Provides methods for maintaining system log files.</summary>
    internal static class LogMaintenance
    {
        /// <summary>Recreates the system log files.</summary>
        /// <param name="frameworkSetting">The framework settings.</param>
        /// <param name="runtimeSetting">The runtime settings.</param>
        internal static void RecreateSystemLogs(FrameworkSetting frameworkSetting, RuntimeSetting runtimeSetting)
        {
            LogEvent.SystemLog(frameworkSetting.SysLogRoot, "Runtime.details", Redprint.RuntimeDetails(runtimeSetting));
            LogEvent.SystemLog(frameworkSetting.SysLogRoot, "Framework.settings", Redprint.FrameworkSettings(frameworkSetting));
        }
    }
}