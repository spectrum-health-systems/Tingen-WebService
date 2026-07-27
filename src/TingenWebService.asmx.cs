// 260726_code
// 260726_documentation

using System.Reflection;
using System.Web.Services;
using ScriptLinkStandard.Objects;
using TingenWebService.Configuration;
using TingenWebService.Core;
using TingenWebService.Logger;
using TingenWebService.Session;
using TingenWebService.Trove;

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

        /// <summary>Tingen Web Servic session instance.</summary>
        internal TngnWsvcSession TwsSession { get; set; }

        /// <summary>Get the current version of the Tingen Web Service.</summary>
        /// <remarks>
        /// This method is required by Avatar.
        /// </remarks>
        /// <returns>A string representing the current version.</returns>
        [WebMethod]
        public string GetVersion() => $"VERSION {_twsRelease}";

        /// <summary>Main entry method.</summary>
        /// <param name="sentOptObj">The OptionObject sent from Avatar.</param>
        /// <param name="sentScriptParam">The Script Parameter sent from Avatar.</param>
        /// <remarks>
        /// This method is required by Avatar.
        /// </remarks>
        /// <returns>A completed OptionObject, which has potentially been modified.</returns>
        [WebMethod]
        public OptionObject2015 RunScript(OptionObject2015 sentOptObj, string sentScriptParam)
        {
            /* For debugging prior to logging functionality being initialized.
             * Disable in production.
             */
            //LogEvent.Primeval("TingenWebServiceStarted", Epistle.DebugStartMessage(sentScriptParam));

            if (IsMissingAvatarData(sentOptObj, sentScriptParam))
            {
                return sentOptObj.ToReturnOptionObject(0, "");
            }
            else
            {
                StartApp(sentOptObj, sentScriptParam); // Initializes the session

                LogEvent.Trace(9, TwsSession.TwsConfig.TraceLevelLimit, TwsSession.SessionFolder);

                // TODO - Route to the appropriate place.

                LogEvent.Session(TwsSession);

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
                LogEvent.Primeval("[CR3876]MissingData", Epistle.Error3876());
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
            /* For debugging prior to logging functionality being initialized.
             * Disable in production.
             */
            //LogEvent.Primeval("StartApp");

            RuntimeConfig rtConfig = RuntimeConfig.Load(_twsRelease);

            Framework twsFramework = Framework.Load(rtConfig.DataRoot, rtConfig.AvatarSystem);

            Maintenance.SessionMaintenance(rtConfig, twsFramework);

            TwsSession = TngnWsvcSession.StartSession(sentOptionObject, sentScriptParameter, rtConfig, twsFramework);
        }
    }
}