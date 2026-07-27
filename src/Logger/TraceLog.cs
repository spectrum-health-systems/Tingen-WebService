// 260725_code
// 260725_documentation

namespace TingenWebService.Logger
{
    /// <summary>Provides helpers for writing trace log files for the current session.</summary>
    internal static class TraceLog
    {
        ///// <summary>Creates a trace log file in the session folder when the trace level is within the configured limit.</summary>
        ///// <remarks>
        ///// Skips writing when <paramref name="traceLogLimit"/> is <c>0</c> or when <paramref name="traceLevel"/>
        ///// exceeds <paramref name="traceLogLimit"/>.<br/>
        ///// <br/>
        ///// When a trace is written, this method sleeps briefly so successive trace files have unique timestamp-based
        ///// file names.
        ///// </remarks>
        ///// <param name="traceLevel">The trace level for this entry.</param>
        ///// <param name="levelLimit">The configured maximum trace level that should be written; <c>0</c> disables tracing.</param>
        ///// <param name="sessionFolder">The session folder where the trace file will be written.</param>
        ///// <param name="classPath">The full class path used to derive the class name in the trace file name.</param>
        ///// <param name="methodName">The method name included in the trace file name.</param>
        ///// <param name="lineNumber">The source line number included in the trace file name.</param>
        ///// <example>
        ///// <code>
        ///// TraceLog.Create(
        /////     traceLevel:    1,
        /////     levelLimit:    tngnWsvcSession.LogSetting.TraceLogLimit,
        /////     sessionFolder: tngnWsvcSession.Framework.TngnWsvcDataFolder.Session,
        /////     exeAsm:        AvatarOptionObject.ExeAsm,
        /////     classPath:     "Core/Avatar/AvatarOptionObject.cs",
        /////     methodName:    "ToReturn",
        /////     lineNumber:    57);
        ///// // Writes a file similar to:
        ///// //   {Session}\3045-12345-TingenWebService-AvatarOptionObject-ToReturn-57.trace
        ///// </code>
        ///// </example>
        //internal static void Create(int traceLevel, int levelLimit, string sessionFolder, string classPath, string methodName, int lineNumber)
        //{
        //    if (levelLimit != 0 && (traceLevel <= levelLimit))
        //    {
        //        Thread.Sleep(levelLimit);

        //        var logName = $"{DateTime.Now:ssff-fffff}-{LogUtility.GetClassName(classPath)}-{methodName}-{lineNumber}.trace";

        //        LogUtility.WriteLocal(sessionFolder, logName, "");
        //    }
        //}
    }
}