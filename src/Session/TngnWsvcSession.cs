// 260728_code
// 260728_documentation

using System.IO;
using ScriptLinkStandard.Objects;
using TingenWebService.Configuration;
using TingenWebService.Core;

namespace TingenWebService.Session
{
    /// <summary>The Tingen Web Service session object.</summary>
    /// <remarks>
    /// <note type="note" title="About the Tingen Web Service Session">
    /// The <see cref="Session.TngnWsvcSession"/> class contains everything needed for the Tingen Web Service to process
    /// a request, including:
    /// <list type="bullet">
    /// <item>The request (<see cref="AvatarComponent.SentScriptParameter"/>)</item>
    /// <item>The <i>original</i> data from Avatar (<see cref="AvatarComponent.SentOptionObject"/>)</item>
    /// <item>The <i>working</i> (<see cref="AvatarComponent.WorkingOptionObject"/>) and <i>completed</i> data (<see cref="AvatarComponent.CompleteOptionObject"/>) from Avatar</item>
    /// <item>Various settings, configurations, and framework information</item>
    /// <item>Details about the session</item>
    /// </list>
    /// </note>
    /// </remarks>
    internal class TngnWsvcSession
    {
        /// <summary>The <see cref="RuntimeConfig"> runtime settings</see>.</summary>
        public RuntimeConfig RtConfig { get; set; }

        /// <summary>The <see cref="Framework"> framework components</see>.</summary>
        public Framework TwsFramework { get; set; }

        /// <summary>The Tingen Web Service <see cref="TngnWsvcConfig"> configuration settings</see>.</summary>
        public TngnWsvcConfig TwsConfig { get; set; }

        public AvatarData AvatarOptionObjects { get; set; }

        ///// <summary>The <see cref="AvatarComponent.SentOptionObject"> <b>sent</b> option object</see> from Avatar.</summary>
        //public OptionObject2015 SentOptionObject { get; set; }

        ///// <summary> The <see cref="AvatarComponent.WorkerOptionObject"> <b>working</b> option object</see> that is (potentially) modified during the session. </summary>
        //public OptionObject2015 WorkerOptionObject { get; set; }

        ///// <summary>The <see cref="AvatarComponent.CompleteOptionObject"> <b>completed</b> option object</see> that has is ready to be returned to Avatar.</summary>
        //public OptionObject2015 CompleteOptionObject { get; set; }

        /// <summary>The <see cref="AvatarComponent.SentScriptParameter"> script parameter </see> sent from Avatar.</summary>
        public string SentScriptParameter { get; set; }

        /// <summary>The folder path that will store session data.</summary>
        public string SessionFolder { get; set; }

        /// <summary>Details about the session.</summary>
        public string SessionDetails { get; set; }

        /// <summary>Start a new Tingen Web Service session.</summary>
        /// <param name="sentOptionObject"><see cref="AvatarComponent.SentOptionObject"><b>sent</b> option object</see></param>
        /// <param name="sentScriptParameter">The <see cref="AvatarComponent.SentScriptParameter">script parameter </see> sent from Avatar.</param>
        /// <param name="rtConfig">The <see cref="RuntimeConfig"> runtime configuration settings</see>.</param>
        /// <param name="twsFramework">The <see cref="Framework"> framework components</see>.</param>
        /// <returns>A new Tingen Web Service session object.</returns>
        internal static TngnWsvcSession StartSession(OptionObject2015 sentOptionObject, string sentScriptParameter, RuntimeConfig rtConfig, Framework twsFramework)
        {
            /* For debugging prior to logging functionality being initialized.
             * Disable in production.
             */
            //LogEvent.Primeval("StartSession");

            return new TngnWsvcSession()
            {
                RtConfig              = rtConfig,
                TwsFramework          = twsFramework,
                TwsConfig             = TngnWsvcConfig.Load(Path.Combine(twsFramework.ConfigRoot, "TngnWsvc.config")),
                AvatarOptionObjects   = AvatarData.Initialize(sentOptionObject),
                SentScriptParameter   = sentScriptParameter,
                SessionFolder         = Path.Combine(twsFramework.SessionRoot, rtConfig.CurrentDate, sentOptionObject.OptionUserId, rtConfig.CurrentTime),
                SessionDetails        = string.Empty

            };
        }
    }
}