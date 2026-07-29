// 260729_code
// 260729_documentation

using System.Collections.Generic;
using System.IO;
using TingenWebService.Core.Trove;

namespace TingenWebService.Core.Logger
{
    internal class LogMaintenance
    {
        internal static void VerifySystemLogs(Sess twsSession)
        {
            if (!File.Exists(Path.Combine(twsSession.FrameworkSetting.SysLogRoot, "Configuration.current"))) // TODO - better check
            {
                ResetSystemLogs(twsSession);
            }
        }

        internal static void ResetSystemLogs(Sess twsSession)
        {
            var systemFileNames = Catalog.SystemLogFileNames();

            RemoveSystemLogs(twsSession.FrameworkSetting.SysLogRoot, systemFileNames);
            BuildSystemLogs(twsSession.FrameworkSetting.SysLogRoot, systemFileNames, twsSession.RuntimeSetting, twsSession.FrameworkSetting, twsSession.TwsSetting);


        }

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

        internal static void BuildSystemLogs(string systemLogRoot, List<string> systemFileNames, RuntimeConfig rtConfig, Core.Framework.FrwkConfig twsFramework, TwsConfig twsConfig)
        {
            LogEvent.SystemLog(systemLogRoot, "Runtime.current", RedPrint.RuntimeDetails(rtConfig));
            LogEvent.SystemLog(systemLogRoot, "Framework.current", RedPrint.FrameworkDetails(twsFramework));
            LogEvent.SystemLog(systemLogRoot, "Configuration.current", RedPrint.ConfigurationDetails(twsConfig));
        }
    }
}