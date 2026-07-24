// 260723_code
// 260723_documentation

namespace TingenWebService.Du
{
    public class DuFile
    {
        // [260723]
        /// <summary>Writes the specified content to a file at the given path. If the file already exists, it will be overwritten.</summary>
        /// <param name="filePath">The path of the file to write to.</param>
        /// <param name="fileContent">The content to write to the file.</param>
        /// <example>
        /// <code>
        /// DuFile.DeadDrop("C:\\Tingen_Data\\example.txt", "Hello, World!");
        /// </code>
        /// </example>
        public static void DeadDrop(string filePath, string fileContent = "")
        {
            System.IO.File.WriteAllText(filePath, fileContent);
        }

        // [260725]
        /// <summary>Writes the specified content to a file at the given path. If the file already exists, it will be overwritten.</summary>
        /// <param name="filePath">The path of the file to write to.</param>
        /// <param name="fileContent">The content to write to the file.</param>
        /// <example>
        /// <code>
        /// DuFile.DeadDrop("C:\\Tingen_Data\\example.txt", "Hello, World!");
        /// </code>
        /// </example>
        public static void DeadDropAppend(string filePath, string fileContent = "")
        {
            System.IO.File.AppendAllText(filePath, fileContent);
        }
    }
}