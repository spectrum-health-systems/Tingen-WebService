// 260812_code
// 260812_documentation

using System;
using System.Collections.Generic;
using System.IO;
using TingenWebService.Du;

namespace TingenWebService.Core.Trove
{
    /// <summary>User-modifiable templates and logic.</summary>
    /// <remarks>
    /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="Trove"]/AboutBlueprints/*'/>
    /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="Trove"]/RestoringBlueprints/*'/>
    /// </remarks>
    internal static class Blueprint
    {
        /// <summary>Build the blueprint for text error logs.</summary>
        /// <remarks>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="Logger"]/AboutLogFormats/*'/> <br/>
        /// </remarks>
        /// <returns>The text error log template string.</returns>
        internal static string ErrorLogTxt() =>
            $"Tingen Web Service Error Log{Environment.NewLine}" +
            $"Date/Time: ~SESSION~DATE~TIME~{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"[~ERROR~CODE~]{Environment.NewLine}" +
            $"~ERROR~MESSAGE~";

        /// <summary>Build the blueprint for text session logs.</summary>
        /// <remarks>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="Logger"]/AboutLogFormats/*'/> <br/>
        /// </remarks>
        /// <returns>The text session log template string.</returns>
        internal static string SessLogTxt() =>
            $"Release/Build: ~RELEASE~BUILD~{Environment.NewLine}" +
            $"Date: ~SESSION~DATE~ " +
            $"Start: ~SESSION~START~ / End: ~SESSION~END~ / Duration: ~SESSION~DURATION~{Environment.NewLine}" +
            $"Logged into ~AVATAR~SYSTEM~ as: ~AVATAR~USER~NAME~{Environment.NewLine}" +
            $"Script parameter: ~SCRIPT~PARAMETER~{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"~SESSION~RUNNING~LOG~" +
            $"{Environment.NewLine}" +
            $"~SESSION~DETAILS~" +
            $"{Environment.NewLine}" +
            $"[END]";

        /// <summary>Build the blueprint for markdown session logs.</summary>
        /// <remarks>
        /// <include file='AppData/XmlDoc/TopicDoc.xml' path='Topics/Topic[@name="Logger"]/AboutLogFormats/*'/><br/>
        /// </remarks>
        /// <returns>A string containing the markdown blueprint for the session log.</returns>
        internal static string SessLogMd() =>
            $"# Tingen Web Service Session Log{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"|                       |                 |{Environment.NewLine}" +
            $"|----------------------:|-----------------|{Environment.NewLine}" +
            $"| **Release/Build**     | ~RELEASE~BUILD~ |{Environment.NewLine}" +
            $"| **Date**              | ~SESSION~DATE~  |{Environment.NewLine}" +
            $"| **Start**             | ~SESSION~START~ |{Environment.NewLine}" +
            $"| **Logged in as**      | ~AVATAR~USER~NAME~ |{Environment.NewLine}" +
            $"| **Avatar system**     | ~AVATAR~SYSTEM~ |{Environment.NewLine}" +
            $"| **Script parameter**  | ~SCRIPT~PARAMETER~ |{Environment.NewLine}" +
            $"| **End**               | ~SESSION~END~ |{Environment.NewLine}" +
            $"| **Duration**          | ~SESSION~DURATION~ |{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"~SESSION~RUNNING~LOG~" +
            $"***{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"<sub>{Environment.NewLine}" +
            $"~SESSION~DETAILS~" +
            $"<sub>{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"***{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"<sub>[END]</sub>{Environment.NewLine}";

        ///// <summary>Build the blueprint for HTML session logs.</summary>
        ///// <remarks>
        ///// <include file='AppData/XmlDoc/Topics.xml' path='Topics/Topic[@name="Logger"]/AboutLogFormats/*'/> <br/>
        ///// </remarks>
        ///// <returns>The HTML session log template string.</returns>
        //internal static string SessLogHtml() =>
        //    $"<h1>Tingen Web Service Session Log</h1>{Environment.NewLine}" +
        //    $"<table>{Environment.NewLine}" +
        //    $"<tr>{Environment.NewLine}" +
        //    $"<td align = \"right\"><strong>Release/Build</strong></td>{Environment.NewLine}" +
        //    $"<td>~RELEASE~BUILD~</td>{Environment.NewLine}" +
        //    $"</tr>{Environment.NewLine}" +
        //    $"<tr>{Environment.NewLine}" +
        //    $"<td align = \"right\"><strong>Date</strong></td>{Environment.NewLine}" +
        //    $"<td>~SESSION~DATE~</td>{Environment.NewLine}" +
        //    $"</tr>{Environment.NewLine}" +
        //    $"<tr>{Environment.NewLine}" +
        //    $"<td align = \"right\"><strong>Start</strong></td>{Environment.NewLine}" +
        //    $"<td>~SESSION~START~</td>{Environment.NewLine}" +
        //    $"</tr>{Environment.NewLine}" +
        //    $"<tr>{Environment.NewLine}" +
        //    $"<td align = \"right\"><strong>Logged in as</strong></td>{Environment.NewLine}" +
        //    $"<td>~AVATAR~USER~NAME~</td>{Environment.NewLine}" +
        //    $"</tr>{Environment.NewLine}" +
        //    $"<tr>{Environment.NewLine}" +
        //    $"<td align = \"right\"><strong>Avatar system</strong></td>{Environment.NewLine}" +
        //    $"<td>~AVATAR~SYSTEM~</td>{Environment.NewLine}" +
        //    $"</tr>{Environment.NewLine}" +
        //    $"<tr>{Environment.NewLine}" +
        //    $"<td align = \"right\"><strong>Script parameter</strong></td>{Environment.NewLine}" +
        //    $"<td>~SCRIPT~PARAMETER~</td>{Environment.NewLine}" +
        //    $"</tr>{Environment.NewLine}" +
        //    $"<tr>{Environment.NewLine}" +
        //    $"<td align = \"right\"><strong>End</strong></td>{Environment.NewLine}" +
        //    $"<td>~SESSION~END~</td>{Environment.NewLine}" +
        //    $"</tr>{Environment.NewLine}" +
        //    $"<tr>{Environment.NewLine}" +
        //    $"<td align = \"right\"><strong>Duration</strong></td>{Environment.NewLine}" +
        //    $"<td>~SESSION~DURATION~</td>{Environment.NewLine}" +
        //    $"</tr>{Environment.NewLine}" +
        //    $"</table>{Environment.NewLine}" +
        //    $"<br/>{Environment.NewLine}" +
        //    $"<hr>{Environment.NewLine}" +
        //    $"<para>~SESSION~RUNNING~LOG~</para>{Environment.NewLine}" +
        //    $"<br/>{Environment.NewLine}" +
        //    $"<h2>Session Details</h2>{Environment.NewLine}" +
        //    $"<hr>{Environment.NewLine}" +
        //    $"<br/>{Environment.NewLine}" +
        //    $"<p><sub>End</sub></p>{Environment.NewLine}" +
        //    $"{Environment.NewLine}";

        /// <summary>Exports all blueprint templates to the specified host directory.</summary>
        /// <param name="blueprintRoot">The root directory where the blueprints will be exported.</param>
        internal static void ExportBlueprints(string blueprintRoot)
        {
            //LogEvent.Primeval("PRELOG-TRACE-FrameworkMaintenance-ExportBlueprints");

            var blueprintFactories = new Dictionary<string, Func<string>>
            {
                ["SessLogTxt"] = SessLogTxt,
                ["SessLogMd"]  = SessLogMd
            };

            foreach (var blueprintName in Catalog.BlueprintNames())
            {
                var blueprintFile = Path.Combine(blueprintRoot, $"{blueprintName}.blueprint");

                if (File.Exists(blueprintFile))
                {
                    continue;
                }

                if (blueprintFactories.TryGetValue(blueprintName, out var factory))
                {
                    var blueprintContent = factory();
                    DuFile.DeadDrop(blueprintFile, blueprintContent);
                }
            }
        }
    }
}