// 260724_code
// 260724_documentation

using TingenWebService.Core;

namespace TingenWebService.Trove
{
    /// <summary>Provides catalog information for the Tingen Web Service.</summary>
    /// <remarks>
    /// Preset collections.
    /// </remarks>
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

        internal static string FormIdToFormNameTranslation() =>
            "INCIDENT1=OpenIncident";
    }
}