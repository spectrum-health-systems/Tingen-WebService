// 260725_code
// 260725_documentation

using System.IO;
using TingenWebService.Du;

namespace TingenWebService.Logger
{
    public class LoggerConfig
    {
        public int TraceLogLimit { get; set; }

        public int LogPause { get; set; }

        internal static LoggerConfig Load(string configPath)
        {
            if (!File.Exists(configPath))
            {
                CreateNew(configPath);
            }

            return DuJson.ImportFile<LoggerConfig>(configPath);
        }

        private static void CreateNew(string configPath)
        {
            LoggerConfig loggerConfig = new LoggerConfig()
            {
                TraceLogLimit = 0,
                LogPause      = 0
            };

            DuJson.ExportFile(loggerConfig, configPath, true);
        }
    }


}