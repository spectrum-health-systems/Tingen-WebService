// 260729_code
// 260730_documentation

using System.Collections.Generic;

namespace TingenWebService.Core.Trove
{
    /// <summary>Internal preset collections.</summary>
    /// <remarks><include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="Catalog"]/About/*'/></remarks>
    internal static class Catalog
    {
        /// <summary>Build the list of required framework folders.</summary>
        /// <param name="twsFramework">The framework object that contains the folder list.</param>
        /// <returns>A string array of the required framework folders.</returns>
        internal static string[] RequiredFrameworkFolders(Framework.FrwkConfig twsFramework) =>
            new string[]
            {
                twsFramework.AvatarGeneratedDataRoot,
                twsFramework.ConfigRoot,
                twsFramework.ExportRoot,
                twsFramework.ImportRoot,
                twsFramework.SysLogRoot,
                twsFramework.BlueprintRoot,
                twsFramework.SessionRoot,
                twsFramework.TranslationTableRoot
            };

        /// <summary>Build the list of system log file names.</summary>
        /// <returns>A list of system log file names.</returns>
        internal static List<string> SystemLogFileNames() =>
            new List<string>
            {
                "Configuration",
                "Framework",
                "Runtime",
                "OpenIncident"
            };

        // TODO - Add ErrorLogMd and ErrorLogHtml. Also rename ErrorLogTxt to ErrLogTxt
        /// <summary>Build the list of blueprint file names.</summary>
        /// <returns>A list of blueprint file names.</returns>
        internal static List<string> BlueprintFileNames() =>
            new List<string>
            {
                "ErrorLogTxt",
                "SessLogTxt",
                "SessLogMd",
                "SessLogHtml"
            };

        // TODO - Prob belongs in Redprints.cs
        /// <summary>_Gets the translation for a form ID to its corresponding form name.</summary>
        /// <returns>A string representing the form ID to form name translation.</returns>
        internal static string FormIdToFormNameTranslation() =>
            "INCIDENT1=OpenIncident";
    }
}