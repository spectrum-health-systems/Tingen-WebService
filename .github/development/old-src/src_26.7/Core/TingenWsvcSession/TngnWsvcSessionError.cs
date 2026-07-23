// 251112_code
// 260515documentation

using System;
using System.Reflection;
using TingenWebService.Core.Avatar;
using TingenWebService.Core.Logger;

namespace TingenWebServiceCore.TingenWsvcSession
{
    /// <summary>Provides error-handling helpers for an active Tingen Web Service session.</summary>
    /// <remarks>
    /// <para>
    /// <c>TngnWsvcSessionError</c> centralizes the way unrecoverable ("hard") errors are reported back
    /// to Avatar. Routing all hard errors through a single helper guarantees that the running log,
    /// the session log, and the response payload returned to the calling Avatar form remain
    /// consistent regardless of where the error originated in the request pipeline.
    /// </para>
    /// </remarks>
    internal class TngnWsvcSessionError
    {
        /// <include file='AppData/XmlDocumentation/TngnWsvc.xml' path='TngnWsvc/Class[@name="CommonDefinition"]/ExecutingAssembly/*'/>
        public static string ExeAsm { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>Records a hard error against the active session and returns it to the Avatar caller.</summary>
        /// <param name="wsvcSession">The active Tingen Web Service session object that exposes the script parameter and Avatar option object.</param>
        /// <param name="errCode">The numeric error code that should be sent back to the Avatar form.</param>
        /// <param name="errMsg">The human-readable error message describing what failed.</param>
        /// <remarks>
        /// <para>
        /// The error message and the original script parameter are appended to the session's running
        /// log via <see cref="SessionLog.AddToRunningLog(dynamic, string, string, string)"/> with the
        /// <c>"HARD ERROR"</c> category, and then
        /// <see cref="AvatarOptionObject.ToReturn(dynamic, int, string)"/> is invoked to deliver the
        /// error payload to the Avatar form. This method does not throw; it is intended to be the
        /// final step before control returns to Avatar.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// TngnWsvcSessionError.HardError(
        ///     wsvcSession: tngnWsvcSession,
        ///     errCode:     3,
        ///     errMsg:      "Unable to resolve the requested Avatar user.");
        /// </code>
        /// </example>
        internal static void HardError(dynamic wsvcSession, int errCode, string errMsg)
        {
            var runningLogContent = errMsg + Environment.NewLine +
                                    $"Parameter = {wsvcSession.ScriptParameter}{Environment.NewLine}";

            SessionLog.AddToRunningLog(wsvcSession, LogComponents.GetCallerInfo(), "HARD ERROR", runningLogContent);
            AvatarOptionObject.ToReturn(wsvcSession, errCode, errMsg);
        }
    }
}