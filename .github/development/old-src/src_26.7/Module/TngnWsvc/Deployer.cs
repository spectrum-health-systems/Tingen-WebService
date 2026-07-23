// 251106_code
// 251106_documentation

namespace TingenWebService.Module.TngnWsvc
{
    /////////// <summary>Administrative deployment functionality.</summary>
    //////public class Deployer
    //////{
    //////    public static string AsmName { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;
    //////    public static void ExportOptionObject(OptionObject2015 optObj)
    //////    {
    //////        /* For future use */
    //////        //var optObjHtml = optObj.ToHtmlString(true);
    //////        //File.WriteAllText($@"C:\IT\TngnWsvc_OptionObject2015.html", optObjHtml);
    //////        //var optObjJson = optObj.ToJson();
    //////        //File.WriteAllText($@"C:\IT\TngnWsvc_OptionObject2015.json", optObjJson);
    //////    }
    //////    public static void Deploy(TngnWsvcSession wsvcSession)
    //////    {
    //////        // Note about how config files are handled.
    //////        ////Folders.CreateFolderFramework(wsvcSession.Folder, wsvcSession.RuntimeSetting.AvatarSystem);
    //////        ////LogEvent.Debug();
    //////        ////RefreshData(wsvcSession.Folder, wsvcSession.LogSetting.TraceLogLimit);
    //////        ////LogEvent.Debug();
    //////        ////wsvcSession.RuntimeSetting.RunningLog = "Deployed Tingen Web Service framework.";
    //////    }
    //////    //internal static void RefreshData(Folders folder, int traceLogLimit)
    //////    //{
    //////    //    RefreshAppData(folder.BaseWww, folder.BaseData, folder.CurrentSession);
    //////    //}
    //////    public static void RefreshAppData(string baseWww, string baseData, string currentFolder)
    //////    {
    //////        // TODO - This doesn't work right. You need to run this a few times to get all the files copied.
    //////        var source = $@"{baseWww}\bin\AppData";
    //////        var target = $@"{baseData}\AppData\www";
    //////        if (Directory.Exists(target))
    //////        {
    //////            Directory.Delete(target, true);
    //////        }
    //////        Directory.CreateDirectory(target);
    //////        CopyDirectory(source, target);
    //////    }
    //////    internal static void CopyDirectory(string source, string target)
    //////    {
    //////        Directory.CreateDirectory(target);
    //////        foreach (string file in Directory.GetFiles(source))
    //////        {
    //////            string destFile = Path.Combine(target, Path.GetFileName(file));
    //////            File.Copy(file, destFile, true);
    //////        }
    //////        foreach (string subDir in Directory.GetDirectories(source))
    //////        {
    //////            string destSubDir = Path.Combine(target, Path.GetFileName(subDir));
    //////            CopyDirectory(subDir, destSubDir);
    //////        }
    //////    }
    //////    internal static void Test(Folders folder, int traceLogLimit)
    //////    {
    //////        GenerateAppLogs(folder, traceLogLimit);
    //////    }
    //////    internal static void GenerateAppLogs(Folders folder, int traceLogLimit)
    //////    {
    //////        LogEvent.Debug("Regression test 1 of 20");
    //////        LogEvent.Debug("Regression test 2 of 20");
    //////        LogEvent.Debug("Regression test 3 of 20");
    //////        LogEvent.Debug("Regression test 4 of 20");
    //////        LogEvent.Debug("Regression test 5 of 20");
    //////        LogEvent.Debug("Regression test 6 of 20");
    //////        LogEvent.Debug("Regression test 7 of 20");
    //////        LogEvent.Debug("Regression test 8 of 20");
    //////        LogEvent.Debug("Regression test 9 of 20");
    //////        LogEvent.Debug("Regression test 10 of 20");
    //////        LogEvent.Debug("Regression test 11 of 20");
    //////        LogEvent.Debug("Regression test 12 of 20");
    //////        LogEvent.Debug("Regression test 13 of 20");
    //////        LogEvent.Debug("Regression test 14 of 20");
    //////        LogEvent.Debug("Regression test 15 of 20");
    //////        LogEvent.Debug("Regression test 16 of 20");
    //////        LogEvent.Debug("Regression test 17 of 20");
    //////        LogEvent.Debug("Regression test 18 of 20");
    //////        LogEvent.Debug("Regression test 19 of 20");
    //////        LogEvent.Debug("Regression test 20 of 20");
    //////        LogEvent.Critical("RegressonTestUser01", folder, AsmName, logTitle: "Regression test", logMsg: "Regression test");
    //////        LogEvent.Critical("RegressonTestUser02", folder, AsmName, logTitle: "Regression test");
    //////        LogEvent.Critical("RegressonTestUser03", folder, AsmName);
    //////        LogEvent.Error("RegressonTestUser01", folder, AsmName, errCode: "E998", errMsg: "Regression test");
    //////        LogEvent.Error("RegressonTestUser02", folder, AsmName, errCode: "E999");
    //////        LogEvent.Error("RegressonTestUser03", folder, AsmName);
    //////        //////LogEvent.Trace(0, traceLogLimit, folder.CurrentSession, $"0-{traceLogLimit}");
    //////        //////LogEvent.Trace(1, traceLogLimit, folder.CurrentSession, $"1-{traceLogLimit}");
    //////        //////LogEvent.Trace(2, traceLogLimit, folder.CurrentSession, $"2-{traceLogLimit}");
    //////        //////LogEvent.Trace(3, traceLogLimit, folder.CurrentSession, $"3-{traceLogLimit}");
    //////        //////LogEvent.Trace(4, traceLogLimit, folder.CurrentSession, $"4-{traceLogLimit}");
    //////        //////LogEvent.Trace(5, traceLogLimit, folder.CurrentSession, $"5-{traceLogLimit}");
    //////        //////LogEvent.Trace(6, traceLogLimit, folder.CurrentSession, $"6-{traceLogLimit}");
    //////        //////LogEvent.Trace(7, traceLogLimit, folder.CurrentSession, $"7-{traceLogLimit}");
    //////        //////LogEvent.Trace(8, traceLogLimit, folder.CurrentSession, $"8-{traceLogLimit}");
    //////        //////LogEvent.Trace(9, traceLogLimit, folder.CurrentSession, $"9-{traceLogLimit}");
    //////    }
    //////}
}
