// 260724_code
// 260724_documentation

namespace TingenWebService.Configuration
{
    internal class TngnWsvcConfig
    {
        public string Mode { get; set; }

        public string TraceLogLevel { get; set; }

        public string LogDelay { get; set; }

        public string EmailAddress { get; set; }

        public string EmailPassword { get; set; }

        public string NtstWsvcUserName { get; set; }

        public string NtstWsvcPassword { get; set; }

        public string CriticalErrorMessage { get; set; } //TODO - Elsewhere?
        public string ErrorMessage { get; set; } //TODO - Elsewhere?

        //internal static TngnWsvcConfig Load()
        //{
        //}
    }
}