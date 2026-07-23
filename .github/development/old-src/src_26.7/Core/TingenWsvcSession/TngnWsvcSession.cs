// 251112_code
// 260515_documentation

using System.Collections.Generic;
using System.IO;
using ScriptLinkStandard.Objects;
using TingenWebService.Core.Avatar;
using TingenWebService.Core.Framework;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Maintenance;

namespace TingenWebService.Core.TingenWsvcSession
{
    /// <summary>Represents a single Tingen Web Service session and the state required to process it.</summary>
    /// <remarks>
    /// <para>
    /// <c>TngnWsvcSession</c> aggregates everything a single Avatar request needs in one place: the
    /// per-session runtime values (<see cref="Runtime"/>), the resolved framework folder layout
    /// (<see cref="Framework"/>), the active logging configuration (<see cref="LogSetting"/>), the
    /// inbound and working <c>OptionObject</c> data (<see cref="OptObj"/>), and the script parameter
    /// supplied by Avatar (<see cref="ScriptParameter"/>).
    /// </para>
    /// <para>
    /// A session is created by calling <see cref="Start(OptionObject2015, string, Dictionary{string, string})"/>,
    /// which loads all dependent state, runs the per-session and daily maintenance quick checks, and
    /// parses the script parameter before returning the populated session to the caller.
    /// </para>
    /// </remarks>
    public class TngnWsvcSession
    {
        /// <summary>The per-session runtime values (timestamps, version, mode, Avatar system, and credentials).</summary>
        public TngnWsvcRuntime Runtime { get; set; }

        /// <summary>The resolved Tingen Web Service framework, including data and web folder paths.</summary>
        public TngnWsvcFramework Framework { get; set; }

        /// <summary>The logging configuration (trace and session log limits) for this session.</summary>
        public LogSettings LogSetting { get; set; }

        /// <summary>The original, working, and completed Avatar <c>OptionObject</c> instances for this session.</summary>
        public AvatarOptionObject OptObj { get; set; }

        /// <summary>The parsed Avatar script parameter associated with this session.</summary>
        public AvatarScriptParameter ScriptParameter { get; set; }

        /// <summary>Starts a new Tingen Web Service session and runs the standard per-session bootstrap.</summary>
        /// <param name="origOptObj">The original Avatar <see cref="OptionObject2015"/> received from the calling form.</param>
        /// <param name="origScriptParam">The original script parameter string supplied by Avatar.</param>
        /// <param name="runtimeConfig">The runtime configuration dictionary loaded for the active environment.</param>
        /// <returns>A fully initialized <see cref="TngnWsvcSession"/> ready to be handed to the request pipeline.</returns>
        /// <remarks>
        /// <para>
        /// This entry point performs three steps in order: it builds the session via
        /// <see cref="Load(OptionObject2015, string, Dictionary{string, string})"/>, runs
        /// <see cref="SessionMaintenance.QuickCheck(int, string)"/> and
        /// <see cref="DailyMaintenance.QuickCheck(dynamic)"/> to enforce log retention and folder integrity, and then
        /// parses the script parameter through <see cref="AvatarScriptParameter.Parse(dynamic)"/> so downstream
        /// dispatchers can route on the parsed values.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// var session = TngnWsvcSession.Start(optionObject, scriptParameter, runtimeConfig);
        /// </code>
        /// </example>
        public static TngnWsvcSession Start(OptionObject2015 origOptObj, string origScriptParam, Dictionary<string, string> runtimeConfig)
        {
            TngnWsvcSession tngnWsvcSession = Load(origOptObj, origScriptParam, runtimeConfig);

            var pathOne         = Path.Combine(tngnWsvcSession.Framework.TngnWsvcDataFolder.Config, "pathOne.txt");
            File.WriteAllText(pathOne, "Path One");

            SessionMaintenance.QuickCheck(tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session);

            var pathTwo         = Path.Combine(tngnWsvcSession.Framework.TngnWsvcDataFolder.Config, "pathTwo.txt");
            File.WriteAllText(pathTwo, "Path Two");

            DailyMaintenance.QuickCheck(tngnWsvcSession);

            var pathThree         = Path.Combine(tngnWsvcSession.Framework.TngnWsvcDataFolder.Config, "pathThree.txt");
            File.WriteAllText(pathThree, "Path Three");

            AvatarScriptParameter.Parse(tngnWsvcSession);

            var pathEight            = Path.Combine(tngnWsvcSession.Framework.TngnWsvcDataFolder.Config, "pathEight.txt");
            File.WriteAllText(pathEight, "Path Eight");

            return tngnWsvcSession;
        }

        /// <summary>Builds a <see cref="TngnWsvcSession"/> from the inbound Avatar request and runtime configuration.</summary>
        /// <param name="origOptObj">The original Avatar <see cref="OptionObject2015"/> received from the calling form.</param>
        /// <param name="origScriptParam">The original script parameter string supplied by Avatar.</param>
        /// <param name="runtimeConfig">The runtime configuration dictionary providing version, mode, Avatar system, server paths, log limits, and Netsmart service credentials.</param>
        /// <returns>A populated <see cref="TngnWsvcSession"/> with <see cref="Runtime"/>, <see cref="Framework"/>, <see cref="LogSetting"/>, <see cref="OptObj"/>, and <see cref="ScriptParameter"/> initialized.</returns>
        /// <remarks>
        /// <para>
        /// <see cref="OptObj"/> is constructed with the original <c>OptionObject2015</c> preserved as
        /// <c>Original</c>, a <c>Clone</c> assigned to <c>Worker</c> for safe mutation, and
        /// <c>Completed</c> initialized to <c>null</c>. The session date and time captured by
        /// <see cref="TngnWsvcRuntime.Load(string, string, string, string, string, string)"/> are
        /// reused when resolving framework folder paths so all per-session artifacts share the same
        /// timestamp.
        /// </para>
        /// </remarks>
        internal static TngnWsvcSession Load(OptionObject2015 origOptObj, string origScriptParam, Dictionary<string, string> runtimeConfig)
        {
            TngnWsvcRuntime tngnWsvcRuntime = TngnWsvcRuntime.Load(runtimeConfig["Version"], runtimeConfig["Mode"], runtimeConfig["AvatarSystem"], origOptObj.OptionUserId, runtimeConfig["NtstWsvcUserName"], runtimeConfig["NtstWsvcUserPass"]);

            return new TngnWsvcSession()
            {
                Runtime    = tngnWsvcRuntime,
                Framework  = TngnWsvcFramework.Load(runtimeConfig["AvatarSystem"], tngnWsvcRuntime.AvatarUserId, runtimeConfig["ServerDataPath"], runtimeConfig["ServerWwwPath"], tngnWsvcRuntime.SessionDate, tngnWsvcRuntime.SessionTime),
                LogSetting = LogSettings.Load(runtimeConfig["TraceLogLimit"], runtimeConfig["SessionLogLimit"]),
                OptObj     = new AvatarOptionObject()
                {
                    Original  = origOptObj,
                    Worker    = origOptObj.Clone(),
                    Completed = null
                },
                ScriptParameter = new AvatarScriptParameter()
                {
                    OriginalScriptParameter = origScriptParam
                },
            };
        }
    }
}