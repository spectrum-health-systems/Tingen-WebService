// 260805_code
// 260805_documentation

using System;

namespace TingenWebService.Core.Trove
{
    public class RunningLog
    {
        internal static string ParseRequest(string request)
        {
            return $"**[PARSE REQUEST]**  {Environment.NewLine}" +
                   $"Request: {request}  {Environment.NewLine}";
        }

        internal static string TranslateFormId(string formId, string formName)
        {
            return $"**[TRANSLATE FORM]**  {Environment.NewLine}" +
                   $"Form ID: `{formId}` => Form Name: `{formName}`  {Environment.NewLine}";
        }
    }
}