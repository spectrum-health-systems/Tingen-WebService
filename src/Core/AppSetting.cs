// 260812_code
// 260812_documentation

using System.Collections.Generic;
using System.IO;
using TingenWebService.Du;

namespace TingenWebService.Core
{
    /// <summary>Tingen Web Service setting logic.</summary>
    /// <remarks>
    /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="Core"]/AboutAppSetting/*'/>
    /// </remarks>
    internal class AppSetting
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
        public int SessionLogDetailLevel { get; set; }

        /// <summary>Indicates whether session logs should be written in plain text format.</summary>
        public bool SessionLogTextFormat { get; set; }

        /// <summary>Indicates whether session logs should be written in Markdown format.</summary>
        public bool SessionLogMarkdownFormat { get; set; }

        /// <summary>Session timeout is too damn high!</summary>
        public string SessionTimeout { get; set; }

        /// <summary>Indicates whether error logs should be written in plain text format.</summary>
        public bool ErrorLogTextFormat { get; set; }

        /// <summary>Indicates whether error logs should be written in Markdown format.</summary>
        public bool ErrorLogMarkdownFormat { get; set; }

        /// <summary>Indicates whether error logs should be written in HTML format.</summary>
        public bool ErrorLogHtmlFormat { get; set; }

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
        internal static AppSetting Load(string configPath)
        {
            //LogEvent.Primeval("PRELOG-TRACE-AppSetting-Load");

            if (!File.Exists(configPath))
            {
                //TODO - Probably send an email (since it should not happen)

                Build(configPath);
            }

            return DuJson.ImportFile<AppSetting>(configPath);
        }

        /// <summary>Create a new Tingen Web Service configuration file.</summary>
        /// <param name="configPath">The path to the configuration file.</param>
        /// <remarks>Add to redprint.</remarks>
        private static void Build(string configPath)
        {
            //LogEvent.Primeval("PRELOG-TRACE-AppSetting-Build");

            AppSetting tngnWsvcConfig = new AppSetting()
            {
                Mode                     = "enabled",
                TraceLimit               = 0,
                LogDelay                 = 0,
                SessionLogDetailLevel    = 0,
                SessionLogTextFormat     = true,
                SessionLogMarkdownFormat = true,
                SessionTimeout           = "05",
                ErrorLogTextFormat       = true,
                ErrorLogMarkdownFormat   = false,
                ErrorLogHtmlFormat       = false,
                FromEmailAddress         = "unassigned",
                FromEmailPassword        = "unassigned",
                ToEmailAddress           = new List<string>() { "unassigned" },
                EmailFormat              = "html",
                NtstWsvcUserName         = "unassigned",
                NtstWsvcPassword         = "unassigned"
            };

            DuJson.ExportFile(tngnWsvcConfig, configPath, true);
        }
    }
}