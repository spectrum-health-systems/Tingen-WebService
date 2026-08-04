// 260804_code
// 2260804_documentation

using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TingenWebService.Du
{
    /// <summary>Utilities for JSON operations.</summary>
    public class DuJson
    {
        // [260723]
        /// <summary>Exports a JSON object to a file.</summary>
        /// <typeparam name="JsonObject">The type of the JSON object.</typeparam>
        /// <param name="jsonObject">The JSON object to export.</param>
        /// <param name="filePath">The file path to export the JSON object to.</param>
        /// <param name="prettyJson">Determines if the JSON data is formatted.</param>
        /// <example>
        /// <code>
        /// var myObject = new MyObject();
        /// DuJson.ExportFile&lt;MyObject&gt;(myObject, @"C:\Path\to\file.json"); // formatted
        /// DuJson.ExportFile&lt;MyObject&gt;(myObject, @"C:\Path\to\file.json", false); //unformatted
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

        // [260723]
        /// <summary>Imports a JSON object from a file.</summary>
        /// <typeparam name="JsonObject">The type of the JSON object.</typeparam>
        /// <param name="filePath">The file path to import the JSON object from.</param>
        /// <returns>The imported JSON object.</returns>
        /// <example>
        /// <code>
        /// var myObject = DuJson.ImportFile&lt;MyObject&gt;(@"C:\Path\to\file.json");
        /// </code>
        /// </example>
        public static JsonObject ImportFile<JsonObject>(string filePath)
        {
            var fileContents = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<JsonObject>(fileContents);
        }

        // [260804]
        /// <summary>Converts a dictionary to a JSON string.</summary>
        /// <param name="translationDictionary">The dictionary to convert.</param>
        /// <returns>The JSON string representation of the dictionary.</returns>
        /// <example>
        /// <code>
        /// var jsonString = DuJson.ConvertDictionary(dictionary);
        /// </code>
        /// </example>
        public static string ConvertDictionary(Dictionary<string, string> translationDictionary)
        {
            return JsonSerializer.Serialize(translationDictionary);
        }
    }
}