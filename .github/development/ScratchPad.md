<div align="center">

  <h1>Tingen Web Service: Scratchpad</h1>

</div>

***


```xml
 <applicationSettings>
    <TingenWebService.Properties.Settings>
      <setting name="AvatarSystem" serializeAs="String">
        <value>UAT</value>
      </setting>

      <setting name="TraceLogLimit" serializeAs="String">
        <value>0</value>
      </setting>
      <setting name="ServerWwwPath" serializeAs="String">
        <value>C:\Tingen_www\WebService</value>
      </setting>
      <setting name="ServerDataPath" serializeAs="String">
        <value>C:\Tingen_Data\WebService</value>
      </setting>
      <setting name="NtstWsvcUserName" serializeAs="String">
        <value>unassigned</value>
      </setting>
      <setting name="NtstWsvcUserPass" serializeAs="String">
        <value>unassigned</value>
      </setting>
      <setting name="BuildNumber" serializeAs="String">
        <value>260618</value>
      </setting>
      <setting name="SessionLogLimit" serializeAs="String">
        <value>0</value>
      </setting>
      <setting name="TingenWebService_NtstWsvcQueryUat_Query" serializeAs="String">
        <value>https://{YourOrganization}nxuat.netsmartcloud.com/csp/{YourOrganization}uat/avpm/WEBSVC.Query.cls</value>
      </setting>
    </TingenWebService.Properties.Settings>
  </applicationSettings>


  | Setting | Description | Default Value |
| ------- | ----------- | ------------- |
| `AvatarSystem` | The [Avatar system](../glossary/SystemSystemCode.md) | `UAT` |
| `Mode` | The Tingen Web Service [mode](../glossary/Mode.md) | `Enabled` |
| `TraceLogLimit` | The [trace log limit](../glossary/logging.md) | `0` (no trace logging) |
| `SessionLogLimit` | The [session log limit](../glossary/Session.md) | `0` (no session logging) |
| `ServerDataPath` | The Tingen Web Service [data folder](../glossary/Framework) | ex: `C:\Tingen_Data\WebService` |
| `ServerWwwPath` | The Tingen Web Service [www folder](../glossary/Framework) | ex: `C:\Tingen_www\WebService` |
| `NtstWsvcUserName` | The Avatar Username for Netsmart web services | `unassigned` |
| `NtstWsvcUserPass` | The Avatar Password for Netsmart web services | `unassigned` |
| `TingenWebService_NtstWsvcQueryUat_Query` | The Netsmart Query web service URL | [See below](#netsmart-query-web-service-url) |
| `BuildNumber` | The build number of the Tingen Web Service. | `260618` |



### Netsmart Query web service URL

The `TingenWebService_NtstWsvcQueryUat_Query` setting should be set to the URL of the Netsmart Query web service for your organization, which will look like this:

`https://{YourOrganization}.netsmartcloud.com/csp/{YourOrganization}uat/avpm/WEBSVC.Query.cls`
