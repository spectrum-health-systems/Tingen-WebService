// 260513_code.
// 260601_documentation.

using System.Collections.Generic;
using System.Reflection;
using System.Web.Services;
using ScriptLinkStandard.Objects;
using TingenWebService.Core.Catalog;
using TingenWebService.Core.Configuration;
using TingenWebService.Core.Logger;
using TingenWebService.Core.TingenWsvcSession;
using TingenWebService.Properties;

namespace TingenWebService
{
    /// <summary>The entry class for the Tingen Web Service.</summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class TingenWebService : WebService
    {
        /// <summary>The current version of the Tingen Web Service.</summary>
        /// <remarks>To update the version number, modify the AssemblyInfo.cs file.</remarks>
        /// <seealso href="https://github.com/spectrum-health-systems/Tingen-WebService/blob/development/docs/man/dev/source-code/Asmx.md">TingenWebService.asmx.cs</seealso>
        public static string TngnWsvcVersion { get; set; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>Get the current version of the Tingen Web Service.</summary>
        /// <remarks>This method is required by Avatar.</remarks>
        /// <returns>The current <see cref="TngnWsvcVersion"/> of the Tingen Web Service.</returns>
        [WebMethod]
        public string GetVersion() => $"VERSION {TngnWsvcVersion}";

        /// <summary>Determines what the Tingen Web Service will do, if anything.</summary>
        /// <remarks>
        /// This method is required by Avatar.<br/>
        /// <br/>
        /// <note type="C#" title="What might happen">
        /// One of three things will occur when this method is called:
        /// <list type="bullet">
        /// <item>If a <b>critical failure</b> is detected, an empty <see cref="OptionObject2015"/> is returned immediately.</item>
        /// <item>If the mode is <c>enabled</c> or <c>passthrough</c>, a <see cref="TngnWsvcSession"/> is started and the completed option object is returned.</item>
        /// <item>In all other cases, an empty <see cref="OptionObject2015"/> is returned as a fallback.</item>
        /// </list>
        /// </note>
        /// </remarks>
        /// <param name="origOptObj">The original <see cref="OptionObject2015"/> passed by Avatar.</param>
        /// <param name="origScriptParam">The original ScriptParameter passed by Avatar.</param>
        /// <returns>A completed <see cref="OptionObject2015"/> to be returned to Avatar.</returns>
        [WebMethod]
        public OptionObject2015 RunScript(OptionObject2015 origOptObj, string origScriptParam)
        {
            /* Since it's too early to call a Trace log event, you'll need to use a Debug log event here for
             * troubleshooting. Leave this commented out when not needed.
             */
            //LogEvent.Debug();

            Dictionary<string, string> runtimeConfig = RuntimeConfig.Load(Settings.Default, TngnWsvcVersion);

            if (CriticalFailureOccurred(origOptObj, origScriptParam, runtimeConfig["Mode"]))
            {
                /* Eventually we'll want to generate a specific error log for this scenario (and probably
                 * send an email), but for now we'll just call a Debug log event.
                 */
                LogEvent.Debug();

                return origOptObj.ToReturnOptionObject(0, "");
            }
            else if (runtimeConfig["Mode"] == "enabled" || runtimeConfig["Mode"] == "passthrough")
            {
                TngnWsvcSession wsvcSession = TngnWsvcSession.Start(origOptObj, origScriptParam, runtimeConfig);

                LogEvent.Session(wsvcSession);

                return wsvcSession.OptObj.Completed;
            }
            else
            {
                /* Technically we should never get here because of the CriticalFailureOccurred() check above, and
                 * eventually we'll want to generate a specific error log for this scenario (and probably send an
                 * email), but for now we'll just call a Debug log event.. */
                LogEvent.Debug();

                return origOptObj.ToReturnOptionObject(0, "");
            }
        }

        /// <summary>Determines whether a critical failure has occurred.</summary>
        /// <remarks>
        /// If a critical failure is <i>not</i> detected, <c>false</c> is returned and the request <i>is</i> processed.<br/>
        /// <br/>
        /// If a critical failure <i>is</i> detected, <c>true</c> is returned and the request will <i>not</i> be processed.<br/>
        /// <br/>
        /// <note type="tip" title="What is a 'critical failure'?">
        /// A critical failure occurs when one of the following conditions is met:
        /// <list type="bullet">
        /// <item>The <paramref name="origOptObj"/> is <c>null</c>, or <paramref name="origScriptParam"/> is null or whitespace.</item>
        /// <item>The <paramref name="tngnWsvcMode"/> is <c>"disabled"</c>.</item>
        /// <item>The <paramref name="tngnWsvcMode"/> is an unrecognized value.</item>
        /// </list>
        /// </note>
        /// </remarks>
        /// <param name="origOptObj">The original <see cref="OptionObject2015"/> received from the caller.</param>
        /// <param name="origScriptParam">The script parameter string passed with the request.</param>
        /// <param name="tngnWsvcMode">The current operating mode of the Tingen web service (e.g., <c>"enabled"</c>, <c>"disabled"</c>, <c>"passthrough"</c>).</param>
        /// <returns><c>true</c> if a critical failure condition is detected; otherwise, <c>false</c>.</returns>
        internal static bool CriticalFailureOccurred(OptionObject2015 origOptObj, string origScriptParam, string tngnWsvcMode)
        {
            if (origOptObj == null || string.IsNullOrWhiteSpace(origScriptParam))
            {
                /* Eventually we'll want to generate a specific error log for this scenario (and probably
                 * send an email), but for now we'll just call a Debug log event.
                 */
                LogEvent.Debug(msg_TngnWscv.MissingComponent(origOptObj, origScriptParam));

                return true;
            }
            else
            {
                switch (tngnWsvcMode)
                {
                    case "enabled":
                    case "passthrough":

                        LogEvent.Debug();
                        return false;

                    case "disabled":
                        /* Eventually we'll want to generate a specific error log for this scenario (and probably
                         * send an email), but for now we'll just call a Debug log event.
                         */
                        LogEvent.Debug(msg_TngnWscv.DisabledMode());

                        return true;

                    default:
                        /* Eventually we'll want to generate a specific error log for this scenario (and probably
                         * send an email), but for now we'll just call a Debug log event.
                         */
                        LogEvent.Debug(msg_TngnWscv.UnknownMode());

                        return true;
                }
            }
        }
    }
}