// 260723_code
// 260724_documentation

using System;
using TingenWebService.Properties;

namespace TingenWebService.Configuration
{
    /// <summary>Runtime configuration/settings logic.</summary>
    internal class RuntimeConfig
    {
        /// <summary>Current date.</summary>
        public string SessionDate { get; set; }

        /// <summary>Current time.</summary>
        public string SessionTime { get; set; }

        /// <summary>Current web service build.</summary>
        public string WsvcBuild { get; set; }

        /// <summary>The Avatar system that the Tingen Web Service will interface with.</summary>
        public string AvatarSystem { get; set; }

        /// <summary>The root directory for data.</summary>
        public string DataRoot { get; set; }

        /// <summary>The root directory for the web server.</summary>
        public string WwwRoot { get; set; } // TODO - May not need.

        /// <summary>Loads the runtime configuration.</summary>
        /// <returns>A <see cref="RuntimeConfig"/> instance with the current settings.</returns>
        internal static RuntimeConfig Load()
        {
            return new RuntimeConfig()
            {
                SessionDate   = DateTime.Now.ToString("yyMMdd"),
                SessionTime   = DateTime.Now.ToString("HHmmss"),
                WsvcBuild     = Settings.Default.TngnWsvcBuild,
                AvatarSystem  = Settings.Default.AvatarSystem,
                DataRoot      = Settings.Default.HostDataRoot,
                WwwRoot       = Settings.Default.HostWwwRoot
            };
        }
    }
}