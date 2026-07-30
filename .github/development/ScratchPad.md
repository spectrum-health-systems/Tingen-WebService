<div align="center">

  <h1>Tingen Web Service: Scratchpad</h1>

</div>

***

"${DateTime.Now:yyMMdd-HHmmss-fffffff}-[ERR1120]-TwsConfigLoadFailed", SysMsg.ERR1120(ex.Message)[1]

$"{DateTime.Now:yyMMdd-HHmmss-fffffff}-[DEBUG]-FrwkConfig.Build"

***

/* Use primeval logs here to debug, since logging functionality has not been initialized yet. */
//LogEvent.Primeval("TingenWebServiceStarted", RedPrint.DebugStartMessage(sentScriptParam));

*** 

Update/create/modify the XML documentation in AppData/XmlDocumentation/NamespaceDocumentation.xml, using the guidelines in .github/agents/AGENT-CSharp-XmlDocumentation.md
You caren update/modify existing documentation if necessary.

****

### Netsmart Query web service URL

The `TingenWebService_NtstWsvcQueryUat_Query` setting should be set to the URL of the Netsmart Query web service for your organization, which will look like this:

`https://{YourOrganization}.netsmartcloud.com/csp/{YourOrganization}uat/avpm/WEBSVC.Query.cls`
