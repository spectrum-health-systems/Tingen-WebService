// 260724_code
// 260724_documentation

using System.IO;
using TingenWebService.Du;

namespace TingenWebService.Configuration
{
    internal class TngnWsvcConfig
    {
        /// <summary>Tingen Web Service mode</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>enabled</item>
        /// <item>disabled</item>
        /// <item>passthrough</item>
        /// </list>
        /// </remarks>
        public string Mode { get; set; }

        /// <summary>Trace log level.</summary>
        public int TraceLogLevel { get; set; }

        /// <summary>Log delay.</summary>
        public int LogDelay { get; set; }

        /// <summary>Email address the web services uses.</summary>
        public string EmailAddress { get; set; }

        /// <summary>Email password the web services uses.</summary>
        public string EmailPassword { get; set; }

        /// <summary>The NTST web service username.</summary>
        public string NtstWsvcUserName { get; set; }

        /// <summary>The NTST web service password.</summary>
        public string NtstWsvcPassword { get; set; }

        /// <summary>Load the Tingen Web Service configuration from a file.</summary>
        /// <param name="configPath">The path to the configuration file.</param>
        /// <returns>The loaded Tingen Web Service configuration.</returns>
        internal static TngnWsvcConfig Load(string configPath)
        {
            if (!File.Exists(configPath))
            {
                CreateNew(configPath);
            }

            return DuJson.ImportFile<TngnWsvcConfig>(configPath);
        }

        /// <summary>Create a new Tingen Web Service configuration file.</summary>
        /// <param name="configPath">The path to the configuration file.</param>
        private static void CreateNew(string configPath)
        {
            TngnWsvcConfig tngnWsvcConfig = new TngnWsvcConfig()
            {
                Mode              = "enabled",
                TraceLogLevel     = 0,
                LogDelay          = 0,
                EmailAddress      = "unassigned",
                EmailPassword     = "unassigned",
                NtstWsvcUserName  = "unassigned",
                NtstWsvcPassword  = "unassigned"
            };

            DuJson.ExportFile(tngnWsvcConfig, configPath, true);
        }
    }
}