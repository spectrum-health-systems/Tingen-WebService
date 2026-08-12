// 260812_code
// 260812_documentation
using System;

namespace TingenWebService.Core.Trove
{
    public class RunningLog
    {
        internal static string ParseRequest(string request) =>
            $"**PARSE REQUEST**  {Environment.NewLine}" +
            $"Request: {request}  {Environment.NewLine}" +
            $"{Environment.NewLine}";

        internal static string TranslateFormId(string formId, string formName) =>
            $"**TRANSLATE FORM**  {Environment.NewLine}" +
            $"Form ID: `{formId}` => Form Name: `{formName}`  {Environment.NewLine}" +
            $"{Environment.NewLine}";
    }
}