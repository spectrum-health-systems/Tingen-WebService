// 260725_code
// 260725_documentation

using System.Reflection;
using System.Web.Services;
using ScriptLinkStandard.Objects;
using TingenWebService.Configuration;
using TingenWebService.Core;
using TingenWebService.Session;

namespace TingenWebService
{
    /// <summary>The entry class for the Tingen Web Service.</summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class TingenWebService : System.Web.Services.WebService
    {
        /// <summary>Current Tingen Web Service release.</summary>
        /// <remarks>To update the version number, modify the AssemblyInfo.cs file.</remarks>
        private static string _twsRelease { get; set; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>The session instance for the Tingen Web Service.</summary>
        internal TngnWsvcSession TwsSession { get; set; }

        [WebMethod]
        public string GetVersion() => $"VERSION {_twsRelease}";

        [WebMethod]
        public OptionObject2015 RunScript(OptionObject2015 sentOptObj, string sentScriptParam)
        {
            /* For debugging purposes - Disable in production. */
            //Logger.LogEvent.Primeval("TingenWebServiceStarted", $"RunScript called with script parameter: {sentScriptParam}");

            if (IsMissingAvatarData(sentOptObj, sentScriptParam))
            {
                return sentOptObj.ToReturnOptionObject(0, "");
            }
            else
            {
                StartApp(sentOptObj, sentScriptParam);

                // Route to the appropriate place.

                return sentOptObj.ToReturnOptionObject(0, ""); //TODO - Placeholder
            }
        }

        /// <summary>Determine if the Avatar data is missing based.</summary>
        /// <param name="sentOptionObject">The OptionObject sent from Avatar.</param>
        /// <param name="sentScriptParameter">The Script Parameter sent from Avatar.</param>
        /// <returns><c>True</c> if the avatar data is missing; otherwise, <c>false</c>.</returns>
        private static bool IsMissingAvatarData(OptionObject2015 sentOptionObject, string sentScriptParameter)
        {
            if (sentOptionObject == null || string.IsNullOrWhiteSpace(sentScriptParameter))
            {
                /* Use a primeval log to log the error, since the logging functionality is not initialized yet.
                 */
                Logger.LogEvent.Primeval("ERROR-MissingAvatarData", $"[3876]: Missing OptionObject and/or Script Parameter");
                // TODO - Potentially send an email in addition to the error log.

                return true;
            }

            return false;
        }

        /// <summary>Start the Tingen Web Service.</summary>
        /// <param name="sentOptionObject">The OptionObject sent from Avatar.</param>
        /// <param name="sentScriptParameter">The Script Parameter sent from Avatar.</param>
        internal void StartApp(OptionObject2015 sentOptionObject, string sentScriptParameter)
        {
            RuntimeConfig rtConfig = RuntimeConfig.Load(_twsRelease);
            Framework twsFramework = Framework.Load(rtConfig.DataRoot, rtConfig.AvatarSystem);

            Maintenance.SessionMaintenance(rtConfig, twsFramework);

            TwsSession = TngnWsvcSession.StartSession(sentOptionObject, sentScriptParameter, rtConfig, twsFramework);
        }
    }
}