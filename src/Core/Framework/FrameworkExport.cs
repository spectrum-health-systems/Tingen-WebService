// 260804_code
// 260730_documentation

using System.IO;
using TingenWebService.Core.Logger;
using TingenWebService.Core.Trove;
using TingenWebService.Du;

namespace TingenWebService.Core.Framework
{
    internal class FrameworkExport
    {
        /// <summary>Exports all blueprint templates to the specified host directory.</summary>
        /// <param name="blueprintRoot">The root directory where the blueprints will be exported.</param>
        internal static void ExportBlueprints(string blueprintRoot)
        {
            LogEvent.Primeval("PRELOG-TRACE-FrwkMaint-ExportBlueprints");

            foreach (var blueprintFileName in Catalog.BlueprintFileNames())
            {
                var blueprintPath = Path.Combine(blueprintRoot, $"{blueprintFileName}.blueprint");

                if (!File.Exists(blueprintPath))
                {
                    string blueprintContent = null;

                    switch (blueprintFileName)
                    {
                        case "SessLogTxt":
                            blueprintContent = Blueprint.SessLogTxtBP();

                            break;

                        case "SessLogMd":
                            blueprintContent = Blueprint.SessLogMdBP();

                            break;

                        case "SessLogHtml":
                            blueprintContent = Blueprint.SessLogHtmlBP();

                            break;
                    }

                    if (blueprintContent != null)
                    {
                        DuFile.DeadDrop(blueprintPath, blueprintContent);
                    }
                }
            }
        }

        /// <summary>Exports all translation files to the specified host directory.</summary>
        /// <param name="translationRoot">The root directory where the translations will be exported.</param>
        internal static void ExportTranslations(string translationRoot)
        {
            LogEvent.Primeval("PRELOG-TRACE-FrwkMaint-ExportTranslations");

            foreach (var translationFileName in Catalog.TranslationFileNames())
            {
                var translationPath = Path.Combine(translationRoot, $"{translationFileName}.translation");

                string translationContent = null;

                switch (translationFileName)
                {
                    case "FormIdToName":
                        translationContent = Catalog.TranslateFormIdToName();

                        break;
                }

                if (translationContent != null)
                {
                    DuFile.DeadDrop(translationPath, translationContent);
                }
            }
        }

    }
}