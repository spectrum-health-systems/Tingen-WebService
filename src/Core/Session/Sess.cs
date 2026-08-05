// 260804_code
// 260729_documentation

using System.IO;
using ScriptLinkStandard.Objects;
using TingenWebService.Core.Avatar;

namespace TingenWebService.Core.Session
{
    /// <summary>The Tingen Web Service session object.</summary>
    /// <remarks>
    /// <note type="note" title="About the Tingen Web Service Session">
    /// This class contains everything needed for the Tingen Web Service to process
    /// a request, including:
    /// <list type="bullet">
    /// <item>The request (sentScriptParam)</item>
    /// <item>The <i>original</i> data from Avatar (<see cref="Avatar.AvatarOptionObject"/>)</item>
    /// <item>The <i>working</i> (<see cref="AvatarOptionObject.WorkerOptionObject"/>) and <i>completed</i> data (<see cref="AvatarOptionObject.CompleteOptionObject"/>) from Avatar</item>
    /// <item>Various settings, configurations, and framework information</item>
    /// <item>Details about the session</item>
    /// </list>
    /// </note>
    /// </remarks>
    internal class Sess
    {
        /// <summary>The <see cref="RuntimeConfig"> runtime settings</see>.</summary>
        public RuntimeConfig RuntimeSetting { get; set; }

        /// <summary>The <see cref="Framework"> framework components</see>.</summary>
        public Framework.FrameworkConfig FrameworkSetting { get; set; }

        /// <summary>The Tingen Web Service <see cref="TwsConfig"> configuration settings</see>.</summary>
        public TwsConfig TwsSetting { get; set; }

        /// <remarks>Used to create the session folder.</remarks>
        public string SentScriptParameter { get; set; }

        /// <summary>The Avatar OptionObjects associated with the session.</summary>
        public AvatarOptionObject OptionObject { get; set; }

        /// <summary>The folder path that will store session data.</summary>
        public string SessionFolder { get; set; }

        /// <summary>Details about the session.</summary>
        /// <remarks>The running log.</remarks>
        public string RunningLog { get; set; }

        /// <summary>Start a new Tingen Web Service session.</summary>
        /// <param name="sentOptionObject">The <see cref="OptionObject2015"/> sent from Avatar.</param>
        /// <param name="sentScriptParameter">The script parameter sent from Avatar.</param>
        /// <param name="runtimeConfig">The <see cref="RuntimeConfig"> runtime configuration settings</see>.</param>
        /// <param name="frameworkConfig">The <see cref="FrameworkSetting"> framework components</see>.</param>
        /// <returns>A new Tingen Web Service session object.</returns>
        internal static Sess StartSession(OptionObject2015 sentOptionObject, string sentScriptParameter, RuntimeConfig runtimeConfig, Framework.FrameworkConfig frameworkConfig)
        {
            //LogEvent.Primeval("PRELOG-TRACE-Sess-StartSession");

            return new Sess
            {
                RuntimeSetting      = runtimeConfig,
                FrameworkSetting    = frameworkConfig,
                TwsSetting          = TwsConfig.Load(Path.Combine(frameworkConfig.ConfigRoot, "TingenWebService.config")),
                OptionObject        = AvatarOptionObject.Build(sentOptionObject),
                SentScriptParameter = sentScriptParameter,
                SessionFolder       = Path.Combine(frameworkConfig.SessionRoot, runtimeConfig.SessionStartDate, sentOptionObject.OptionUserId, runtimeConfig.SessionStartTime),
                RunningLog          = string.Empty
            };
        }
    }
}