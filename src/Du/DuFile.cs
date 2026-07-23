// 260723_code
// 260723_documentation

namespace TingenWebService.Du
{
    public class DuFile
    {
        // [260723]
        /// <summary>Writes the specified content to a file at the given path. If the file already exists, it will be overwritten.</summary>
        /// <param name="path">The path of the file to write to.</param>
        /// <param name="content">The content to write to the file.</param>
        public static void DeadDrop(string path, string content)
        {
            System.IO.File.WriteAllText(path, content);
        }
    }
}