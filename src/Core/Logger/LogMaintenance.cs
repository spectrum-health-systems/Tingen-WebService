// 260729_code
// 260730_documentation

using System.Collections.Generic;
using System.IO;
using TingenWebService.Core.Trove;

namespace TingenWebService.Core.Logger
{
    /// <summary>Provides methods for maintaining system log files.</summary>
    internal static class LogMaintenance
    {
        /// <summary>Verifies the existence of system log files and resets them if necessary.</summary>
        /// <param name="twsSession"></param>
        internal static void VerifySystemLogs(Sess twsSession)
        {
            if (!File.Exists(Path.Combine(twsSession.FrameworkSetting.SysLogRoot, "Configuration.current"))) // TODO - better check
            {
                ResetSystemLogs(twsSession);
            }
        }

        /// <summary>Resets the system log files by removing existing logs and rebuilding them.</summary>
        /// <param name="twsSession">The current session.</param>
        internal static void ResetSystemLogs(Sess twsSession)
        {
            var systemFileNames = Catalog.SystemLogFileNames();

            RemoveSystemLogs(twsSession.FrameworkSetting.SysLogRoot, systemFileNames);
            BuildSystemLogs(twsSession.FrameworkSetting.SysLogRoot, systemFileNames, twsSession.RuntimeSetting, twsSession.FrameworkSetting, twsSession.TwsSetting);
        }

        /// <summary>Removes the specified system log files.</summary>
        /// <param name="systemLogRoot">The root folder of the system log files.</param>
        /// <param name="systemFileNames">The list of system log file names to remove.</param>
        internal static void RemoveSystemLogs(string systemLogRoot, List<string> systemFileNames)
        {
            foreach (var file in systemFileNames)
            {
                if (File.Exists(Path.Combine(systemLogRoot, $"{file}.current")))
                {
                    File.Delete(Path.Combine(systemLogRoot, $"{file}.current"));
                }
            }
        }

        /// <summary>Builds the specified system log files.</summary>
        /// <param name="systemLogRoot">The root folder of the system log files.</param>
        /// <param name="systemFileNames">The list of system log file names to build.</param>
        /// <param name="rtConfig">The runtime configuration.</param>
        /// <param name="twsFramework">The framework configuration.</param>
        /// <param name="twsConfig">The TWS configuration.</param>
        internal static void BuildSystemLogs(string systemLogRoot, List<string> systemFileNames, RuntimeConfig rtConfig, Core.Framework.FrwkConfig twsFramework, TwsConfig twsConfig)
        {
            LogEvent.SystemLog(systemLogRoot, "Runtime.current", RedPrint.RuntimeDetails(rtConfig));
            LogEvent.SystemLog(systemLogRoot, "Framework.current", RedPrint.FrameworkDetails(twsFramework));
            LogEvent.SystemLog(systemLogRoot, "Configuration.current", RedPrint.ConfigurationDetails(twsConfig));
        }
    }
}