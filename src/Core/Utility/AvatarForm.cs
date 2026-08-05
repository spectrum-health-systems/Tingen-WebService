// 260805_code
// 260805_documentation

using System.IO;
using TingenWebService.Core.Trove;
using TingenWebService.Du;

namespace TingenWebService.Core.Utility
{
    public class AvatarForm
    {
        /// <summary>
        /// Gets the form name from the translation table based on the original option ID sent by Avatar.
        /// </summary>
        /// <param name="tngnWsvcSession">
        /// The session object containing the original option ID and translation table.
        /// </param>
        /// <remarks>
        /// This translates the FormId to the corresponding FormName using the translation table located at <c>
        /// FormIdToName.translation</c> in the translation path.
        /// </remarks>
        /// <returns>The name of the form corresponding to the original option ID.</returns>
        /// <example>
        /// <code>
        /// var formName = AvatarScriptParameter.GetFormName(tngnWsvcSession);
        /// Console.WriteLine($"Resolved form name: {formName}");
        /// </code>
        /// </example>
        internal static string GetFormName(string translationPath, string formId)
        {
            //LogEvent.Primeval("PRELOG-TRACE-TingenWebService-GetFormName");

            var path  = Path.Combine(translationPath, "FormIdToName.translation");
            var forms = DuJson.ImportFile<Translation.FormId>(path);

            return forms.ToFormName[formId];
        }
    }
}