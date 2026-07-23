// 251112_code
// 260515_documentation.

using System;

namespace TingenWebService.Core.Logger
{
    /// <summary>Represents the logging configuration values used by the Tingen Web Service.</summary>
    public class LogSettings
    {
        /// <summary>Gets or sets the maximum trace level that should be written to the trace log.</summary>
        /// <value>An integer trace level limit; trace entries above this level are skipped.</value>
        public int TraceLogLimit { get; set; }

        /// <summary>Gets or sets the maximum number of session log entries that should be retained.</summary>
        /// <value>An integer that controls the session log size limit.</value>
        public int SessionLogLimit { get; set; }

        /// <summary>Gets or sets the millisecond delay applied between log writes.</summary>
        /// <value>An integer number of milliseconds to wait between writes.</value>
        public int LogMsec { get; set; }

        /// <summary>Creates a populated <see cref="LogSettings"/> instance from string configuration values.</summary>
        /// <remarks>
        /// Parses <paramref name="traceLogLimit"/> and <paramref name="sessionLogLimit"/> as integers and assigns them
        /// to the corresponding properties.
        /// </remarks>
        /// <param name="traceLogLimit">The trace log limit, as a string, parsed into <see cref="TraceLogLimit"/>.</param>
        /// <param name="sessionLogLimit">The session log limit, as a string, parsed into <see cref="SessionLogLimit"/>.</param>
        /// <returns>A new <see cref="LogSettings"/> instance with the parsed limit values.</returns>
        /// <example>
        /// <code>
        /// var logSettings = LogSettings.Load(traceLogLimit: "3", sessionLogLimit: "100");
        /// Console.WriteLine(logSettings.TraceLogLimit);   // 3
        /// Console.WriteLine(logSettings.SessionLogLimit); // 100
        /// </code>
        /// </example>
        public static LogSettings Load(string traceLogLimit, string sessionLogLimit)
        {
            return new LogSettings
            {
                TraceLogLimit   = Convert.ToInt32(traceLogLimit),
                SessionLogLimit = Convert.ToInt32(sessionLogLimit),
            };
        }
    }
}