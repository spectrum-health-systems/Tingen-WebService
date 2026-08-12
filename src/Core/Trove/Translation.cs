// 260806_code
// 260806_documentation

using System.Collections.Generic;
using System.IO;
using TingenWebService.Du;

namespace TingenWebService.Core.Trove
{
    /// <summary>Represents a JSON object used within the Tingen Web Service.</summary>
    internal class Translation
    {
        /// <summary>Represents a form ID mapping used within the JSON object.</summary>
        internal class FormId
        {
            /// <summary>Gets a dictionary mapping form IDs to form names.</summary>
            public Dictionary<string, string> ToFormName = new Dictionary<string, string>
            {
                 { "INCIDENT1", "OpenIncident" }
            };
        }

        /// <summary>Exports all translation files to the specified host directory.</summary>
        /// <param name="translationRoot">The root directory where the translations will be exported.</param>
        internal static void ExportTranslations(string translationRoot)
        {
            Logger.//LogEvent.Primeval("PRELOG-TRACE-FrwkMaint-ExportTranslations");

            foreach (var translationFileName in Catalog.TranslationNames())
            {
                var translationPath = Path.Combine(translationRoot, $"{translationFileName}.translation");

                Dictionary<string, string> translationContent = null;

                switch (translationFileName)
                {
                    case "FormIdToName":
                        translationContent = new Translation.FormId().ToFormName;

                        break;
                }

                if (translationContent != null)
                {
                    DuJson.ExportFile(translationContent, translationPath);
                }
            }
        }


    }
}