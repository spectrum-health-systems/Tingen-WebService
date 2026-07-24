// 260724_code
// 260724_documentation

namespace TingenWebService.Logger
{
    internal class LogEvent
    {
        // [260723]
        /// <summary>Logs a primeval event with the specified name and content.</summary>
        /// <param name="logName">The name of the log.</param>
        /// <param name="logContent">The content of the log.</param>
        internal static void Primeval(string logName, string logContent) => PrimevalLog.Create(logName, logContent);
    }
}