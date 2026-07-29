// 260729_code
// 260729_documentation

using System.IO;
using ScriptLinkStandard.Objects;
using TingenWebService.Core.Avatar;
using TingenWebService.Core.Configuration;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Trove;

namespace TingenWebService.Core
{
    /// <summary>The Tingen Web Service session object.</summary>
    /// <remarks>
    /// <note type="note" title="About the Tingen Web Service Session">
    /// The <see cref="TingenWebService.Core.Session.TwsSession"/> class contains everything needed for the Tingen Web Service to process
    /// a request, including:
    /// <list type="bullet">
    /// <item>The request (<see cref="TwsSession.SentScriptParameter"/>)</item>
    /// <item>The <i>original</i> data from Avatar (<see cref="TwsSession.AvatarData"/>)</item>
    /// <item>The <i>working</i> (<see cref="AvatarData.WorkerOptionObject"/>) and <i>completed</i> data (<see cref="AvatarData.CompleteOptionObject"/>) from Avatar</item>
    /// <item>Various settings, configurations, and framework information</item>
    /// <item>Details about the session</item>
    /// </list>
    /// </note>
    /// </remarks>
    internal class TwsSession
    {
        /// <summary>The <see cref="RuntimeConfiguration"> runtime settings</see>.</summary>
        public RuntimeConfiguration RtConfig { get; set; }

        /// <summary>The <see cref="Framework"> framework components</see>.</summary>
        public Framework.FrameworkConfiguration TwsFramework { get; set; }

        /// <summary>The Tingen Web Service <see cref="Configuration.TwsConfiguration"> configuration settings</see>.</summary>
        public TwsConfiguration TwsConfig { get; set; }
        /// <summary>The current date (yyMMdd).</summary>

        /// <summary>The Avatar data associated with the session.</summary>
        public AvatarData AvatarData { get; set; }

        /// <summary>The folder path that will store session data.</summary>
        public string SessionFolder { get; set; }

        /// <summary>Details about the session.</summary>
        /// <remarks>The running log.</remarks>
        public string RunningLog { get; set; }

        internal static TwsSession Init()
        {
            /* For debugging prior to logging functionality being initialized.
             * Disable in production.
             */
            //Logger.LogEvent.Primeval("InitTwsSession");



            var session = new TwsSession();







            return session;
        }

        /// <summary>Start a new Tingen Web Service session.</summary>
        /// <param name="sentOptionObject">The <see cref="OptionObject2015"/> sent from Avatar.</param>
        /// <param name="sentScriptParameter">The script parameter sent from Avatar.</param>
        /// <param name="rtConfig">The <see cref="RuntimeConfiguration"> runtime configuration settings</see>.</param>
        /// <param name="twsFramework">The <see cref="FrameworkConfig"> framework components</see>.</param>
        /// <returns>A new Tingen Web Service session object.</returns>
        internal static TwsSession StartSession(OptionObject2015 sentOptionObject, string sentScriptParameter, RuntimeConfiguration rtConfig, Framework.FrameworkConfiguration twsFramework)
        {
            /* DEVNOTE
             * - Use primeval logs here to debug, since logging functionality has not been initialized yet.
             * - Disable this in production.
             */
            LogEvent.Primeval("StartSession");

            TwsConfiguration twsConfig = TwsConfiguration.Load(Path.Combine(twsFramework.ConfigRoot, "TwsConfig.config"));

            TwsSession twsSession = new TwsSession()
            {
                RtConfig      = rtConfig,
                TwsFramework  = twsFramework,
                TwsConfig     = TwsConfiguration.Load(Path.Combine(twsFramework.ConfigRoot, "TngnWsvc.config")),
                AvatarData    = AvatarData.Initialize(sentOptionObject, sentScriptParameter),
                SessionFolder = Path.Combine(twsFramework.SessionRoot, rtConfig.SessionStartDate, sentOptionObject.OptionUserId, rtConfig.SessionStartTime),
                RunningLog    = string.Empty
            };

            if (!File.Exists(Path.Combine(twsFramework.SysLogRoot, "Configuration.current")))
            {
                Logger.LoggerMaintenance.ResetSyslogs(twsFramework.SysLogRoot);
                LogEvent.SystemLog(twsFramework.SysLogRoot, "Runtime.current", Epistle.RuntimeDetails(twsSession.RtConfig));
                LogEvent.SystemLog(twsFramework.SysLogRoot, "Framework.current", Epistle.FrameworkDetails(twsFramework));
                LogEvent.SystemLog(twsFramework.SysLogRoot, "Configuration.current", Epistle.ConfigurationDetails(twsSession.TwsConfig));
            }

            return twsSession;
        }
    }
}