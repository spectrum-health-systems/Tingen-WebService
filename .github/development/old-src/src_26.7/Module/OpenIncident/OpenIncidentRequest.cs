// 251112_code
// 260520_documentation

using System.Reflection;
using TingenWebService.Core.Avatar;
using TingenWebService.Core.Catalog;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Query;
using TingenWebService.Core.TingenWsvcSession;

namespace TingenWebService.Module.OpenIncident
{
    /// <summary>Processes OpenIncident module request-related actions for the current session.</summary>
    /// <remarks>
    /// Contains request handlers invoked by the OpenIncident event dispatcher. Handlers produce appropriate
    /// Avatar option object responses and may record running/session log entries as needed.
    /// </remarks>
    internal class OpenIncidentRequest
    {
        /// <include file='AppData/XmlDocumentation/TngnWsvc.xml' path='TngnWsvc/Class[@name="CommonDefinition"]/ExecutingAssembly/*'/>
        public static string ExeAsm { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>Handles post-file processing for the OpenIncident module.</summary>
        /// <remarks>
        /// Executed during the post-file script phase. Implementations should validate and perform any actions
        /// required after the option object has been posted.
        /// </remarks>
        /// <param name="tngnWsvcSession">The active Tingen Web Service session.</param>
        /// <param name="openIncidentConfig">The loaded OpenIncident module configuration.</param>
        /// <example>
        /// <code>
        /// OpenIncidentRequest.PostFileEvent(tngnWsvcSession, openIncidentConfig);
        /// </code>
        /// </example>
        internal static void PostFileEvent(TngnWsvcSession tngnWsvcSession, OpenIncidentConfig openIncidentConfig)
        {
            LogEvent.Trace(1, tngnWsvcSession.LogSetting.TraceLogLimit, tngnWsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);
            //Nothing here.
        }
    }
}