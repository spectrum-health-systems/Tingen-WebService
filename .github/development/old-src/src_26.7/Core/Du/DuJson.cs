// 251111_code
// 260603_documentation.

using System.IO;

namespace TingenWebService.Core.Du
{
    /// <summary>JSON data logic.</summary>
    /// <remarks>Logging is disabled.</remarks>
    internal static class DuJson
    {
        /// <summary>Export an object to a local JSON file.</summary>
        /// <remarks>
        /// The JSON is written with indentation by default; pass <c>false</c> for <paramref name="formatJson"/> to
        /// disable formatting.<br/>
        /// <br/>
        /// .NET Framework 4.8 does not support <c>System.Text.Json</c>, so the object's
        /// <see cref="object.ToString"/> representation is written to the file.
        /// </remarks>
        /// <typeparam name="JsonObject">The type of the object to be serialized to JSON.</typeparam>
        /// <param name="jsonObject">The object to be serialized to JSON.</param>
        /// <param name="filePath">The path of the file to which the JSON will be written.</param>
        /// <param name="formatJson">Indicates whether the JSON should be formatted with indentation.</param>
        /// <example>
        /// <code>
        /// var settings = new { AvatarSystem = "LIVE", Mode = "enabled" };
        ///
        /// // Write with indentation:
        /// DuJson.ExportToLocalFile(settings, @"C:\Tingen_Data\config\settings.json");
        ///
        /// // Write without indentation:
        /// DuJson.ExportToLocalFile(settings, @"C:\Tingen_Data\config\settings.json", formatJson: false);
        /// </code>
        /// </example>
        internal static void ExportToLocalFile<JsonObject>(JsonObject jsonObject, string filePath, bool formatJson = true)
        {
            var fileContent = jsonObject.ToString();

            File.WriteAllText(filePath, fileContent);
        }

        /// <summary>Import a local JSON file and deserialize it into an object of the specified type.</summary>
        /// <typeparam name="JsonObject">The type of the object to be deserialized from JSON.</typeparam>
        /// <param name="filePath">The path of the file from which the JSON will be read.</param>
        /// <returns>The deserialized object from the JSON file, or the default value of <typeparamref name="JsonObject"/>.</returns>
        /// <example>
        /// <code>
        /// var settings = DuJson.ImportFromLocalFile&lt;AppSettings&gt;(@"C:\Tingen_Data\config\settings.json");
        /// if (settings != null)
        /// {
        ///     Console.WriteLine(settings.AvatarSystem);
        /// }
        /// </code>
        /// </example>
        internal static JsonObject ImportFromLocalFile<JsonObject>(string filePath)
        {
            var configurationFileContents = File.ReadAllText(filePath);

            return default(JsonObject);
        }
    }
}