// 251112_code
// 260515_documentation.

using System;
using System.Reflection;
using ScriptLinkStandard.Objects;
using TingenWebService.Core.Logger;

namespace TingenWebServiceCore.Operation
{
    /// <summary>Provides helper operations for inspecting and comparing Avatar field values.</summary>
    /// <remarks>
    /// <para>
    /// <c>AvatarField</c> centralizes the small, frequently-reused routines that work with individual Avatar fields
    /// exposed through a <see cref="OptionObject"/>. The helpers normalize comparisons, retrieve field values by
    /// identifier, and report whether a field is empty so that calling code does not have to repeat null/whitespace
    /// checks or case-sensitivity logic.
    /// </para>
    /// <para>
    /// Every public entry point emits a trace log entry through <see cref="LogEvent.Trace(int, int, string, string)"/>
    /// so that field-level operations can be correlated with the originating Tingen Web Service session.
    /// </para>
    /// </remarks>
    internal static class AvatarField
    {
        /// <include file='AppData/XmlDocumentation/TngnWsvc.xml' path='TngnWsvc/Class[@name="CommonDefinition"]/ExecutingAssembly/*'/>
        public static string ExeAsm { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>Determines whether two Avatar string field values are equal using a case-insensitive comparison.</summary>
        /// <param name="field01">The first Avatar field value to compare.</param>
        /// <param name="field02">The second Avatar field value to compare.</param>
        /// <param name="traceLogLimit">The maximum number of trace log files retained for the active session.</param>
        /// <param name="sessionFolder">The absolute path to the current Tingen Web Service session folder.</param>
        /// <returns><c>true</c> if the two values are equal ignoring case; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// <para>
        /// The comparison uses <see cref="StringComparison.OrdinalIgnoreCase"/>, which matches Avatar's 
        /// case-insensitive treatment of most character-based field values without incurring the cost of
        /// culture-sensitive comparisons.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// bool match = AvatarField.AreEqual("Yes", "yes", traceLogLimit, sessionFolder);
        /// // match == true
        /// </code>
        /// </example>
        internal static bool AreEqual(string field01, string field02, int traceLogLimit, string sessionFolder)
        {
            LogEvent.Trace(1, traceLogLimit, sessionFolder, ExeAsm);

            return string.Equals(field01, field02, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>Determines whether two Avatar integer field values are equal.</summary>
        /// <param name="field01">The first Avatar field value to compare.</param>
        /// <param name="field02">The second Avatar field value to compare.</param>
        /// <param name="traceLogLimit">The maximum number of trace log files retained for the active session.</param>
        /// <param name="sessionFolder">The absolute path to the current Tingen Web Service session folder.</param>
        /// <returns><c>true</c> if the two values are equal; otherwise, <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// bool match = AvatarField.AreEqual(42, 42, traceLogLimit, sessionFolder);
        /// // match == true
        /// </code>
        /// </example>
        internal static bool AreEqual(int field01, int field02, int traceLogLimit, string sessionFolder)
        {
            LogEvent.Trace(1, traceLogLimit, sessionFolder, ExeAsm);

            return field01 == field02;
        }

        /// <summary>Retrieves the value of a single Avatar field from the supplied <see cref="OptionObject"/>.</summary>
        /// <param name="optObj">The Avatar <see cref="OptionObject"/> that contains the field.</param>
        /// <param name="fieldId">The Avatar field identifier (FieldNumber) whose value should be returned.</param>
        /// <param name="traceLogLimit">The maximum number of trace log files retained for the active session.</param>
        /// <param name="sessionFolder">The absolute path to the current Tingen Web Service session folder.</param>
        /// <returns>The value of the requested field as returned by <see cref="OptionObject.GetFieldValue(string)"/>.</returns>
        /// <remarks>
        /// <para>
        /// This is a thin wrapper over <see cref="OptionObject.GetFieldValue(string)"/> that adds session
        /// trace logging so field reads can be diagnosed through the standard Tingen log pipeline.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// string firstName = AvatarField.GetValue(optionObject, "12345", traceLogLimit, sessionFolder);
        /// </code>
        /// </example>
        internal static string GetValue(OptionObject optObj, string fieldId, int traceLogLimit, string sessionFolder)
        {
            LogEvent.Trace(1, traceLogLimit, sessionFolder, ExeAsm);

            return optObj.GetFieldValue(fieldId);
        }

        /// <summary>Reports whether an Avatar field value is empty as a lowercase Avatar-friendly string.</summary>
        /// <param name="fieldValue">The Avatar field value to test.</param>
        /// <param name="traceLogLimit">The maximum number of trace log files retained for the active session.</param>
        /// <param name="sessionFolder">The absolute path to the current Tingen Web Service session folder.</param>
        /// <returns>The literal string <c>"true"</c> when <paramref name="fieldValue"/> is null, empty, or whitespace; otherwise <c>"false"</c>.</returns>
        /// <remarks>
        /// <para>
        /// Avatar scripts typically expect boolean-style answers as the lowercase strings <c>"true"</c>
        /// and <c>"false"</c> rather than .NET <see cref="bool"/> values. This helper performs the
        /// null/empty/whitespace check and returns the value in the form Avatar expects.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// string isEmpty = AvatarField.IsEmpty(optionObject.GetFieldValue("12345"), traceLogLimit, sessionFolder);
        /// // isEmpty == "true" when the field has no value
        /// </code>
        /// </example>
        internal static string IsEmpty(string fieldValue, int traceLogLimit, string sessionFolder)
        {
            LogEvent.Trace(1, traceLogLimit, sessionFolder, ExeAsm);

            return string.IsNullOrWhiteSpace(fieldValue)
                ? "true"
                : "false";
        }
    }
}