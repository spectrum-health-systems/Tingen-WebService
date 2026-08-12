// 260811_code
// 260811_documentation

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
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="Asmx"]/AppVersion/*'/>
        /// This is set in <c>Properties.AssemblyInfo.cs</c>.
        /// </remarks>
        /// <returns>A string representing the current release.</returns>
        /// <value>e.g., "R26.8"</value>
        private static string _appVersion { get; set; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>Initializes a Tingen Web Service session instance.</summary>
        /// <remarks>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="Asmx"]/Sess/*'/>
        /// </remarks>
        private Sess _sess { get; set; }

        /// <summary>Display the current version of the Tingen Web Service.</summary>
        /// <remarks>This method is required by Avatar.</remarks>
        /// <returns>A string representing the current version.</returns>
        [WebMethod]
        public string GetVersion() => $"VERSION {_appVersion}";

        /// <summary>The main entry method for the Tingen Web Service.</summary>
        /// <param name="sentOptObj">The OptionObject sent from Avatar.</param>
        /// <param name="sentScriptParameter">The Script Parameter sent from Avatar.</param>
        /// <remarks>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="Asmx"]/RunScript/*'/>
        ///
        /// <para><b><i>IMPORTANT</i>: Don't change the parameter names 'sentOptObj' and 'sentScriptParam'!</b><br/> For
        /// some unexplainable (at least to me) reason, the OptionObject and Script Parameter sent from Avatar must be
        /// named "<c>sentOptObj</c>" and "<c>sentScriptParam</c>", otherwise you'll get an error when Avatar tries to
        /// process the response.<br/> <br/> This is only the case for the<c> RunScript()</c> method.<br/> <br/> If
        /// somebody knows why this is the case, please let me know. </para>
        ///
        /// This method is required by Avatar.
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
                //LogEvent.Primeval("PRELOG-TRACE-TingenWebService1"); // TESTING
                LogEvent.Trace(9, _sess.Trc.Lmt, _sess.Trc.Fld);

                AvatarScriptParameter.Parse(_sess);
                //LogEvent.Primeval("PRELOG-TRACE-TingenWebService1"); // TESTING
                LogEvent.Session(_sess);

                return _sess.OptionObject.WorkerOptionObject.ToReturnOptionObject(0, ""); // is this enough? Do we need CompleteOptObj?
            }
        }

        /// <summary>Start the Tingen Web Service.</summary>
        /// <param name="sentOptionObject">The <see cref="AvatarOptionObject.SentOptionObject"/> sent from Avatar.</param>
        /// <param name="sentScriptParameter">The <see cref="AvatarScriptParameter.SentScriptParameter"/> sent from Avatar.</param>
        /// <remarks>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="Asmx"]/StartApp/*'/>
        /// This method is required by Avatar.
        /// </remarks>
        internal void StartApp(OptionObject2015 sentOptionObject, string sentScriptParameter)
        {
            //LogEvent.Primeval("PRELOG-TRACE-TingenWebService-StartApp");

            RuntimeSetting runtimeSetting   = RuntimeSetting.Load(_appVersion);
            FrameworkSetting frameworkConfig = FrameworkSetting.Load(runtimeSetting.DataRoot, runtimeSetting.AvatarSystem);

            FrameworkMaintenance.DailyValidation(runtimeSetting, frameworkConfig);

            SessMaintenance.InitializeNewSession(runtimeSetting, frameworkConfig); // TODO - is this really "initialize", or verify?

            _sess = Sess.StartSession(sentOptionObject, sentScriptParameter, runtimeSetting, frameworkConfig);

            //LogEvent.Primeval("PRELOG-TRACE-TingenWebService-StopApp"); // TESTING
        }
    }
}