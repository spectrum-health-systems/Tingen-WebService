// 251112_code
// 260520_documentation

using System.Reflection;
using TingenWebService.Core.Avatar;
using TingenWebService.Core.Logger;
using TingenWebService.Core.TingenWsvcSession;

namespace TingenWebService.Module.TngnWsvc
{
    /// <summary>Utility helpers for working with option objects in Tingen Web Service modules.</summary>
    /// <remarks>
    /// Provides lightweight helpers used by module code to export or manipulate the option object payload
    /// for diagnostic or integration purposes.
    /// </remarks>
    internal class OptObjUtility
    {
        /// <include file='AppData/XmlDocumentation/TngnWsvc.xml' path='TngnWsvc/Class[@name="CommonDefinition"]/ExecutingAssembly/*'/>
        public static string ExeAsm { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>Exports the current option object original payload to the configured export folder.</summary>
        /// <remarks>
        /// This method writes the session's original option object using <see cref="AvatarOptionObject.ExportOptObj"/>,
        /// allowing external inspection or archiving of the option object contents.
        /// </remarks>
        /// <param name="wsvcSession">The active Tingen Web Service session containing the option object and folder settings.</param>
        /// <example>
        /// <code>
        /// OptObjUtility.CatchOptionObject(tngnWsvcSession);
        /// </code>
        /// </example>
        internal static void CatchOptionObject(TngnWsvcSession wsvcSession)
        {
            LogEvent.Trace(1, wsvcSession.LogSetting.TraceLogLimit, wsvcSession.Framework.TngnWsvcDataFolder.Session, ExeAsm);

            AvatarOptionObject.ExportOptObj(wsvcSession.OptObj.Original, wsvcSession.Framework.TngnWsvcDataFolder.Export);
        }
    }
}