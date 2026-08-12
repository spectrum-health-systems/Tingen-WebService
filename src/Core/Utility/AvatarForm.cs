// 260812_code
// 260812_documentation
using System;
using System.IO;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Session;
using TingenWebService.Core.Trove;
using TingenWebService.Du;

namespace TingenWebService.Core.Utility
{
    public class AvatarForm
    {
        /// <summary>Gets the form name from the translation table based on the original option ID sent by Avatar.</summary>
        /// <param name="translationPath">The path to the translation files.</param>
        /// <param name="formId">The original option ID sent by Avatar.</param>
        /// <param name="trc">The tracer object for logging.</param>
        /// <remarks>
        /// This translates the FormId to the corresponding FormName using the translation table located at <c>FormIdToName.translation</c> in the translation path.
        /// </remarks>
        /// <returns>The name of the form corresponding to the original option ID.</returns>
        /// <example>
        /// <code>
        /// var formName = AvatarScriptParameter.GetFormName(tngnWsvcSession);
        /// Console.WriteLine($"Resolved form name: {formName}");
        /// </code>
        /// </example>
        internal static string GetTranslatedName(string translationPath, string formId, Tracer trc)
        {
            LogEvent.Trace(1, trc.Lmt, trc.Fld);

            try
            {
                LogEvent.Trace(3, trc.Lmt, trc.Fld);

                var path  = Path.Combine(translationPath, "FormIdToName.translation");
                var forms = DuJson.ImportFile<Translation.FormId>(path);

                return forms.ToFormName[formId];
            }
            catch (Exception ex)
            {
                LogEvent.Trace(3, trc.Lmt, trc.Fld);

                LogEvent.Primeval("ERR1000-FailedToTranslateFormId", ErrorMessage.ERR1000(ex.Message));

                return string.Empty;
            }
        }
    }
}