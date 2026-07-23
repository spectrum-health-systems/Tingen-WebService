// 260723_code
// 260723_documentation

using System;
using TingenWebService.Properties;

namespace TingenWebService.Configuration
{
    /// <summary>Runtime configuration/settings logic.</summary>
    internal class RuntimeConfig
    {
        public string SessionDate { get; set; }
        public string SessionTime { get; set; }
        public string WsvcBuild { get; set; }
        public string AvatarSystem { get; set; }
        public string DataRoot { get; set; }
        public string WwwRoot { get; set; }

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