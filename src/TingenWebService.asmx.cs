// 260728_code
// 260728_documentation

using System.Reflection;
using System.Web.Services;
using ScriptLinkStandard.Objects;
using TingenWebService.Configuration;
using TingenWebService.Core;
using TingenWebService.Logger;
using TingenWebService.Session;

namespace TingenWebService
{
    /// <summary>The Tingen Web Service entry class.</summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class TingenWebService : WebService
    {
        /// <summary>The current Tingen Web Service release.</summary>
        /// <remarks>To update the version number, modify the AssemblyInfo.cs file.</remarks>
        /// <returns>A string representing the current release.</returns>
        private static string _twsRelease { get; set; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>The Tingen Web Service session instance.</summary>
        /// <remarks>
        /// This property is used to maintain the session state for the Tingen Web Service. It is initialized in the
        /// StartApp method and is used throughout the service to access session-specific data and configurations.
        /// </remarks>
        /// <returns>The (empty) Tingen Web Service session instance.</returns>
        internal TngnWsvcSession TwsSession { get; set; }

        /// <summary>Get the current version of the Tingen Web Service.</summary>
        /// <remarks>This method is required by Avatar.</remarks>
        /// <returns>A string representing the current version.</returns>
        [WebMethod]
        public string GetVersion() => $"VERSION {_twsRelease}";

        /// <summary>The main entry method for the Tingen Web Service.</summary>
        /// <param name="sentOptObj">The OptionObject sent from Avatar.</param>
        /// <param name="sentScriptParam">The Script Parameter sent from Avatar.</param>
        /// <remarks>This method is where the magic happens (and is required by Avatar).</remarks>
        /// <returns>A completed OptionObject, which has potentially been modified.</returns>
        [WebMethod]
        public OptionObject2015 RunScript(OptionObject2015 sentOptObj, string sentScriptParam)
        {
            /* DEVNOTE
             * - Use primeval logs here to debug, since logging functionality has not been initialized yet.
             * - Disable this in production.
             */
            //LogEvent.Primeval("TingenWebServiceStarted", Epistle.DebugStartMessage(sentScriptParam));

            if (IsMissingAvatarData(sentOptObj, sentScriptParam))
            {
                return sentOptObj.ToReturnOptionObject(0, "");
            }
            else
            {
                StartApp(sentOptObj, sentScriptParam);

                LogEvent.Trace(9, TwsSession.TwsConfig.TraceLevelLimit, TwsSession.SessionFolder);

                // TODO - Route to the appropriate place.

                LogEvent.Session(TwsSession);

                return sentOptObj.ToReturnOptionObject(0, ""); //TODO - Placeholder
            }
        }

        /// <summary>Determines if the Avatar data is missing.</summary>
        /// <param name="sentOptionObject">The OptionObject sent from Avatar.</param>
        /// <param name="sentScriptParameter">The Script Parameter sent from Avatar.</param>
        /// <remarks>
        /// <note type="note" title="About Avatar data">
        /// Avatar sends two pieces of data to the Tingen Web Service:
        /// <list type="number">
        /// <item>The <see cref="AvatarComponent.SentOptionObject"/></item>
        /// <item>The <see cref="AvatarComponent.SentScriptParameter"/> </item>
        /// </list>
        /// If either of these components are missing, the Tingen Web Service cannot function properly. This method
        /// checks for the presence of both components and logs an error if either is missing.</note>
        /// </remarks>
        /// <returns><c>True</c> if the avatar data is missing; otherwise, <c>false</c>.</returns>
        private static bool IsMissingAvatarData(OptionObject2015 sentOptionObject, string sentScriptParameter)
        {
            return !Avatar.AvatarData.WasSent(sentOptionObject) || !Avatar.AvatarData.WasSent(sentScriptParameter);
        }

        /// <summary>Start the Tingen Web Service.</summary>
        /// <param name="sentOptionObject">The <see cref="AvatarComponent.SentOptionObject"/> sent from Avatar.</param>
        /// <param name="sentScriptParameter">
        /// The <see cref="AvatarComponent.SentScriptParameter"/> sent from Avatar.
        /// </param>
        /// <remarks>
        /// This method does the heavy-lifting of starting the Tingen Web Service by loading multiple configurations,
        /// validating requirements, and initializing the session state.
        /// </remarks>
        internal void StartApp(OptionObject2015 sentOptionObject, string sentScriptParameter)
        {
            /* DEVNOTE
             * - Use primeval logs here to debug, since logging functionality has not been initialized yet.
             * - Disable this in production.
             */
            //LogEvent.Primeval("StartApp");

            RuntimeConfig rtConfig = RuntimeConfig.Load(_twsRelease);

            Framework twsFramework = Framework.Load(rtConfig.DataRoot, rtConfig.AvatarSystem);

            Maintenance.SessionMaintenance(rtConfig, twsFramework);

            TwsSession = TngnWsvcSession.StartSession(sentOptionObject, sentScriptParameter, rtConfig, twsFramework);
        }
    }
}