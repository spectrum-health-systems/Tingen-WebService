// 260804_code
// 260730_documentation

using System.Collections.Generic;
using System.IO;
using TingenWebService.Core.Session;
using TingenWebService.Core.Trove;
using TingenWebService.Module.OpenIncident;

namespace TingenWebService.Core.Logger
{
    /// <summary>Provides methods for maintaining system log files.</summary>
    internal static class LogMaintenance
    {
        /// <summary>Verifies the existence of system log files and resets them if necessary.</summary>
        /// <param name="twsSession">The current session.</param>
        internal static void VerifySystemLogs(Sess twsSession)
        {
            if (!File.Exists(Path.Combine(twsSession.FrameworkSetting.SysLogRoot, "Configuration.current"))) // TODO - better check
            {
                ResetSystemLogs(twsSession);
            }
        }

        /// <summary>Resets the system log files by removing existing logs and rebuilding them.</summary>
        /// <param name="sess">The current session.</param>
        internal static void ResetSystemLogs(Sess sess)
        {
            LogEvent.Trace(9, sess.TwsSetting.TraceLimit, sess.SessionFolder);

            var systemFileNames = Catalog.SystemLogFileNames();

            RemoveSystemLogs(sess.FrameworkSetting.SysLogRoot, systemFileNames, sess.TwsSetting.TraceLimit, sess.SessionFolder);
            BuildSystemLogs(sess.FrameworkSetting.SysLogRoot, systemFileNames, sess.RuntimeSetting, sess.FrameworkSetting, sess.TwsSetting);
        }

        /// <summary>Removes the specified system log files.</summary>
        /// <param name="systemLogRoot">The root folder of the system log files.</param>
        /// <param name="systemFileNames">The list of system log file names to remove.</param>
        /// <param name="traceLimit">The trace limit for logging.</param>
        /// <param name="sessionFolder">The session folder for logging.</param>
        internal static void RemoveSystemLogs(string systemLogRoot, List<string> systemFileNames, int traceLimit, string sessionFolder)
        {
            LogEvent.Trace(9, traceLimit, sessionFolder);

            foreach (var file in systemFileNames)
            {
                LogEvent.Primeval("PRELOG-TRACE_LogMaintenance-RemoveSystemLogs_Looking", $"Looking for: {file}");

                if (File.Exists(Path.Combine(systemLogRoot, $"{file}.current")))
                {
                    LogEvent.Primeval("PRELOG-TRACE_LogMaintenance-RemoveSystemLogs_Found", $"Found: {file}");

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
        internal static void BuildSystemLogs(string systemLogRoot, List<string> systemFileNames, RuntimeConfig rtConfig, Framework.FrameworkConfig twsFramework, TwsConfig twsConfig)
        {
            LogEvent.Trace(9, twsConfig.TraceLimit, twsFramework.SessionRoot);

            LogEvent.SystemLog(systemLogRoot, "Runtime.current", Redprint.RuntimeDetails(rtConfig));
            LogEvent.SystemLog(systemLogRoot, "Framework.current", Redprint.FrameworkDetails(twsFramework));
            LogEvent.SystemLog(systemLogRoot, "Configuration.current", Redprint.ConfigurationDetails(twsConfig));

            LogEvent.Trace(9, twsConfig.TraceLimit, twsFramework.SessionRoot);

            OpenIncidentConfig openIncidentConfig = OpenIncidentConfig.Load(Path.Combine(twsFramework.ConfigRoot, "OpenIncident.config"), twsConfig.TraceLimit, twsFramework.SessionRoot);
            LogEvent.SystemLog(systemLogRoot, "OpenIncident.current", Redprint.OpenIncidentConfig(openIncidentConfig));
        }
    }
}