// 260729_code
// 260729_documentation

using System.IO;

namespace TingenWebService.Core.Logger
{
    internal class LoggerMaintenance
    {

        internal static void ResetSyslogs(string sysLogPath)
        {
            // TODO - Clean this up, there is a better way to do this.

            if (File.Exists(Path.Combine(sysLogPath, "Configuration.current")))
            {
                File.Delete(Path.Combine(sysLogPath, "Configuration.current"));
            }

            if (File.Exists(Path.Combine(sysLogPath, "Framework.current")))
            {
                File.Delete(Path.Combine(sysLogPath, "Framework.current"));
            }

            if (File.Exists(Path.Combine(sysLogPath, "Runtime.current")))
            {
                File.Delete(Path.Combine(sysLogPath, "Runtime.current"));
            }
        }
    }
}