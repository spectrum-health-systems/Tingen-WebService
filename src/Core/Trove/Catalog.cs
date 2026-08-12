// 260812_code
// 260812_documentation

using System.Collections.Generic;
using TingenWebService.Core.Framework;

namespace TingenWebService.Core.Trove
{
    /// <summary>Internal preset collections.</summary>
    /// <remarks><include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="Trove"]/AboutCatalogs/*'/></remarks>
    internal static class Catalog
    {
        /// <summary>Build the list of required framework folders.</summary>
        /// <param name="frameworkSetting">The framework object that contains the folder list.</param>
        /// <returns>A string array of the required framework folders.</returns>
        internal static string[] RequiredFolders(FrameworkSetting frameworkSetting) =>
            new string[]
            {
                frameworkSetting.AvatarGeneratedDataRoot,
                frameworkSetting.ConfigRoot,
                frameworkSetting.ExportRoot,
                frameworkSetting.ImportRoot,
                frameworkSetting.SysLogRoot,
                frameworkSetting.BlueprintRoot,
                frameworkSetting.SessionRoot,
                frameworkSetting.TranslationRoot
            };

        /// <summary>Build the list of system logs that are created during the framework verification process.</summary>
        /// <returns>A list of system log file names.</returns>
        internal static List<string> PreSystemLogFileNames() =>
            new List<string>
            {
                "Runtime",
                "Framework",
            };

        /// <summary>Build the list of system log file names.</summary>
        /// <returns>A list of system log file names.</returns>
        internal static List<string> PostSystemLogFileNames() =>
            new List<string>
            {
                "Configuration",
                "OpenIncident"
            };

        // TODO - Add ErrorLogMd and ErrorLogHtml. Also rename ErrorLogTxt to ErrLogTxt
        /// <summary>Build the list of blueprint file names.</summary>
        /// <returns>A list of blueprint file names.</returns>
        internal static List<string> BlueprintNames() =>
            new List<string>
            {
                "ErrorLogTxt",
                "SessLogTxt",
                "SessLogMd"
            };

        internal static List<string> TranslationNames() =>
            new List<string>
            {
                "FormIdToName"
            };

        ////TODO - Prob belongs in Redprints.cs
        ///// <summary>_Gets the translation for a form ID to its corresponding form name.</summary>
        ///// <returns>A string representing the form ID to form name translation.</returns>
        ////internal static string TranslateFormIdToName() =>
        ////    "INCIDENT1=OpenIncident";

        internal static string TranslationFilesExported() =>
            $"[Translation files built]{System.Environment.NewLine}";
    }
}