// 260805_code
// 260805_documentation

namespace TingenWebService.Core.Framework
{
    internal static class FrameworkExport
    {
        ///// <summary>Exports all blueprint templates to the specified host directory.</summary>
        ///// <param name="blueprintRoot">The root directory where the blueprints will be exported.</param>
        //internal static void ExportBlueprints(string blueprintRoot)
        //{
        //    //LogEvent.Primeval("PRELOG-TRACE-FrwkMaint-ExportBlueprints");

        //    foreach (var blueprintFileName in Catalog.BlueprintFileNames())
        //    {
        //        var blueprintPath = Path.Combine(blueprintRoot, $"{blueprintFileName}.blueprint");

        //        if (!File.Exists(blueprintPath))
        //        {
        //            string blueprintContent = null;

        //            switch (blueprintFileName)
        //            {
        //                case "SessLogTxt":
        //                    blueprintContent = Blueprint.SessLogTxt();

        //                    break;

        //                case "SessLogMd":
        //                    blueprintContent = Blueprint.SessLogMd();

        //                    break;

        //                    //case "SessLogHtml":
        //                    //    blueprintContent = Blueprint.SessLogHtml();

        //                    //    break;
        //            }

        //            if (blueprintContent != null)
        //            {
        //                DuFile.DeadDrop(blueprintPath, blueprintContent);
        //            }
        //        }
        //    }
        //}

        ///////// <summary>Exports all translation files to the specified host directory.</summary>
        ///////// <param name="translationRoot">The root directory where the translations will be exported.</param>
        //////internal static void ExportTranslations(string translationRoot)
        //////{
        //////    //LogEvent.Primeval("PRELOG-TRACE-FrwkMaint-ExportTranslations");

        //////    foreach (var translationFileName in Catalog.TranslationFileNames())
        //////    {
        //////        var translationPath = Path.Combine(translationRoot, $"{translationFileName}.translation");

        //////        Dictionary<string, string> translationContent = null;

        //////        switch (translationFileName)
        //////        {
        //////            case "FormIdToName":
        //////                translationContent = new Translation.FormId().ToFormName;

        //////                break;
        //////        }

        //////        if (translationContent != null)
        //////        {
        //////            DuJson.ExportFile(translationContent, translationPath);
        //////        }
        //////    }
        //////}
    }
}