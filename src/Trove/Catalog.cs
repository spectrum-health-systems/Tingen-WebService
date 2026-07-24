// 260724_code
// 260724_documentation

using TingenWebService.Core;

namespace TingenWebService.Trove
{
    /// <summary>Provides catalog information for the Tingen Web Service.</summary>
    /// <remarks>Catalogs are preset collections that cannot be modified.</remarks>
    internal class Catalog
    {
        /// <summary>Gets all the framework paths.</summary>
        /// <param name="twsFramework">The framework instance containing the paths.</param>
        /// <returns>An array of all framework paths.</returns>
        internal static string[] FrameworkPathList(Framework twsFramework) =>
            new string[]
            {
                twsFramework.AvatarGeneratedDataRoot,
                twsFramework.AppDataRoot,
                twsFramework.ConfigRoot,
                twsFramework.ExportRoot,
                twsFramework.HistoryRoot,
                twsFramework.ImportRoot,
                twsFramework.LogRoot,
                twsFramework.BlueprintRoot,
                twsFramework.EpistleRoot,
                twsFramework.SessionRoot,
                twsFramework.TranslationTableRoot
            };

        /// <summary>Gets the translation for a form ID to its corresponding form name.</summary>
        /// <returns>A string representing the form ID to form name translation.</returns>
        internal static string FormIdToFormNameTranslation() =>
            "INCIDENT1=OpenIncident";
    }
}