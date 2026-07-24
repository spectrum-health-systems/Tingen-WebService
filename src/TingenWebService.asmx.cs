// 260724_code
// 260724_documentation

using System;
using System.IO;
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

        /// <summary>The runtime configuration for the Tingen Web Service.</summary>
        internal RuntimeConfig RtSetting { get; set; }

        /// <summary>The framework instance for the Tingen Web Service.</summary>
        internal Framework TwsFramework { get; set; }

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
                StartApp();

                // Route

                return sentOptObj.ToReturnOptionObject(0, ""); //TODO - Placeholder
            }
        }

        /// <summary>Determine if the Avatar data is missing based.</summary>
        /// <param name="sentOptObj">The OptionObject sent from Avatar.</param>
        /// <param name="sentScriptParam">The Script Parameter sent from Avatar.</param>
        /// <returns><c>True</c> if the avatar data is missing; otherwise, <c>false</c>.</returns>
        private static bool IsMissingAvatarData(OptionObject2015 sentOptObj, string sentScriptParam)
        {
            if (sentOptObj == null || string.IsNullOrWhiteSpace(sentScriptParam))
            {
                // TODO - Potentially send an email in addition to the error log.
                Logger.LogEvent.Primeval("ERROR-MissingAvatarData", $"[3876]: Missing OptionObject and/or Script Parameter");

                return true;
            }

            return false;
        }

        /// <summary>Start the Tingen Web Service.</summary>
        internal void StartApp()
        {
            RtSetting    = RuntimeConfig.Load();
            TwsFramework = Framework.Load(RtSetting.DataRoot, RtSetting.WwwRoot, RtSetting.AvatarSystem);

            var historyFile = Path.Combine(RtSetting.DataRoot, "WebService", RtSetting.AvatarSystem, "History", DateTime.Now.ToString("yyyyMMdd"));

            if (File.Exists(historyFile))
            {
                //TwsSession = TngnWsvcSession.Load(sentOptObj, sentScriptParam, TwsFramework);
            }
            else
            {
                Framework.Verify(TwsFramework);

                Du.DuFile.DeadDrop(historyFile);
            }
        }


    }
}