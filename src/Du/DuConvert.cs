// 260723_code
// 260723_documentation

using System.Collections.Generic;

namespace TingenWebService.Du
{
    public class DuConvert
    {
        // [260723]
        /// <summary>Converts an object to an array of strings.</summary>
        /// <param name="obj">The object to convert. It should be an IEnumerable of objects.</param>
        /// <returns>An array of strings representing the object's elements.</returns>
        public static string[] ObjectToStringArray(object obj)
        {
            var stringList = new List<string>();

            foreach (var item in (IEnumerable<object>)obj)
            {
                var str = item?.ToString() ?? string.Empty;
                stringList.Add(str);
            }

            return stringList.ToArray();
        }
    }
}