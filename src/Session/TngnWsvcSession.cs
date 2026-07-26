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
        public RuntimeConfig RtConfig { get; set; }

        /// <summary>Framework instance associated with the session.</summary>
        public Framework TwsFramework { get; set; }

        /// <summary>Web service configuration settings.</summary>
        public TngnWsvcConfig TwsConfig { get; set; }

        /// <summary>The option object sent to the session.</summary>
        public OptionObject2015 SentOptionObject { get; set; }

        /// <summary>The option object currently being worked on in the session.</summary>  
        public OptionObject2015 WorkingOptionObject { get; set; }

        /// <summary>The option object that has been completed in the session.</summary>
        public OptionObject2015 CompletedOptionObject { get; set; }

        /// <summary>The script parameter sent to the session.</summary>
        public string SentScriptParameter { get; set; }

        /// <summary>Details about the session.</summary>
        public string SessionDetails { get; set; }

        /// <summary>Starts a new Tingen Web Service session.</summary>
        /// <param name="sentOptionObject">The option object.</param>
        /// <param name="sentScriptParameter">The script parameter.</param>
        /// <param name="rtSetting">The runtime configuration settings.</param>
        /// <param name="twsFramwork">The framework instance.</param>
        /// <returns>A new Tingen Web Service session.</returns>
        internal static TngnWsvcSession StartSession(OptionObject2015 sentOptionObject, string sentScriptParameter, RuntimeConfig rtConfig, Framework twsFramework)
        {
            return new TngnWsvcSession()
            {
                RtConfig              = rtConfig,
                TwsFramework          = twsFramework,
                TwsConfig             = TngnWsvcConfig.Load(Path.Combine(twsFramework.ConfigRoot, "TngnWsvc.config")),
                SentOptionObject      = sentOptionObject,
                WorkingOptionObject   = sentOptionObject.Clone(),
                CompletedOptionObject = null,
                SentScriptParameter   = sentScriptParameter,
                SessionDetails        = string.Empty

            };
        }
    }
}