[Development Manual](README.md) ❭ Publish to UAT

<div align="center">

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="../../.github/logo/dark/256x173/DevMan.png">
    <source media="(prefers-color-scheme: light)" srcset="../../.github/logo/light/256x173/DevMan.png">
    <img alt="Fallback image description" src="../../.github/logo/light/256x173/DevMan.png">
  </picture>

  <h1>Publish to UAT</h1>

</div>

***

> [!NOTE]
> ***The Tingen Web Service should only be published to your UAT environment***. This ensures that your production environment remains stable and unaffected by development changes.
>
> For details on how to publish to your production environment, please refer to the [Production Deployment Guide](NEED-LINK).

***

| CONTENTS |
| -------- |
| [Creating a publish profile](#creating-a-publish-profile) |
| [Publishing to your UAT Environment](#publishing-to-your-uat-environment) |
| [Configuring the UAT instance](#configuring-the-uat-instance) |
| &nbsp;&nbsp;&nbsp;&nbsp;[Configuring the Tingen Web Service](#configuring-the-tingen-web-service) |
| &nbsp;&nbsp;&nbsp;&nbsp;[Configuring Modules](#configuring-modules) |
| [Examples of pubxml files](#examples-of-pubxml-files) |

> [!NOTE]
> Depending on what documentation you are reading, you will see the terms *deploying* and *publishing* used when refering to the process of "installing" the Tingen Web Service.
>
> **Deploying** means to "install" the Community Release of the Tingen Web Service to your LIVE environment.
> **Publishing** means to "install" the Tingen Web Service to a UAT environment for development purposes using the Visual Studio publish feature.

Publishing a development version of the Tingen Web Service is fairly simple:

1. Publish the development version to your ***UAT environment***
2. Make any necessary configuration changes

## Creating a publish profile

> [!IMPORTANT]
> Before you publish, make sure all resources in `AppData/` have the following settings:
>
> * **Build Action** = `Content`
> * **Copy to Output Directory** = `Copy if newer`

To ceate a publish profile:

1. **Right click** on the Tingen Web Service project in Visual Studio and select **Publish**
2. In the **Publish - Target** popup, choose **Folder** and click **Next**
3. In the **Publish - Folder** popup, choose a location to publish the release (e.g., `X:\WebService\UAT`) and click **Finish**
4. Click **Close** to close the Publish popup

You should now have a new publish profile called **FolderProfile.pubxml**.

5. Click **More actions**
6. Click **Rename**
7. In the **New Profile Name** field, enter **PublishToUAT** and click **OK**

You should now have the following files in the `src/Properties/PublishProfiles` folder:

<details>
  <summary>[Click here for an example of the <code>PublishToUAT.pubxml</code> file]</summary>

```xml
<?xml version="1.0" encoding="utf-8"?>
<!-- https://go.microsoft.com/fwlink/?LinkID=208121. -->
<Project>
  <PropertyGroup>
    <DeleteExistingFiles>true</DeleteExistingFiles>
    <ExcludeApp_Data>false</ExcludeApp_Data>
    <LaunchSiteAfterPublish>true</LaunchSiteAfterPublish>
    <LastUsedBuildConfiguration>Release</LastUsedBuildConfiguration>
    <LastUsedPlatform>Any CPU</LastUsedPlatform>
    <PublishProvider>FileSystem</PublishProvider>
    <PublishUrl>XWebService\UAT</PublishUrl>
    <WebPublishMethod>FileSystem</WebPublishMethod>
    <_TargetId>Folder</_TargetId>
    <SiteUrlToLaunchAfterPublish />
  </PropertyGroup>
</Project>
```

</details>

<details>
  <summary>[Click here for an example of the <code>PublishToUAT.pubxml.user</code> file]</summary>

```xml
<?xml version="1.0" encoding="utf-8"?>
<!-- https://go.microsoft.com/fwlink/?LinkID=208121. -->
<Project>
  <PropertyGroup>
    <_PublishTargetUrl>X:\WebService\UAT</_PublishTargetUrl>
    <History></History>
    <LastFailureDetails />
  </PropertyGroup>
  <ItemGroup>
    <File Include="AppData/Blueprint/critical-error.blueprint">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="AppData/Blueprint/error.blueprint">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="AppData/Blueprint/session-log.blueprint">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="AppData/OptObjErrorMessage/tngnwsvc-test-complete.ooem">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="AppData/OptObjErrorMessage/unknown-parameter.ooem">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="AppData/TranslationTable/form_id-form_name.trans">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="AppData/XmlDocumentation/TngnWsvc.xml">
      <publishTime>06/17/2026 09:13:00</publishTime>
    </File>
    <File Include="AppData/XmlDocumentation/_old/Core.Admin.xml">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="AppData/XmlDocumentation/_old/Core.Avatar.xml">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="AppData/XmlDocumentation/_old/Core.Logger.xml">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="AppData/XmlDocumentation/_old/Core.TngnWsvc.xml">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="AppData/XmlDocumentation/_old/Manual.xml">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="AppData/XmlDocumentation/_old/Mode.xml">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="AppData/XmlDocumentation/_old/template.xml">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="bin/AppData/Blueprint/critical-error.blueprint">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="bin/AppData/Blueprint/error.blueprint">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="bin/AppData/Blueprint/session-log.blueprint">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="bin/AppData/OptObjErrorMessage/tngnwsvc-test-complete.ooem">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="bin/AppData/OptObjErrorMessage/unknown-parameter.ooem">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="bin/AppData/TranslationTable/form_id-form_name.trans">
      <publishTime>05/13/2026 09:30:01</publishTime>
    </File>
    <File Include="bin/Microsoft.Bcl.AsyncInterfaces.dll">
      <publishTime>08/19/2025 18:12:50</publishTime>
    </File>
    <File Include="bin/Microsoft.CodeDom.Providers.DotNetCompilerPlatform.dll">
      <publishTime>03/05/2023 17:41:40</publishTime>
    </File>
    <File Include="bin/Newtonsoft.Json.dll">
      <publishTime>09/16/2025 04:04:22</publishTime>
    </File>
    <File Include="bin/Outpost31.dll">
      <publishTime>12/01/2025 11:00:13</publishTime>
    </File>
    <File Include="bin/Outpost31.dll.config">
      <publishTime>11/13/2025 10:17:25</publishTime>
    </File>
    <File Include="bin/Outpost31.pdb">
      <publishTime>12/01/2025 11:00:13</publishTime>
    </File>
    <File Include="bin/Outpost31.XmlSerializers.dll">
      <publishTime>12/01/2025 11:00:14</publishTime>
    </File>
    <File Include="bin/roslyn/csc.exe">
      <publishTime>02/15/2022 00:38:32</publishTime>
    </File>
    <File Include="bin/roslyn/csc.exe.config">
      <publishTime>02/15/2022 00:38:32</publishTime>
    </File>
    <File Include="bin/roslyn/csc.rsp">
      <publishTime>02/15/2022 00:21:10</publishTime>
    </File>
    <File Include="bin/roslyn/csi.exe">
      <publishTime>02/15/2022 00:38:42</publishTime>
    </File>
    <File Include="bin/roslyn/csi.exe.config">
      <publishTime>02/15/2022 00:38:42</publishTime>
    </File>
    <File Include="bin/roslyn/csi.rsp">
      <publishTime>02/15/2022 00:21:14</publishTime>
    </File>
    <File Include="bin/roslyn/Microsoft.Build.Tasks.CodeAnalysis.dll">
      <publishTime>02/15/2022 00:33:12</publishTime>
    </File>
    <File Include="bin/roslyn/Microsoft.CodeAnalysis.CSharp.dll">
      <publishTime>02/15/2022 00:36:00</publishTime>
    </File>
    <File Include="bin/roslyn/Microsoft.CodeAnalysis.CSharp.Scripting.dll">
      <publishTime>02/15/2022 00:38:34</publishTime>
    </File>
    <File Include="bin/roslyn/Microsoft.CodeAnalysis.dll">
      <publishTime>02/15/2022 00:33:04</publishTime>
    </File>
    <File Include="bin/roslyn/Microsoft.CodeAnalysis.Scripting.dll">
      <publishTime>02/15/2022 00:33:26</publishTime>
    </File>
    <File Include="bin/roslyn/Microsoft.CodeAnalysis.VisualBasic.dll">
      <publishTime>02/15/2022 00:34:50</publishTime>
    </File>
    <File Include="bin/roslyn/Microsoft.CSharp.Core.targets">
      <publishTime>02/15/2022 00:21:10</publishTime>
    </File>
    <File Include="bin/roslyn/Microsoft.DiaSymReader.Native.amd64.dll">
      <publishTime>10/04/2021 20:47:54</publishTime>
    </File>
    <File Include="bin/roslyn/Microsoft.DiaSymReader.Native.x86.dll">
      <publishTime>10/04/2021 20:49:46</publishTime>
    </File>
    <File Include="bin/roslyn/Microsoft.Managed.Core.CurrentVersions.targets">
      <publishTime>02/15/2022 00:33:08</publishTime>
    </File>
    <File Include="bin/roslyn/Microsoft.Managed.Core.targets">
      <publishTime>02/15/2022 00:21:10</publishTime>
    </File>
    <File Include="bin/roslyn/Microsoft.VisualBasic.Core.targets">
      <publishTime>02/15/2022 00:21:10</publishTime>
    </File>
    <File Include="bin/roslyn/System.Buffers.dll">
      <publishTime>02/19/2020 05:05:18</publishTime>
    </File>
    <File Include="bin/roslyn/System.Collections.Immutable.dll">
      <publishTime>10/19/2020 14:37:44</publishTime>
    </File>
    <File Include="bin/roslyn/System.Memory.dll">
      <publishTime>02/19/2020 05:05:18</publishTime>
    </File>
    <File Include="bin/roslyn/System.Numerics.Vectors.dll">
      <publishTime>05/15/2018 09:29:44</publishTime>
    </File>
    <File Include="bin/roslyn/System.Reflection.Metadata.dll">
      <publishTime>10/19/2020 14:45:32</publishTime>
    </File>
    <File Include="bin/roslyn/System.Runtime.CompilerServices.Unsafe.dll">
      <publishTime>10/19/2020 14:46:30</publishTime>
    </File>
    <File Include="bin/roslyn/System.Text.Encoding.CodePages.dll">
      <publishTime>11/29/2018 10:39:18</publishTime>
    </File>
    <File Include="bin/roslyn/System.Threading.Tasks.Extensions.dll">
      <publishTime>02/19/2020 05:05:18</publishTime>
    </File>
    <File Include="bin/roslyn/vbc.exe">
      <publishTime>02/15/2022 00:34:58</publishTime>
    </File>
    <File Include="bin/roslyn/vbc.exe.config">
      <publishTime>02/15/2022 00:34:56</publishTime>
    </File>
    <File Include="bin/roslyn/vbc.rsp">
      <publishTime>02/15/2022 00:21:12</publishTime>
    </File>
    <File Include="bin/roslyn/VBCSCompiler.exe">
      <publishTime>02/15/2022 00:38:42</publishTime>
    </File>
    <File Include="bin/roslyn/VBCSCompiler.exe.config">
      <publishTime>02/15/2022 00:38:42</publishTime>
    </File>
    <File Include="bin/ScriptLinkStandard.dll">
      <publishTime>06/18/2026 10:19:18</publishTime>
    </File>
    <File Include="bin/ScriptLinkStandard.pdb">
      <publishTime>06/18/2026 10:19:18</publishTime>
    </File>
    <File Include="bin/System.Buffers.dll">
      <publishTime>03/19/2025 16:55:38</publishTime>
    </File>
    <File Include="bin/System.IO.Pipelines.dll">
      <publishTime>08/19/2025 18:20:46</publishTime>
    </File>
    <File Include="bin/System.Memory.dll">
      <publishTime>04/03/2025 19:00:54</publishTime>
    </File>
    <File Include="bin/System.Numerics.Vectors.dll">
      <publishTime>03/19/2025 16:55:42</publishTime>
    </File>
    <File Include="bin/System.Runtime.CompilerServices.Unsafe.dll">
      <publishTime>04/03/2025 19:00:52</publishTime>
    </File>
    <File Include="bin/System.Text.Encodings.Web.dll">
      <publishTime>08/19/2025 18:19:40</publishTime>
    </File>
    <File Include="bin/System.Text.Json.dll">
      <publishTime>08/19/2025 18:21:52</publishTime>
    </File>
    <File Include="bin/System.Threading.Tasks.Extensions.dll">
      <publishTime>04/03/2025 19:00:52</publishTime>
    </File>
    <File Include="bin/System.ValueTuple.dll">
      <publishTime>05/15/2025 10:46:09</publishTime>
    </File>
    <File Include="bin/tingen-web-service.xml">
      <publishTime>06/18/2026 10:19:21</publishTime>
    </File>
    <File Include="bin/TingenWebService.dll">
      <publishTime>06/18/2026 10:19:21</publishTime>
    </File>
    <File Include="bin/TingenWebService.pdb">
      <publishTime>06/18/2026 10:19:21</publishTime>
    </File>
    <File Include="bin/TingenWebService.XmlSerializers.dll">
      <publishTime>06/18/2026 10:19:22</publishTime>
    </File>
    <File Include="TingenWebService.asmx">
      <publishTime>05/13/2026 09:30:02</publishTime>
    </File>
    <File Include="Web References/NtstWsvcQueryUat/Reference.map">
      <publishTime>05/13/2026 09:30:02</publishTime>
    </File>
    <File Include="Web.config">
      <publishTime>06/18/2026 10:19:23</publishTime>
    </File>
  </ItemGroup>
</Project>
```

</details>

## Publishing to your UAT Environment

Select the **Deploy to UAT** profile, then click the **Publish** button.

### Verifying the deployment

Clicking **Navigate** will open a window to the Tingen Web Service in your UAT environment, which should look like [the default Tingen_www folder](../glossary/Framework.md).

## Configuring the UAT instance

> [!NOTE]
> These instructions are for configuring the Tingen Web Service in your **UAT environment**, and assume that you have deployed the Tingen Web Service to `C:\Tingen_www\WebService\UAT\`.

After publishing the Tingen Web Service, there are settings that can/should be configured.

The configuration file for the Tingen Web Service is the `Web.config` file, which is located in the `C:\Tingen_www\WebService\UAT\` folder on the host machine.

<details>
  <summary>[Click here for an example of the <code>Web.config</code> file]</summary>

```xml
 <applicationSettings>
    <TingenWebService.Properties.Settings>
      <setting name="AvatarSystem" serializeAs="String">
        <value>UAT</value>
      </setting>
      <setting name="DataRoot" serializeAs="String">
        <value>C:\Tingen_Data</value>
      </setting>
      <setting name="WwwRoot" serializeAs="String">
        <value>C:\Tingen_www</value>
      </setting>
    </TingenWebService.Properties.Settings>
  </applicationSettings>
```

</details>

These are the settings that can be configured in the `C:\Tingen_www\WebService\UAT\Web.config` file:

| Setting | Description | Default Value |
| ------- | ----------- | ------------- |
| `AvatarSystem` | The [Avatar system](../glossary/SystemSystemCode.md) | `UAT` |
| `ServerDataPath` | The Tingen Web Service [data folder](../glossary/Framework) | ex: `C:\Tingen_Data\WebService` |
| `ServerWwwPath` | The Tingen Web Service [www folder](../glossary/Framework) | ex: `C:\Tingen_www\WebService` |

### Configuring Modules

TBD

<br/>

***

[Development Manual](README.md) ❭ Publish to UAT

<sub>Last updated: 260723</sub>
