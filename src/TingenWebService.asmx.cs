// 260812_code
// 260812_documentation

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
        /// <summary>Gets the current version of the Tingen Web Service.</summary>
        /// <remarks>
        /// Defined here because it's used in multiple places in this class, but I keep forgetting that, and wondering why I define it
        /// here, so that's why this comment exists. Hello future me (again)!<br/>
        /// <br/>
        /// This is set in <c>Properties.AssemblyInfo.cs</c>.
        /// </remarks>
        /// <returns>A string representing the current release.</returns>
        /// <value>e.g., "R26.8.0.0"</value>
        private static string _appVersion { get; set; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>Initializes a Tingen Web Service session instance.</summary>
        /// <remarks>
        /// Defined here because it is <i>built</i> in <c>StartApp()</c>, but <i>used</i> in <c>RunScript()</c>, and I think this is easier
        /// to read/understand than something like:
        /// <code>
        ///     Sess sess = StartApp();
        /// </code>
        /// </remarks>
        private Sess _sess { get; set; }

        /// <summary>Display the current <see cref="_appVersion"> version</see> of the Tingen Web Service.</summary>
        /// <remarks>This method is required by Avatar.</remarks>
        /// <returns>A string representing the current version.</returns>
        [WebMethod]
        public string GetVersion() => $"VERSION {_appVersion}";

        /// <summary>The main entry method for the Tingen Web Service.</summary>
        /// <param name="sentOptObj">The OptionObject sent from Avatar.</param>
        /// <param name="sentScriptParam">The Script Parameter sent from Avatar.</param>
        /// <remarks>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="Asmx"]/TheMagicOfRunScript/*'/>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="Asmx"]/DontChangeTheNames/*'/>
        /// The reason that all of this logic is <i>here</i> is because this way the
        /// <see cref="AvatarOptionObject.CompleteOptionObject">Completed OptionObject</see> is returned to Avatar from a single location,
        /// regardless of whether the request was successful or not. This method is required by Avatar.
        /// </remarks>
        /// <returns>A (potentially modified) completed OptionObject.</returns>
        [WebMethod]
        public OptionObject2015 RunScript(OptionObject2015 sentOptObj, string sentScriptParam)
        {
            //LogEvent.Primeval("PRELOG-TRACE-TingenWebService-Runscript", $"{sentScriptParam}");

            if (!AvatarOptionObject.WasSent(sentOptObj) || !AvatarScriptParameter.WasSent(sentScriptParam))
            {
                return sentOptObj.ToReturnOptionObject(0, "");
            }
            else
            {
                StartApp(sentOptObj, sentScriptParam);

                /* DEVNOTE: This is the first place where you can create a trace log.
                 */
                LogEvent.Trace(9, _sess.Trc.Lmt, _sess.Trc.Fld);

                AvatarScriptParameter.Parse(_sess);

                LogEvent.Session(_sess);

                return _sess.OptionObject.WorkerOptionObject.ToReturnOptionObject(0, ""); // is this enough? Do we need CompleteOptObj?
            }
        }

        /// <summary>Start the Tingen Web Service.</summary>
        /// <param name="sentOptionObject">The <see cref="AvatarOptionObject.SentOptionObject"/> sent from Avatar.</param>
        /// <param name="sentScriptParameter">The <see cref="AvatarScriptParameter.SentScriptParameter"/> sent from Avatar.</param>
        /// <remarks>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="Asmx"]/TheMagicOfStartApp/*'/>
        /// This method is required by Avatar.
        /// </remarks>
        internal void StartApp(OptionObject2015 sentOptionObject, string sentScriptParameter)
        {
            //LogEvent.Primeval("PRELOG-TRACE-TingenWebService-StartApp");

            RuntimeSetting runtimeSetting = RuntimeSetting.Load(_appVersion);

            FrameworkSetting frameworkConfig = FrameworkSetting.Load(runtimeSetting.DataRoot, runtimeSetting.AvatarSystem);

            FrameworkUtility.DailyValidation(runtimeSetting, frameworkConfig);

            SessUtility.InitializeNewSession(runtimeSetting, frameworkConfig); // TODO - is this really "initialize", or verify?

            _sess = Sess.StartSession(sentOptionObject, sentScriptParameter, runtimeSetting, frameworkConfig);
        }
    }
}