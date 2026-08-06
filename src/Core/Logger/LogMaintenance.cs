// 260805_code
// 260805_documentation
using System.Collections.Generic;
using System.IO;
using TingenWebService.Core.Framework;
using TingenWebService.Core.Trove;

namespace TingenWebService.Core.Logger
{
    /// <summary>Provides methods for maintaining system log files.</summary>
    internal static class LogMaintenance
    {
        /// <summary>Verifies the existence of system log files and resets them if necessary.</summary>
        /// <param name="frameworkSetting">The framework configuration.</param>
        /// <param name="runtimeSetting">The runtime configuration.</param>
        /// <param name="twsSetting">The TWS configuration.</param>
        internal static void VerifySystemLogs(RuntimeSetting runtimeSetting, FrameworkSetting frameworkSetting, AppSetting twsConfig)
        {
            if (!File.Exists(Path.Combine(frameworkSetting.SysLogRoot, "Configuration.current"))) // TODO - better check
            {
                ResetSystemLogs(frameworkSetting, runtimeSetting);
            }
        }

        /// <summary>Resets the system log files by removing existing logs and rebuilding them.</summary>
        /// <param name="frameworkSetting">The framework configuration.</param>
        /// <param name="runtimeSetting">The runtime configuration.</param>
        /// <param name="twsSetting">The TWS configuration.</param>
        internal static void ResetSystemLogs(FrameworkSetting frameworkSetting, RuntimeSetting runtimeSetting)
        {
            //LogEvent.Primeval("PRELOG-TRACE-LogMaintenance-ResetSystemLogs");

            var systemFileNames = Catalog.SystemLogFileNames();

            RemoveSystemLogs(frameworkSetting.SysLogRoot, systemFileNames, frameworkSetting.SessionRoot);
            BuildSystemLogs(frameworkSetting.SysLogRoot, systemFileNames, runtimeSetting, frameworkSetting);
        }

        /// <summary>Removes the specified system log files.</summary>
        /// <param name="systemLogRoot">The root folder of the system log files.</param>
        /// <param name="systemFileNames">The list of system log file names to remove.</param>
        /// <param name="traceLimit">The trace limit for logging.</param>
        /// <param name="sessionFolder">The session folder for logging.</param>
        internal static void RemoveSystemLogs(string systemLogRoot, List<string> systemFileNames, string sessionFolder)
        {
            //LogEvent.Primeval("PRELOG-TRACE-LogMaintenance-RemoveSystemLogs");

            foreach (var file in systemFileNames)
            {
                //LogEvent.Primeval("PRELOG-TRACE-LogMaintenance-RemoveSystemLogs_Looking", $"Looking for: {file}");

                if (File.Exists(Path.Combine(systemLogRoot, $"{file}.current")))
                {
                    //LogEvent.Primeval("PRELOG-TRACE-LogMaintenance-RemoveSystemLogs_Found", $"Found: {file}");

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
        internal static void BuildSystemLogs(string systemLogRoot, List<string> systemFileNames, RuntimeSetting runtimeConfig, FrameworkSetting twsFramework)
        {
            //LogEvent.Primeval("PRELOG-TRACE-LogMaintenance-BuildSystemLogs");

            LogEvent.SystemLog(systemLogRoot, "Runtime.current", Redprint.RuntimeDetails(runtimeConfig));
            LogEvent.SystemLog(systemLogRoot, "Framework.current", Redprint.FrameworkDetails(twsFramework));

            //LogEvent.SystemLog(systemLogRoot, "Configuration.current", Redprint.ConfigurationDetails(twsFramework));

            //OpenIncidentConfig openIncidentConfig = OpenIncidentConfig.Load(Path.Combine(twsFramework.ConfigRoot, "OpenIncident.config"), twsConfig.TraceLimit, twsFramework.SessionRoot);
            //LogEvent.SystemLog(systemLogRoot, "OpenIncident.current", Redprint.OpenIncidentConfig(openIncidentConfig));
        }
    }
}