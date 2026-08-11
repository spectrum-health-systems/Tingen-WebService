// 260807_code
// 260806_documentation

using System.IO;
using ScriptLinkStandard.Objects;
using TingenWebService.Core.Avatar;
using TingenWebService.Module.OpenIncident;

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
        /// <summary>The <see cref="Core.RuntimeSetting"> runtime settings</see>.</summary>
        public RuntimeSetting RuntimeSetting { get; set; }

        /// <summary>The <see cref="Framework"> framework components</see>.</summary>
        public Framework.FrameworkSetting FrameworkSetting { get; set; }

        /// <summary>The Tingen Web Service <see cref="Core.AppSetting"> configuration settings</see>.</summary>
        public AppSetting AppSetting { get; set; }

        public ModuleSetting ModuleSetting { get; set; }

        /// <remarks>Used to create the session folder.</remarks>
        public string SentScriptParameter { get; set; }

        /// <summary>The Avatar OptionObjects associated with the session.</summary>
        public AvatarOptionObject OptionObject { get; set; }

        /// <summary>The tracer information for the session.</summary>
        public Tracer Trc { get; set; }

        /// <summary>The folder path that will store session data.</summary>
        public string SessionFolder { get; set; }

        /// <summary>Details about the session.</summary>
        /// <remarks>The running log.</remarks>
        public string RunningLog { get; set; }

        /// <summary>Details about the session.</summary>
        /// <remarks>The running detail.</remarks>
        public string RunningDetail { get; set; }

        /// <summary>Start a new Tingen Web Service session.</summary>
        /// <param name="sentOptionObject">The <see cref="OptionObject2015"/> sent from Avatar.</param>
        /// <param name="sentScriptParameter">The script parameter sent from Avatar.</param>
        /// <param name="runtimeSetting">The <see cref="Core.RuntimeSetting"> runtime configuration settings</see>.</param>
        /// <param name="frameworkSetting">The <see cref="FrameworkSetting"> framework components</see>.</param>
        /// <returns>A new Tingen Web Service session object.</returns>
        internal static Sess StartSession(OptionObject2015 sentOptionObject, string sentScriptParameter, RuntimeSetting runtimeSetting, Framework.FrameworkSetting frameworkSetting)
        {
            Logger.LogEvent.Primeval("PRELOG-TRACE-Sess-StartSession");

            AppSetting appSetting = AppSetting.Load(Path.Combine(frameworkSetting.ConfigRoot, "TingenWebService.config"));
            string sessionFolder  = Path.Combine(frameworkSetting.SessionRoot, sentOptionObject.OptionUserId, runtimeSetting.CurrentDate, runtimeSetting.CurrentTime);

            var s = new Sess();
            Logger.LogEvent.Primeval("PRELOG-TRACE-Sess1");
            s.RuntimeSetting = runtimeSetting;
            Logger.LogEvent.Primeval("PRELOG-TRACE-Sess2");
            s.FrameworkSetting = frameworkSetting;
            Logger.LogEvent.Primeval("PRELOG-TRACE-Sess3");
            s.AppSetting = appSetting;
            Logger.LogEvent.Primeval("PRELOG-TRACE-Sess4");
            s.OptionObject = AvatarOptionObject.Build(sentOptionObject);
            Logger.LogEvent.Primeval("PRELOG-TRACE-Sess5");
            s.SentScriptParameter = sentScriptParameter;
            Logger.LogEvent.Primeval("PRELOG-TRACE-Sess6");
            s.Trc = Tracer.Build(appSetting.TraceLimit, sessionFolder);
            Logger.LogEvent.Primeval("PRELOG-TRACE-Sess7");
            s.SessionFolder = sessionFolder;
            Logger.LogEvent.Primeval("PRELOG-TRACE-Sess8");
            s.RunningLog = string.Empty;
            Logger.LogEvent.Primeval("PRELOG-TRACE-Sess9");
            s.RunningDetail = string.Empty;
            Logger.LogEvent.Primeval("PRELOG-TRACE-Sess10");



            return s;

            //return new Sess
            //{
            //    RuntimeSetting      = runtimeSetting,
            //    FrameworkSetting    = frameworkSetting,
            //    AppSetting          = appSetting,
            //    OptionObject        = AvatarOptionObject.Build(sentOptionObject),
            //    SentScriptParameter = sentScriptParameter,
            //    Trc                 = Tracer.Build(appSetting.TraceLimit, sessionFolder),
            //    SessionFolder       = sessionFolder,
            //    RunningLog          = string.Empty,
            //    RunningDetail       = string.Empty,
            //};
        }
    }

    internal class Tracer
    {
        public int Lmt { get; set; }
        public string Fld { get; set; }

        public static Tracer Build(int traceLimit, string sessionFolder)
        {
            return new Tracer()
            {
                Lmt   = traceLimit,
                Fld = sessionFolder
            };
        }
    }

    public class ModuleSetting
    {
        public OpenIncidentSetting OpenIncidentConfig { get; set; }


    }

}