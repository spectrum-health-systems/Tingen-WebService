[Manual](README.md) ❭ Configuring the Tingen Web Service

<div align="center">

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="../../.github/logo/dark/256x173/Man.png">
    <source media="(prefers-color-scheme: light)" srcset="../../.github/logo/light/256x173/Man.png">
    <img alt="Fallback image description" src="../../.github/logo/light/256x173/Man.png">
  </picture>

  <br/>
  <br/>

  ![RELEASE](https://img.shields.io/badge/Release-26.7-teal)

<h1>Configuring the Tingen Web Service</h1>

</div>

After publishing the Tingen Web Service, there are settings that can/should be configured.

The configuration file for the Tingen Web Service is the `Web.config` file, which is located in the `{HOST}:\Tingen_www\WebService\LIVE\` folder on the host machine.

<details>
  <summary>[Click here for an example of the <code>Web.config</code> file]</summary>

```xml
 <applicationSettings>
    <TingenWebService.Properties.Settings>
      <setting name="AvatarSystem" serializeAs="String">
        <value>LIVE</value>
      </setting>
      <setting name="Mode" serializeAs="String">
        <value>Enabled</value>
      </setting>
      <setting name="TraceLogLimit" serializeAs="String">
        <value>0</value>
      </setting>
      <setting name="ServerWwwPath" serializeAs="String">
        <value>{HOST}:\Tingen_www\WebService</value>
      </setting>
      <setting name="ServerDataPath" serializeAs="String">
        <value>{HOST}:\Tingen_Data\WebService</value>
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
```

</details>

These are the settings that can be configured in the `C:\Tingen_www\WebService\LIVE\Web.config` file:

| Setting | Description | Default Value |
| ------- | ----------- | ------------- |
| `AvatarSystem` | The [Avatar system](../glossary/SystemSystemCode.md) | `LIVE` |
| `Mode` | The Tingen Web Service [mode](../glossary/Mode.md) | `Enabled` |
| `TraceLogLimit` | The [trace log limit](../glossary/logging.md) | `0` (no trace logging) |
| `SessionLogLimit` | The [session log limit](../glossary/Session.md) | `0` (no session logging) |
| `ServerDataPath` | The Tingen Web Service [data folder](../glossary/Framework) | ex: `{HOST}:\Tingen_Data\WebService` |
| `ServerWwwPath` | The Tingen Web Service [www folder](../glossary/Framework) | ex: `{HOST}:\Tingen_www\WebService` |
| `NtstWsvcUserName` | The Avatar Username for Netsmart web services | `unassigned` |
| `NtstWsvcUserPass` | The Avatar Password for Netsmart web services | `unassigned` |
| `TingenWebService_NtstWsvcQueryUat_Query` | The Netsmart Query web service URL | [See below](#netsmart-query-web-service-url) |
| `BuildNumber` | The build number of the Tingen Web Service. | `260618` |

### Netsmart Query web service URL

The `TingenWebService_NtstWsvcQueryUat_Query` setting should be set to the URL of the Netsmart Query web service for your organization, which will look like this:

`https://{YourOrganization}.netsmartcloud.com/csp/{YourOrganization}uat/avpm/WEBSVC.Query.cls`

### Configuring Modules

TBD

<br/>

***

[Manual](README.md) ❭ Configuring the Tingen Web Service

<sub>Last updated: 260709</sub>
