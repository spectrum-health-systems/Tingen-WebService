// 260725_code
// 260725_documentation

using System.IO;
using ScriptLinkStandard.Objects;
using TingenWebService.Configuration;
using TingenWebService.Core;

namespace TingenWebService.Session
{
    /// <summary>Tingen Web Service session.</summary>
    internal class TngnWsvcSession
    {
        /// <summary>Runtime configuration settings for the session.</summary>
        public RuntimeConfig RtSetting { get; set; }

        /// <summary>Framework instance associated with the session.</summary>
        public Framework TwsFramework { get; set; }

        /// <summary>Web service configuration settings.</summary>
        public TngnWsvcConfig TwsConfig { get; set; }

        /// <summary>Starts a new Tingen Web Service session.</summary>
        /// <param name="sentOptObj">The option object.</param>
        /// <param name="sentScriptParam">The script parameter.</param>
        /// <param name="rtSetting">The runtime configuration settings.</param>
        /// <param name="twsFramwork">The framework instance.</param>
        /// <returns>A new Tingen Web Service session.</returns>
        internal static TngnWsvcSession StartSession(OptionObject2015 sentOptObj, string sentScriptParam, RuntimeConfig rtSetting, Framework twsFramwork)
        {
            return new TngnWsvcSession()
            {
                RtSetting    = rtSetting,
                TwsFramework = twsFramwork,
                TwsConfig    = TngnWsvcConfig.Load(Path.Combine(twsFramwork.ConfigRoot, "TngnWsvc.config"))
            };
        }
    }
}