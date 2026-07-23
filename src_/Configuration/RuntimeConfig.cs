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
        public string TngnWsvcBuild { get; set; }
        public string AvatarSystem { get; set; }
        public string HostDataRoot { get; set; }
        public string HostWwwRoot { get; set; }

        internal static RuntimeConfig Load()
        {
            return new RuntimeConfig()
            {
                SessionDate   = DateTime.Now.ToString("yyMMdd"),
                SessionTime   = DateTime.Now.ToString("HHmmss"),
                TngnWsvcBuild = Settings.Default.TngnWsvcBuild,
                AvatarSystem  = Settings.Default.AvatarSystem,
                HostDataRoot  = Settings.Default.HostDataRoot,
                HostWwwRoot   = Settings.Default.HostWwwRoot
            };
        }
    }
}