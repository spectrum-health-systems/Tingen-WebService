// 260725_code
// 260726_documentation

using System;
using TingenWebService.Properties;

namespace TingenWebService.Configuration
{
    /// <summary>Runtime configuration/settings logic.</summary>
    /// <remarks>
    /// The RuntimeConfiguration is the first configuration component that is loaded when the Tingen Web Service starts.
    /// It contains information that the web service needs to know before anything else can be loaded.
    /// </remarks>
    internal class RuntimeConfig
    {
        /// <summary>The current date (yyMMdd).</summary>
        public string CurrentDate { get; set; }

        /// <summary>The current time (HHmmss).</summary>
        public string CurrentTime { get; set; }

        /// <summary>Release and build information.</summary>
        /// <remarks>
        /// These two pieces of information are combined to form a single string that is used to identify the version of
        /// the Tingen Web Service that is running.
        /// </remarks>
        public string ReleaseBuild { get; set; }

        /// <summary>The Avatar system that the Tingen Web Service will interface with.</summary>
        public string AvatarSystem { get; set; }

        /// <summary>The root directory for Tingen Web Service data.</summary>
        public string DataRoot { get; set; }

        /// <summary>Load the runtime configuration.</summary>
        /// <returns>A <see cref="RuntimeConfig"/> instance with the current settings.</returns>
        internal static RuntimeConfig Load(string twsRelease)
        {
            /* For debugging prior to logging functionality being initialized.
             * Disable in production.
             */
            //LogEvent.Primeval("LoadRuntimeConfig");

            return new RuntimeConfig()
            {
                CurrentDate  = DateTime.Now.ToString("yyMMdd"),
                CurrentTime  = DateTime.Now.ToString("HHmmssfffffff"),
                ReleaseBuild = $"{twsRelease} (b{Settings.Default.TngnWsvcBuild})",
                AvatarSystem = Settings.Default.AvatarSystem,
                DataRoot     = Settings.Default.HostDataRoot,
            };
        }
    }
}