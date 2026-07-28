// 260728_code
// 260728_documentation

using System.IO;
using ScriptLinkStandard.Objects;
using TingenWebService.Avatar;
using TingenWebService.Configuration;
using TingenWebService.Core;

namespace TingenWebService.Session
{
    /// <summary>The Tingen Web Service session object.</summary>
    /// <remarks>
    /// <note type="note" title="About the Tingen Web Service Session">
    /// The <see cref="Session.TwsSession"/> class contains everything needed for the Tingen Web Service to process
    /// a request, including:
    /// <list type="bullet">
    /// <item>The request (<see cref="TwsSession.SentScriptParameter"/>)</item>
    /// <item>The <i>original</i> data from Avatar (<see cref="TwsSession.AvatarOptionObjects"/>)</item>
    /// <item>The <i>working</i> (<see cref="AvatarData.WorkerOptionObject"/>) and <i>completed</i> data (<see cref="AvatarData.CompleteOptionObject"/>) from Avatar</item>
    /// <item>Various settings, configurations, and framework information</item>
    /// <item>Details about the session</item>
    /// </list>
    /// </note>
    /// </remarks>
    internal class TwsSession
    {
        /// <summary>The <see cref="RuntimeConfig"> runtime settings</see>.</summary>
        public RuntimeConfig RtConfig { get; set; }

        /// <summary>The <see cref="Framework"> framework components</see>.</summary>
        public Framework TwsFramework { get; set; }

        /// <summary>The Tingen Web Service <see cref="TngnWsvcConfig"> configuration settings</see>.</summary>
        public TngnWsvcConfig TwsConfig { get; set; }

        /// <summary>The Avatar data associated with the session.</summary>
        public AvatarData AvatarOptionObjects { get; set; }

        /// <summary>The script parameter sent from Avatar.</summary>
        public string SentScriptParameter { get; set; }

        /// <summary>The folder path that will store session data.</summary>
        public string SessionFolder { get; set; }

        /// <summary>Details about the session.</summary>
        /// <remarks>The running log.</remarks>
        public string RunningLog { get; set; }

        /// <summary>Start a new Tingen Web Service session.</summary>
        /// <param name="sentOptionObject">The <see cref="OptionObject2015"/> sent from Avatar.</param>
        /// <param name="sentScriptParameter">The script parameter sent from Avatar.</param>
        /// <param name="rtConfig">The <see cref="RuntimeConfig"> runtime configuration settings</see>.</param>
        /// <param name="twsFramework">The <see cref="Framework"> framework components</see>.</param>
        /// <returns>A new Tingen Web Service session object.</returns>
        internal static TwsSession StartSession(OptionObject2015 sentOptionObject, string sentScriptParameter, RuntimeConfig rtConfig, Framework twsFramework)
        {
            /* DEVNOTE
             * - Use primeval logs here to debug, since logging functionality has not been initialized yet.
             * - Disable this in production.
             */
            //LogEvent.Primeval("StartSession");

            return new TwsSession()
            {
                RtConfig              = rtConfig,
                TwsFramework          = twsFramework,
                TwsConfig             = TngnWsvcConfig.Load(Path.Combine(twsFramework.ConfigRoot, "TngnWsvc.config")),
                AvatarOptionObjects   = AvatarData.InitializeOptionObjects(sentOptionObject),
                SentScriptParameter   = sentScriptParameter,
                SessionFolder         = Path.Combine(twsFramework.SessionRoot, rtConfig.SessionStartDate, sentOptionObject.OptionUserId, rtConfig.SessionStartTime),
                RunningLog            = string.Empty
            };
        }
    }
}