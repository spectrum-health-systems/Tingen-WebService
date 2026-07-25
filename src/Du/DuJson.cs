// 260723_code
// 260723_documentation


using System.IO;
using System.Text.Json;

namespace TingenWebService.Du
{
    public class DuJson
    {
        // [094941]
        /// <summary> Exports a JSON object to a file.</summary>
        /// <typeparam name="T">The type of the JSON object.</typeparam>
        /// <param name="jsonObject">The JSON object to export.</param>
        /// <param name="filePath">The file path to export the JSON object to.</param>
        /// <param name="prettyJson">Determines if the JSON data is formatted.</param>
        /// <example>
        /// To export a nicely formatted JSON object to a file:
        /// <code>
        /// var myObject = new MyObject();
        /// DuJson.ExportFile(myObject, "path/to/file.json");
        /// </code>
        /// To export a compact JSON object to a file:
        /// <code>
        /// var myObject = new MyObject();
        /// DuJson.ExportFile(myObject, "path/to/file.json", false);
        /// </code>
        /// </example>
        public static void ExportFile<JsonObject>(JsonObject jsonObject, string filePath, bool prettyJson = true)
        {
            // TODO - There is a better way to do this.
            var jsonFormat = prettyJson
                ? new JsonSerializerOptions { WriteIndented = true }
                : new JsonSerializerOptions { WriteIndented = false };

            var fileContent = JsonSerializer.Serialize(jsonObject, jsonFormat);

            File.WriteAllText(filePath, fileContent);
        }

        // [094941]
        /// <summary>Imports a JSON object from a file.</summary>
        /// <typeparam name="JsonObject">The type of the JSON object.</typeparam>
        /// <param name="filePath">The file path to import the JSON object from.</param>
        /// <example>
        /// To import a JSON object from a file:
        /// <code>
        /// var myObject = DuJson.ImportFile<MyObject>("path/to/file.json");
        /// </code>
        /// </example>
        /// <returns>The imported JSON object.</returns>
        public static JsonObject ImportFile<JsonObject>(string filePath)
        {
            var fileContents = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<JsonObject>(fileContents);
        }
    }
}