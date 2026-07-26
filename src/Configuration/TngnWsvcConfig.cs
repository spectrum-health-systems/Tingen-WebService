// 260724_code
// 260726_documentation

using System.IO;
using TingenWebService.Du;
using TingenWebService.Logger;
using TingenWebService.Trove;

namespace TingenWebService.Configuration
{
    /// <summary>Tingen Web Service configuration/settings logic.</summary>
    /// <remarks>
    /// The TngnWsvcConfiguration contains configuration and setting information that the Tingen Web Service needs to
    /// know for this specific instance of the web service. It is loaded after the RuntimeConfiguration and before any
    /// other configuration components.
    /// </remarks>
    internal class TngnWsvcConfig
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
        public int TraceLevelLimit { get; set; }

        /// <summary>Log delay.</summary>
        /// <remarks>
        /// In some cases the Tingen Web Service may create a large number of log entries in a short period of time. The
        /// LogDelay setting ensures that each event is logged with a unique timestamp.<br>
        /// </br>
        /// The LogDelay setting is the number of milliseconds to wait before logging the next event.
        /// </remarks>
        /// <value>Default: 10</value>
        public int LogDelay { get; set; }

        /// <summary>Email address the web services uses to send notifications.</summary>
        public string EmailAddress { get; set; }

        /// <summary>Email password the web services uses to send notifications.</summary>
        public string EmailPassword { get; set; }

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
        internal static TngnWsvcConfig Load(string configPath)
        {
            /* For debugging prior to logging functionality being initialized.
             * Disable in production.
             */
            //LogEvent.Primeval("LoadTingenWebServiceConfig");

            if (!File.Exists(configPath))
            {
                /* Use a primeval log to log the error, since the logging functionality is not initialized yet.
                 */
                LogEvent.Primeval("[CR4694]MissingConfig", ErrorMessage.Error4694());
                //TODO - Probably send an email (since it should not happen)

                CreateNew(configPath);
            }

            return DuJson.ImportFile<TngnWsvcConfig>(configPath);
        }

        /// <summary>Create a new Tingen Web Service configuration file.</summary>
        /// <param name="configPath">The path to the configuration file.</param>
        private static void CreateNew(string configPath)
        {
            /* For debugging prior to logging functionality being initialized.
             * Disable in production.
             */
            //LogEvent.Primeval("CreateTingenWebServiceConfig");

            TngnWsvcConfig tngnWsvcConfig = new TngnWsvcConfig()
            {
                Mode              = "enabled",
                TraceLevelLimit   = 0,
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