// 260723_code
// 260723_documentation

namespace TingenWebService.Du
{
    public class DuDirectory
    {
        // [260723]
        /// <summary>Ensures that the specified directory exists. If it does not exist, it will be created.</summary>
        /// <param name="path">The path of the directory to check or create.</param>
        public static void EnsureDirectoryExists(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
        }
    }
}