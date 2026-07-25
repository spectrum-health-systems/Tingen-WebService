// 260725_code
// 260725_documentation

using System.IO;
using ScriptLinkStandard.Objects;
using TingenWebService.Configuration;
using TingenWebService.Core;

namespace TingenWebService.Session
{
    internal class TngnWsvcSession
    {
        public RuntimeConfig RtSetting { get; set; }

        public Framework TwsFramework { get; set; }

        public TngnWsvcConfig TwsConfig { get; set; }

        internal static TngnWsvcSession StartSession(OptionObject2015 sentOptObj, string sentScriptParam, RuntimeConfig rtSetting, Framework twsFramwork)
        {
            TngnWsvcSession twsSession = new TngnWsvcSession()
            {
                RtSetting = rtSetting,
                TwsFramework = twsFramwork,
                TwsConfig = TngnWsvcConfig.Load(Path.Combine(twsFramwork.ConfigRoot, "TngnWsvc.config"))
            };

            return twsSession;




        }

        //private static void CreateNew(OptionObject2015 sentOptObj, string sentScriptParam, RuntimeConfig rtSetting, Framework twsFramwork)
        //{
        //    TngnWsvcSession twsSession = new TngnWsvcSession()
        //    {
        //        RtSetting = rtSetting,
        //        TwsFramework = twsFramwork,
        //        TwsConfig = TngnWsvcConfig.Load(Path.Combine(twsFramwork.ConfigRoot, "TngnWsvc.config"))
        //    };
        //}
    }
}