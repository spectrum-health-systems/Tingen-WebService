// 260812_code
// 260812_documentation
using System;

namespace TingenWebService.Core.Utility
{
    internal class TimeDuration
    {
        internal static string GetDuration(string start, string startMs, string end, string endMs)
        {
            //LogEvent.Primeval("PRELOG-TRACE-TimeDuration-GetDuration");

            string durationTime = (DateTime.ParseExact(end, "HHmmss", null) - DateTime.ParseExact(start, "HHmmss", null)).ToString(@"hh\:mm\:ss");
            string durationMs   = (DateTime.ParseExact(endMs, "fffffff", null) - DateTime.ParseExact(startMs, "fffffff", null)).ToString("fffffff");

            return $"{durationTime}.{durationMs}";
        }
    }
}