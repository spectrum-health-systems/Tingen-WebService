// 260804_code
// 260730_documentation

using System.IO;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Session;

namespace TingenWebService.Module.OpenIncident
{
    internal class OpenIncidentRequest
    {
        /// <summary>Parses the OpenIncident request and sends it to the appropriate handler.</summary>
        /// <remarks>
        /// This method:
        /// <list type="number">
        /// <item>Loads the OpenIncident module configuration.</item>
        /// <item>Routes the request.</item>
        /// </list>
        /// </remarks>
        /// <param name="tngnWsvcSession">The current Tingen Web Service session being processed.</param>
        /// <example>
        /// <code>
        /// OpenIncidentEvent.Parse(tngnWsvcSession);
        /// </code>
        /// </example>
        internal static void Parse(Sess sess)
        {
            LogEvent.Trace(1, sess.TwsSetting.TraceLimit, sess.FrwkSetting.SessionRoot);

            var configPath = Path.Combine(sess.FrwkSetting.ConfigRoot, "OpenIncident.config");
            var openIncidentConfig = OpenIncidentConfig.Load(configPath, sess.TwsSetting.TraceLimit, sess.FrwkSetting.SessionRoot);

            var moduleEnabled = openIncidentConfig.Mode.Equals("enabled", System.StringComparison.OrdinalIgnoreCase);

            var bypassUser = openIncidentConfig.BypassList.Contains(sess.AvatarData.SentOptObj.OptionUserId);

            if (bypassUser)
            {
                LogEvent.Trace(4, sess.TwsSetting.TraceLimit, sess.FrwkSetting.SessionRoot);

                sess.RunningLog += $"Bypass user {sess.AvatarData.SentOptObj.OptionUserId} detected. OpenIncident module will not be processed.\n";

                return;
            }

            if (moduleEnabled)
            {

                var s = "ste";
                //    LogEvent.Trace(2, traceLimit, sessionFolder);

                //    switch (scriptParameter)
                //    {
                //        case "_formload":
                //            LogEvent.Trace(3, traceLimit, sessionFolder);

                //            FormLoad(tngnWsvcSession, openIncidentConfig);

                //            break;

                //        case "_prefile":
                //            LogEvent.Trace(3, traceLimit, sessionFolder);

                //            PreFile(tngnWsvcSession, openIncidentConfig);

                //            break;

                //        case "_postfile":
                //            LogEvent.Trace(3, traceLimit, sessionFolder);

                //            OpenIncidentRequest.PostFileEvent(tngnWsvcSession, openIncidentConfig);

                //            break;

                //        default:
                //            LogEvent.Trace(3, traceLimit, sessionFolder);

                //            // TODO Hard error for unsupported script parameter?

                //            break;
                //    }
            }
        }

    }

}