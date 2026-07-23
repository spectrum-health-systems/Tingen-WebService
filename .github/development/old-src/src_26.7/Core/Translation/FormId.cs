// 251112_code
// 260515_documentation

using System.IO;
using System.Reflection;
using TingenWebService.Core.Logger;

namespace TingenWebService.Core.Translation
{
    /// <summary>Translates Avatar form (option) identifiers into human-readable form names.</summary>
    /// <remarks>
    /// <para>
    /// <c>FormId</c> performs lookups against the <c>form_id-form_name.trans</c> translation file,
    /// which maps Avatar option ids to the form names used throughout Tingen logging and reporting.
    /// Centralizing the lookup here keeps the translation file format (one <c>id=name</c> entry per
    /// line) hidden from callers and ensures that every read is captured in the per-session trace
    /// log.
    /// </para>
    /// </remarks>
    internal static class FormId
    {
        /// <include file='AppData/XmlDocumentation/TngnWsvc.xml' path='TngnWsvc/Class[@name="CommonDefinition"]/ExecutingAssembly/*'/>
        public static string ExeAsm { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>Returns the form name associated with the supplied Avatar option id.</summary>
        /// <param name="optionId">The Avatar option id (form id) to look up.</param>
        /// <param name="translationPath">The absolute path to the folder that contains <c>form_id-form_name.trans</c>.</param>
        /// <param name="sessionFolder">The absolute path to the current Tingen Web Service session folder.</param>
        /// <param name="traceLogLimit">The maximum number of trace log files retained for the active session.</param>
        /// <returns>The form name that matches <paramref name="optionId"/>, or the literal error code <c>"WSVC2491"</c> when no match is found.</returns>
        /// <remarks>
        /// <para>
        /// The translation file is expected to contain one <c>optionId=formName</c> entry per line.
        /// Blank lines are skipped, and the first matching entry wins. Trace entries are emitted on
        /// entry, on every line scanned, and again when a match is returned so that lookup behavior
        /// can be reconstructed from the session trace log.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// string formName = FormId.GetFormName(
        ///     optionId:        "1234",
        ///     translationPath: tngnWsvcSession.Framework.TngnWsvcDataFolder.Translation,
        ///     sessionFolder:   tngnWsvcSession.Framework.TngnWsvcDataFolder.Session,
        ///     traceLogLimit:   tngnWsvcSession.LogSetting.TraceLogLimit);
        /// </code>
        /// </example>
        internal static string GetFormName(string optionId, string translationPath, string sessionFolder, int traceLogLimit)
        {
            LogEvent.Trace(1, traceLogLimit, sessionFolder, ExeAsm);

            foreach (string line in File.ReadAllLines($@"{translationPath}\form_id-form_name.trans"))
            {
                LogEvent.Trace(2, traceLogLimit, sessionFolder, ExeAsm);

                if (!string.IsNullOrWhiteSpace(line) && line.Split('=')[0] == optionId)
                {
                    LogEvent.Trace(3, traceLogLimit, sessionFolder, ExeAsm);

                    return line.Split('=')[1];
                }
            }

            return "WSVC2491";
        }
    }
}