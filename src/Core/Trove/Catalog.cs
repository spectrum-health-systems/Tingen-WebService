// 260729_code
// 260729_documentation

using System.Collections.Generic;

namespace TingenWebService.Core.Trove
{
    /// <summary>Preset collections and components.</summary>
    /// <remarks>
    /// <note type="note" title="About catalogs">
    /// Catalogs are preset <b>collections</b> that cannot be modified by the user, and are used to format data in a
    /// consistent manner.
    /// </note>
    /// </remarks>
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

        internal static List<string> SystemLogFileNames() =>
            new List<string>
            {
                "Configuration",
                "Framework",
                "Runtime"
            };

        internal static List<string> BlueprintFileNames() =>
            new List<string>
            {
                "ErrorLogTxt",
                "SessLogTxt",
                "SessLogMd",
                "SessLogHtml"
            };

        // TODO - prob belongs in Redprints.cs
        /// <summary>_Gets the translation for a form ID to its corresponding form name.</summary>
        /// <returns>A string representing the form ID to form name translation.</returns>
        internal static string FormIdToFormNameTranslation() =>
            "INCIDENT1=OpenIncident";
    }
}