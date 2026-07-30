// 260729_code
// 260729_documentation

using System;
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
    /// <item>The <i>original</i> data from Avatar (<see cref="Avatar.AvatarData"/>)</item>
    /// <item>The <i>working</i> (<see cref="AvatarData.WorkerOptObj"/>) and <i>completed</i> data (<see cref="AvatarData.CompleteOptObj"/>) from Avatar</item>
    /// <item>Various settings, configurations, and framework information</item>
    /// <item>Details about the session</item>
    /// </list>
    /// </note>
    /// </remarks>
    internal class Sess
    {
        /// <summary>The <see cref="RuntimeConfig"> runtime settings</see>.</summary>
        public RuntimeConfig RtSetting { get; set; }

        /// <summary>The <see cref="Framework"> framework components</see>.</summary>
        public Framework.FrwkConfig FrwkSetting { get; set; }

        /// <summary>The Tingen Web Service <see cref="TwsConfig"> configuration settings</see>.</summary>
        public TwsConfig TwsSetting { get; set; }
        /// <summary>The current date (yyMMdd).</summary>

        /// <summary>The Avatar data associated with the session.</summary>
        public AvatarData AvatarData { get; set; }

        /// <summary>The folder path that will store session data.</summary>
        public string SessionFolder { get; set; }

        /// <summary>Details about the session.</summary>
        /// <remarks>The running log.</remarks>
        public string RunningLog { get; set; }

        /// <summary>Start a new Tingen Web Service session.</summary>
        /// <param name="sentOptionObject">The <see cref="OptionObject2015"/> sent from Avatar.</param>
        /// <param name="sentScriptParameter">The script parameter sent from Avatar.</param>
        /// <param name="runtimeConfig">The <see cref="RuntimeConfig"> runtime configuration settings</see>.</param>
        /// <param name="frameworkConfig">The <see cref="FrwkSetting"> framework components</see>.</param>
        /// <returns>A new Tingen Web Service session object.</returns>
        internal static Sess StartSession(OptionObject2015 sentOptionObject, string sentScriptParameter, RuntimeConfig runtimeConfig, Framework.FrwkConfig frameworkConfig)
        {
            /* DEVNOTE
             * - Use primeval logs here to debug, since logging functionality has not been initialized yet.
             * - Disable this in production.
             */
            Logger.LogEvent.Primeval($"_{DateTime.Now:yyMMdd-HHmmss-fffffff}-DEBUG-Sess.StartSession");

            return new Sess
            {
                RtSetting     = runtimeConfig,
                FrwkSetting   = frameworkConfig,
                TwsSetting    = TwsConfig.Load(Path.Combine(frameworkConfig.ConfigRoot, "TingenWebService.config")),
                AvatarData    = AvatarData.Build(sentOptionObject, sentScriptParameter),
                SessionFolder = Path.Combine(frameworkConfig.SessionRoot, runtimeConfig.SessionStartDate, sentOptionObject.OptionUserId, runtimeConfig.SessionStartTime),
                RunningLog    = string.Empty
            };
        }
    }
}