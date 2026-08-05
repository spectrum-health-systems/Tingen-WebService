// 260805_code
// 260805_documentation

using System.Collections.Generic;
using System.IO;
using TingenWebService.Du;

namespace TingenWebService.Core
{
    /// <summary>Tingen Web Service configuration/settings logic.</summary>
    /// <remarks>
    /// The TngnWsvcConfiguration contains configuration and setting information that the Tingen Web Service needs to
    /// know for this specific instance of the web service. It is loaded after the RuntimeConfiguration and before any
    /// other configuration components.
    /// </remarks>
    internal class TwsConfig
    {
        /// <summary>Tingen Web Service mode</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item><c>enabled</c> - All web service functionality is enabled.</item>
        /// <item><c>disabled</c> - All web service functionality is disabled.</item>
        /// <item><c>passthrough</c> - The web service operates in passthrough mode.</item>
        /// </list>
        /// </remarks>
        public string Mode { get; set; }

        /// <summary>Trace level limit.</summary>
        /// <remarks>
        /// LINKTO Logging/TraceLevelLimit documentation
        /// </remarks>
        public int TraceLimit { get; set; }

        /// <summary>Log delay.</summary>
        /// <remarks>
        /// In some cases the Tingen Web Service may create a large number of log entries in a short period of time. The
        /// LogDelay setting ensures that each event is logged with a unique timestamp.<br>
        /// </br>
        /// The LogDelay setting is the number of milliseconds to wait before logging the next event.
        /// </remarks>
        /// <value>Default: 10</value>
        public int LogDelay { get; set; }

        /// <summary>Session log detail level.</summary>
        public int SessLogDetailLevel { get; set; }

        /// <summary>Indicates whether session logs should be written in plain text format.</summary>
        public bool SessLogTxt { get; set; }

        /// <summary>Indicates whether session logs should be written in Markdown format.</summary>
        public bool SessLogMd { get; set; }

        /// <summary>Indicates whether session logs should be written in HTML format.</summary>
        public bool SessLogHtml { get; set; }

        /// <summary>Session timeout is too damn high!</summary>
        public string SessTimeout { get; set; }

        /// <summary>Indicates whether error logs should be written in plain text format.</summary>
        public bool ErrorLogTxt { get; set; }

        /// <summary>Indicates whether error logs should be written in Markdown format.</summary>
        public bool ErrorLogMd { get; set; }

        /// <summary>Indicates whether error logs should be written in HTML format.</summary>
        public bool ErrorLogHtml { get; set; }

        /// <summary>Email address the web services uses to send notifications.</summary>
        public string FromEmailAddress { get; set; }

        /// <summary>Email password the web services uses to send notifications.</summary>
        public string FromEmailPassword { get; set; }

        /// <summary>Email addresses the web services uses to send notifications.</summary>
        public List<string> ToEmailAddress { get; set; }

        /// <summary>The format of the email messages sent by the web services.</summary>
        public string EmailFormat { get; set; }

        /// <summary>The username used to authenticate with NTST web services.</summary>
        public string NtstWsvcUserName { get; set; }

        /// <summary>The password used to authenticate with NTST web services.</summary>
        public string NtstWsvcPassword { get; set; }

        /// <summary>Load the Tingen Web Service configuration from a file.</summary>
        /// <param name="configPath">The path to the configuration file.</param>
        /// <remarks>
        /// If the configuration file does not exist, a new configuration file is created with default values.
        /// </remarks>
        /// <returns>The loaded Tingen Web Service configuration.</returns>
        internal static TwsConfig Load(string configPath)
        {
            //LogEvent.Primeval("PRELOG-TRACE-TwsConfig-Load");

            if (!File.Exists(configPath))
            {
                //TODO - Probably send an email (since it should not happen)

                Build(configPath);
            }

            return DuJson.ImportFile<TwsConfig>(configPath);
        }

        /// <summary>Create a new Tingen Web Service configuration file.</summary>
        /// <param name="configPath">The path to the configuration file.</param>
        private static void Build(string configPath)
        {
            //LogEvent.Primeval("PRELOG-TRACE-TwsConfig-Build");

            TwsConfig tngnWsvcConfig = new TwsConfig()
            {
                Mode               = "enabled",
                TraceLimit         = 0,
                LogDelay           = 0,
                SessLogDetailLevel = 0,
                SessLogTxt         = true,
                SessLogMd          = false,
                SessLogHtml        = false,
                SessTimeout        = "05",
                ErrorLogTxt        = true,
                ErrorLogMd         = false,
                ErrorLogHtml       = false,
                FromEmailAddress   = "unassigned",
                FromEmailPassword  = "unassigned",
                ToEmailAddress     = new List<string>() { "unassigned" },
                EmailFormat        = "html",
                NtstWsvcUserName   = "unassigned",
                NtstWsvcPassword   = "unassigned"
            };

            DuJson.ExportFile(tngnWsvcConfig, configPath, true);
        }
    }
}