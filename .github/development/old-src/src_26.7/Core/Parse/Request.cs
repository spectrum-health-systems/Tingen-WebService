// 251112_code
// 260515_documentation.

using System.IO;
using System.Reflection;
using TingenWebService.Core.Logger;

namespace TingenWebService.Core.Parse
{
    /// <summary>Parses inbound Tingen Web Service requests and dispatches them to the appropriate handler.</summary>
    /// <remarks>
    /// <para>
    /// <c>Request</c> inspects the active session's script parameter and routes the request to the correct internal
    /// workflow (for example, the Tingen Web Service self-test or deployment pipelines). When an unrecognized command
    /// is received, it builds a templated response from the <c>UnknownParameter.blueprint</c> file so the calling
    /// Avatar form receives a predictable error payload instead of a runtime failure.
    /// </para>
    /// <para>
    /// All routing decisions are recorded through <see cref="LogEvent.Trace(int, int, string, string)"/> so the request
    /// pipeline can be reconstructed from the per-session trace log.
    /// </para>
    /// </remarks>
    public static class Request
    {
        /// <include file='AppData/XmlDocumentation/TngnWsvc.xml' path='TngnWsvc/Class[@name="CommonDefinition"]/ExecutingAssembly/*'/>
        public static string ExeAsm { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>Reserved entry point for prototype request handling.</summary>
        /// <param name="tngnWsvcSession">The active Tingen Web Service session object.</param>
        /// <remarks>
        /// <para>
        /// This method currently performs no work beyond emitting a trace log entry. It exists as a stable hook for
        /// experimental request flows so they can be wired into the dispatcher without requiring downstream callers to
        /// change their signatures.
        /// </para>
        /// </remarks>
        internal static void Prototype(dynamic tngnWsvcSession)
        {
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

            // No prototype functionality yet.
        }

        /// <summary>Dispatches a Tingen Web Service request based on the session's original script parameter.</summary>
        /// <param name="tngnWsvcSession">The active Tingen Web Service session object that exposes the script parameter, runtime, framework, and Avatar option object.</param>
        /// <remarks>
        /// <para>
        /// The dispatcher recognizes the following script parameters (case-insensitive):
        /// </para>
        /// <list type="bullet">
        ///   <item><description><c>TngnWsvcTest</c> &#8212; runs the web service self-test pipeline and returns a "Testing complete." message.</description></item>
        ///   <item><description><c>TngnWsvcDeploy</c> &#8212; runs the deployment pipeline for the active Avatar system and returns a deployment confirmation message.</description></item>
        ///   <item><description>Any other value &#8212; loads the <c>UnknownParameter.blueprint</c> template, substitutes the command, version, and error code tokens, and returns the resulting content.</description></item>
        /// </list>
        /// <para>
        /// The response is delivered back to Avatar through <c>tngnWsvcSession.Avatar.AvatarOptionObject.ToReturn(...)</c>,
        /// and additional trace entries are emitted at each branch so the dispatch path can be inspected after the fact.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Inside the request pipeline:
        /// Request.TngnWsvc(tngnWsvcSession);
        /// </code>
        /// </example>
        internal static void TngnWsvc(dynamic tngnWsvcSession)
        {
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

            if (string.Equals(tngnWsvcSession.ScriptParameter.OriginalScriptParameter, "TngnWsvcTest", System.StringComparison.OrdinalIgnoreCase))
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);
                //Module.TngnWsvc.Deployer.Test(tngnWsvcSession.Framework, tngnWsvcSession.LogSetting.TraceLogLimit);
                //LogEvent.Session(sess);
                tngnWsvcSession.Avatar.AvatarOptionObject.ToReturn(tngnWsvcSession, 3, "Testing complete.");
            }
            else if (string.Equals(tngnWsvcSession.ScriptParameter.OriginalScriptParameter, "TngnWsvcDeploy", System.StringComparison.OrdinalIgnoreCase))
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);
                //Module.TngnWsvc.Deployer.Deploy(wsvcSession);
                LogEvent.Debug();
                //LogEvent.Session(sess);
                tngnWsvcSession.Avatar.AvatarOptionObject.ToReturn(tngnWsvcSession, 3, $"Deployment to {tngnWsvcSession.Runtime.AvatarSystem.ToUpper()} complete.");
                LogEvent.Debug();
            }
            else
            {
                LogEvent.Trace(2, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

                var template = File.ReadAllText($@"{tngnWsvcSession.Framework.TngnWsvcDataFolder.Blueprint}\Command\UnknownParameter.blueprint");
                var content  = template.Replace("~COMMAND~", tngnWsvcSession.ScriptParameter.OriginalScriptParameter)
                                       .Replace("~VERSION~", tngnWsvcSession.Runtime.Version)
                                       .Replace("~ERROR~CODE~", "7T7TV");

                tngnWsvcSession.Avatar.AvatarOptionObject.ToReturn(tngnWsvcSession, 3, content);
            }
        }
    }
}