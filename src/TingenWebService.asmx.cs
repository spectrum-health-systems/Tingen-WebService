// 260729_code
// 260729_documentation

using System.IO;
using System.Reflection;
using System.Web.Services;
using ScriptLinkStandard.Objects;
using TingenWebService.Core;
using TingenWebService.Core.Avatar;
using TingenWebService.Core.Framework;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Session;

namespace TingenWebService
{
    /// <summary>The Tingen Web Service entry class.</summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class TingenWebService : WebService
    {
        /// <summary>The current Tingen Web Service release.</summary>
        /// <remarks>
        /// Defined here because it's used in multiple places in this class, but I keep forgetting that, and wondering
        /// why I define it here, so that's why this comment exists. Hello future me (again).
        /// </remarks>
        /// <returns>A string representing the current release.</returns>
        private static string _releaseBuild { get; set; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>The Tingen Web Service session instance.</summary>
        /// <remarks>
        /// Defined here because it is initialized in <c>StartApp()</c>, but used in <c>RunScript()</c>, and I think
        /// this is easier to read/understand than <code>twsSession = StartApp()</code>
        /// </remarks>
        private Sess _sess { get; set; }

        /// <summary>Get the current version of the Tingen Web Service.</summary>
        /// <remarks>This method is required by Avatar.</remarks>
        /// <returns>A string representing the current version.</returns>
        [WebMethod]
        public string GetVersion() => $"VERSION {_releaseBuild}";

        /// <summary>The main entry method for the Tingen Web Service.</summary>
        /// <param name="sentOptObj">The OptionObject sent from Avatar.</param>
        /// <param name="sentScriptParam">The Script Parameter sent from Avatar.</param>
        /// <remarks>
        /// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="RunScript"]/TheMagic/*'/>
        /// This method is required by Avatar.
        /// </remarks>
        /// <returns>A (potentially modified) completed OptionObject.</returns>
        [WebMethod]
        public OptionObject2015 RunScript(OptionObject2015 sentOptObj, string sentScriptParam)
        {
            /* Use primeval logs here to debug, since logging functionality has not been initialized yet. */
            //LogEvent.Primeval("TingenWebServiceStarted", RedPrint.DebugStartMessage(sentScriptParam));

            if (IsMissingAvatarData(sentOptObj, sentScriptParam))
            {
                return sentOptObj.ToReturnOptionObject(0, "");
            }
            else
            {
                StartApp(sentOptObj, sentScriptParam);

                LogEvent.Trace(9, _sess.TwsSetting.TraceLevelLimit, _sess.SessionFolder);

                // TODO - Route to the appropriate place.

                LogEvent.Session(_sess);

                return sentOptObj.ToReturnOptionObject(0, "");
            }
        }

        // TODO - Move to ns:Avatar.AvatarData?
        /// <summary>Determines if the Avatar data is missing.</summary>
        /// <param name="sentOptionObject">The OptionObject sent from Avatar.</param>
        /// <param name="sentScriptParameter">The Script Parameter sent from Avatar.</param>
        /// <remarks>
        /// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="AvatarData"]/About/*'/>
        /// </remarks>
        /// <returns><c>True</c> if the avatar data is missing; otherwise, <c>false</c>.</returns>
        private static bool IsMissingAvatarData(OptionObject2015 sentOptionObject, string sentScriptParameter)
        {
            return !AvatarData.WasSent(sentOptionObject) || !AvatarData.WasSent(sentScriptParameter);
        }

        /// <summary>Start the Tingen Web Service.</summary>
        /// <param name="sentOptionObject">The <see cref="AvatarData.SentOptObj"/> sent from Avatar.</param>
        /// <param name="sentScriptParameter"> The <see cref="AvatarData.SentScriptParam"/> sent from Avatar.</param>
        /// <remarks>
        /// This method does the heavy-lifting of starting the Tingen Web Service by loading multiple configurations,
        /// validating requirements, and initializing the session state.
        /// </remarks>
        internal void StartApp(OptionObject2015 sentOptionObject, string sentScriptParameter)
        {
            RuntimeConfig runtimeConfig = RuntimeConfig.Load(_releaseBuild);

            FrwkConfig frwkConfig = FrwkConfig.Load(runtimeConfig.DataRoot, runtimeConfig.AvatarSystem);

            SessMaint.InitializeNewSession(runtimeConfig, frwkConfig); // TODO - is this really "initialize", or verify?

            _sess = Sess.StartSession(sentOptionObject, sentScriptParameter, runtimeConfig, frwkConfig);

            if (!File.Exists(Path.Combine(frwkConfig.SysLogRoot, "Configuration.current"))) // TODO - Move this somewhere else?
            {
                LogMaintenance.ResetSystemLogs(_sess); // TODO - Test this.
            }
        }
    }
}