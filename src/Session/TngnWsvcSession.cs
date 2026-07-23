// 260723_code
// 260723_documentation

using ScriptLinkStandard.Objects;

namespace TingenWebService.Session
{
    internal class TngnWsvcSession
    {
        public Configuration.RuntimeConfig RuntimeSetting { get; set; }

        internal static void StartSession(OptionObject2015 sentOptObj, string sentScriptParam)
        {
        }

        internal static TngnWsvcSession Load(OptionObject2015 sentOptObj, string sentScriptParam)
        {
            return new TngnWsvcSession()
            {
                RuntimeSetting = Configuration.RuntimeConfig.Load()
            };
        }
    }
}