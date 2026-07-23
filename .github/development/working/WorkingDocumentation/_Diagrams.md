[🏠︎](README.md) ❭ Diagrams

<div align="center">

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="../../../.github/logo/Tingen-WebService-Development-Manual-Logo-Dark-256x256.png">
    <source media="(prefers-color-scheme: light)" srcset="../../../.github/logo/Tingen-WebService-Development-Manual-Logo-Light-256x256.png">
    <img alt="Fallback image description" src="../../../.github/logo/Tingen-WebService-Development-Manual-Logo-Light-256x256.png">
  </picture>

  <h1>Diagrams</h1>

</div>

---
### CONTENTS

* [TingenWebService](#tingenwebservice)<br>
  * Core
    * [Avatar](#avatar)
    * [Configuration](#configuration)
    * [Du](#du)
    * [Framework](#framework)
    * [Logger](#logger)
    * [Maintenance](#maintenance)
    * [Operation](#operation)
    * [Parse](#parse)
    * [Query](#query)
    * [TingenWsvcSession](#tingenwsvcsession)
    * [Translation](#translation)
  * Module
    * [DoseChangeEvaluationOtp](#dosechangeevaluationotp)
    * [OpenIncident](#openincident)
    * [TngnWsvc](#tngnwsvc)

---

## TingenWebService

### GetVersion()

Nothing to see here.

### RunScript()

<div align="center">

```mermaid
flowchart TD
    %% Components
    Start@{shape: sm-circ, label: ""}
    RuntimeConfig_Load@{shape: fr-rect, label: "RuntimeConfig.Load()"}
    CriticalFailureOccurred@{shape: rounded, label: "CriticalFailureOccurred()"}
    ReturnOriginalOptionObject@{shape: dbl-circ, label: "Return original\nOptionObject"}
    Mode@{shape: diam, label: "Mode"}
    Instance.Start@{shape: fr-rect, label: "Instance.Start()"}
    ParseParameter@{shape: fr-rect, label: "AvatarScriptParameter.ParseParameter()"}
    LogSession@{shape: fr-rect, label: "LogEvent.Session()"}
    ReturnUpdatedOptionObject@{shape: dbl-circ, label: "Return updated\nOptionObject"}
    %% Layout
    Start --> RuntimeConfig_Load:::E3_ --> CriticalFailureOccurred:::E3_
    CriticalFailureOccurred -- True --> ReturnOriginalOptionObject:::E3_
    CriticalFailureOccurred -- False --> Mode:::E3_
    Mode -- enabled --> Instance.Start:::U3_
    Mode -- passthrough --> Instance.Start
    Mode -- other --> ReturnOriginalOptionObject
    Instance.Start --> ParseParameter:::U3_
    ParseParameter --> LogSession:::U3_
    LogSession --> ReturnUpdatedOptionObject:::E3_
    %% Styles
    classDef E3_ stroke:#fdf2e9,stroke-width:2px,fill:#ca6f1e,color:#fdf2e9
    classDef U3_ stroke:#eaf2f8,stroke-width:2px,fill:#2471a3,color:#eaf2f8
    %% Links
    click RuntimeConfig_Load "https://github.com/spectrum-health-systems/tingen-documentation-project/blob/main/static/diagrams/TingenWebService.Configuration.md#tingenwebserviceconfigurationruntimeconfigcs"
    click CriticalFailureOccurred "https://github.com/spectrum-health-systems/tingen-documentation-project/blob/main/static/diagrams/TingenWebService.md#criticalfailureoccurred"
```

</div>

### CriticalFailureOccurred()

<div align="center">

```mermaid
flowchart TD
    %% Content
    Start@{shape: sm-circ, label: ""}
    InvalidAvatarData:::E3_@{shape: diam, label: "Invalid data\nfrom Avatar?"}
    Mode:::E3_@{shape: diam, label: "Mode"}
    WriteTrueLog:::E3_@{shape: rect, label: "Write log"}
    WriteFalseLog:::E3_@{shape: rect, label: "Write log"}
    ReturnTrue:::E3_@{shape: rounded, label: "Return true"}
    ReturnFalse:::E3_@{shape: rounded, label: "Return false"}
    %% Layout
    Start --> InvalidAvatarData
    InvalidAvatarData -- Yes --> WriteFalseLog--> ReturnTrue
    InvalidAvatarData -- No --> Mode
    Mode -- enabled --> ReturnFalse
    Mode -- passthrough --> ReturnFalse
    Mode -- disabled --> WriteTrueLog
    Mode -- default --> WriteTrueLog
    WriteTrueLog --> ReturnTrue
    %% Styles
    classDef E3_ stroke:#fdf2e9,stroke-width:2px,fill:#ca6f1e,color:#fdf2e9
```

</div>

## TngnWsvcSession

### Instance.Load()

```mermaid
flowchart TD
    A[Instance.Load] --> B[Call Details.Load]
    B --> C[Call Folders.Load]
    C --> D[Call LogSettings.Load]
    D --> E[Create AvatarOptionObject]
    E --> F[Create AvatarScriptParameter ]
    F --> G[Return new Instance]
```

## Instance.Start()

```mermaid
flowchart TD
    A[Instance.Start] --> B[Instance.Load]
    B --> C[Instance created]
    C --> D[Folders.CreateSessionFolder]
    D --> E[Return session]
```

### Detail.Load()

```mermaid
flowchart TD
    A[Details.Load] --> B[Get current date ]
    B --> C[Get current time]
    C --> D[Set Version]
    D --> E[Set Mode]
    E --> F[Set AvatarSystem]
    F --> G[Set AvatarUserName]
    G --> H[Set RunningLog]
    H --> I[Return new Details object]
```

### Configuration.RuntimeConfig.Load()

```mermaid
flowchart TD
    A[RuntimeConfig.Load] --> B[Receive webConfig and tngnWsvcVer]
    B --> C[Create new Dictionary]
    C --> D[Add Version]
    D --> E[Add BuildNumber]
    E --> F[Add AvatarSys]
    F --> G[Add Mode]
    G --> H[Add BaseWww]
    H --> I[Add BaseData]
    I --> J[Add TraceLogLimit]
    J --> K[Return Dictionary]
```

<br/>

***

[🏠︎](README.md) ❭ Diagrams

<sub>Last updated: 2026-06-01</sub>
