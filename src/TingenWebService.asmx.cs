// 260805_code
// 260805_documentation

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
        /// this is easier to read/understand than <c>twsSession = StartApp()</c>
        /// </remarks>
        private Sess _sess { get; set; }

        /// <summary>Get the current version of the Tingen Web Service.</summary>
        /// <remarks>This method is required by Avatar.</remarks>
        /// <returns>A string representing the current version.</returns>
        [WebMethod]
        public string GetVersion() => $"VERSION {_releaseBuild}";

        /// <summary>The main entry method for the Tingen Web Service.</summary>
        /// <param name="sentOptObj">The OptionObject sent from Avatar.</param>
        /// <param name="sentScriptParameter">The Script Parameter sent from Avatar.</param>
        /// <remarks>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="Method"]/RunScript/*'/>
        /// This method is required by Avatar.
        /// </remarks>
        /// <returns>A (potentially modified) completed OptionObject.</returns>
        [WebMethod]
        public OptionObject2015 RunScript(OptionObject2015 sentOptObj, string sentScriptParameter)
        {
            //Core.Logger.LogEvent.Primeval("PRELOG-TRACE-TingenWebService-Runscript", Core.Trove.SysMsg.PretraceStartMessage(sentScriptParameter));

            if (!AvatarOptionObject.WasSent(sentOptObj) || !AvatarScriptParameter.WasSent(sentScriptParameter))
            {
                return sentOptObj.ToReturnOptionObject(0, "");
            }
            else
            {
                StartApp(sentOptObj, sentScriptParameter);

                LogEvent.Trace(9, _sess.TwsSetting.TraceLimit, _sess.SessionFolder);

                // TODO - Route to the appropriate place.

                LogEvent.Session(_sess);

                return _sess.OptionObject.WorkerOptionObject.ToReturnOptionObject(0, ""); // is this enough? Do we need CompleteOptObj?
            }
        }

        // TODO - Move to ns:Avatar.AvatarData?
        /// <summary>Determines if the Avatar data is missing.</summary>
        /// <param name="sentOptionObject">The OptionObject sent from Avatar.</param>
        /// <param name="sentScriptParameter">The Script Parameter sent from Avatar.</param>
        /// <remarks>
        /// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="AvatarData"]/AboutAvatarData/*'/>
        /// <br/>
        /// </remarks>
        /// <returns><c>True</c> if the avatar data is missing; otherwise, <c>false</c>.</returns>
        private static bool IsMissingAvatarData(OptionObject2015 sentOptionObject, string sentScriptParameter)
        {
            //LogEvent.Primeval("PRELOG-TRACE-TingenWebService-IsMissingAvatarData");

            return !AvatarOptionObject.WasSent(sentOptionObject) || !AvatarScriptParameter.WasSent(sentScriptParameter);
        }

        /// <summary>Start the Tingen Web Service.</summary>
        /// <param name="sentOptionObject">The <see cref="AvatarOptionObject.SentOptionObject"/> sent from Avatar.</param>
        /// <param name="sentScriptParameter"> The <see cref="AvatarScriptParameter.SentScriptParameter"/> sent from Avatar.</param>
        /// <remarks>
        /// This method does the heavy-lifting of starting the Tingen Web Service by loading multiple configurations,
        /// validating requirements, and initializing the session state.
        /// </remarks>
        internal void StartApp(OptionObject2015 sentOptionObject, string sentScriptParameter)
        {
            //LogEvent.Primeval("PRELOG-TRACE-TingenWebService-StartApp");

            RuntimeConfig runtimeConfig     = RuntimeConfig.Load(_releaseBuild);
            FrameworkConfig frameworkConfig = FrameworkConfig.Load(runtimeConfig.DataRoot, runtimeConfig.AvatarSystem);

            FrameworkMaintenance.DailyVerify(runtimeConfig, frameworkConfig);

            SessMaintenance.InitializeNewSession(runtimeConfig, frameworkConfig); // TODO - is this really "initialize", or verify?

            _sess = Sess.StartSession(sentOptionObject, sentScriptParameter, runtimeConfig, frameworkConfig);

            AvatarScriptParameter.Parse(_sess);
        }
    }
}