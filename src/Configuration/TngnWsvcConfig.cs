// 260724_code
// 260724_documentation

namespace TingenWebService.Configuration
{
    internal class TngnWsvcConfig
    {
        /// <summary>Tingen Web Service mode</summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>enabled</item>
        /// <item>disabled</item>
        /// <item>passthrough</item>
        /// </list>
        /// </remarks>
        public string Mode { get; set; }

        /// <summary>Trace log level.</summary>
        public string TraceLogLevel { get; set; }

        /// <summary>Log delay.</summary>
        public string LogDelay { get; set; }

        /// <summary>Email address the web services uses.</summary>
        public string EmailAddress { get; set; }

        /// <summary>Email password the web services uses.</summary>
        public string EmailPassword { get; set; }

        /// <summary>The NTST web service username.</summary>
        public string NtstWsvcUserName { get; set; }

        /// <summary>The NTST web service password.</summary>
        public string NtstWsvcPassword { get; set; }

        //internal static TngnWsvcConfig Load()
        //{
        //}
    }
}