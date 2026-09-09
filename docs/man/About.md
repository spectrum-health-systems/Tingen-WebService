[Manual](README.md) ❭ About the Tingen Web Service

<div align="center">

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="../../.github/logo/dark/256x173/Man.png">
    <source media="(prefers-color-scheme: light)" srcset="../../.github/logo/light/256x173/Man.png">
    <img alt="Fallback image description" src="../../.github/logo/light/256x173/Man.png">
  </picture>

  <br/>
  <br/>

  ![RELEASE](https://img.shields.io/badge/Release-26.9-teal)

<h1>About the Tingen Web Service</h1>

</div>

| CONTENTS |
|:---------|
| [An introduction](#an-introduction) |
| [How it works](#how-it-works) |
| &nbsp;&nbsp;&nbsp;&nbsp;[The 50,000 foot view](#the-50000-foot-view)  |
| &nbsp;&nbsp;&nbsp;&nbsp;[A closer look](#a-closer-look)               |
| &nbsp;&nbsp;&nbsp;&nbsp;[An *even closer* look](#an-even-closer-look) |

***

# An introduction

<!-- This text should be straight from the repository README.md, and should be updated when the README.md is updated. -->
Netsmart's [AvatarNX™ EHR](https://www.ntst.com/Solutions-and-Services/Offerings/myAvatar) is a behavioral health Electronic Health Records application that offers a recovery-focused suite of solutions that leverage real-time analytics and clinical decision support to drive value-based care.

While AvatarNX™ is a robust platform, it isn't perfect. The good news is that you can extend AvatarNX™ functionality via [Netsmart's Web Services](URL), and/or custom web services that are written by other AvatarNX™ users.

The **Tingen Web Service** is one such custom web service which includes various tools and utilities for AvatarNX™ that aren't included in the official release, and provides a solid foundation for building additional functionality quickly and efficiently.

# How it works

Here are some cool looking diagrams that will help explain how the Tingen Web Service works.

## The 50,000 foot view

A very high level overview of how the Tingen Web Service works:

1. Avatar sends an [`OptionObject`](../glossary/OptionObject.md) and a [`ScriptParameter`](../glossary/ScriptParameter.md) to the Tingen Web Service
2. The Tingen Web Service processes the request and returns a modified `OptionObject` back to Avatar

```mermaid
flowchart TB
  %% Components
  Start@{shape: circle, label: "Avatar"}
  TingenWebService@{shape: rounded, label: "Tingen Web Service"}
  %% Layout
  Start:::G1_ -- 1. Request --> TingenWebService:::E4_ -- 2. Response --> Start
  %% Styles
  classDef G1_ stroke:#e9f7ef,stroke-width:3px,fill:#a9dfbf,color:#145a32
  classDef E4_ stroke:#fdf2e9,stroke-width:3px,fill:#784212,color:#fdf2e9
```

## A closer look

If you want to understand the Tingen Web Service a little better, here's a more detailed overview of how it works:

1. Avatar sends an [`OptionObject`](../glossary/OptionObject.md) and a [`ScriptParameter`](../glossary/ScriptParameter.md) to the Tingen Web Service
2. The Tingen Web Service uses the `ScriptParameter` to determine what work needs to be done
3. The Tingen Web Service does the work, which may include modifying the `OptionObject`
4. The Tingen Web Service returns the modified `OptionObject` back to Avatar

```mermaid
flowchart TB
    %% Components
    Start@{shape: circle, label: "Avatar"}
    End@{shape: circle, label: "Avatar"}

    subgraph TingenWebService ["Tingen Web Service"]
      direction TB
      %% Components
      ParseScriptParameter@{shape: fr-rect, label: "Parse the Script Parameter"}
      DoWork@{shape: fr-rect, label: "Do work"}
      %% Layout
      ParseScriptParameter:::U3_ --> DoWork:::G3_
      %% Styles
      classDef G3_ stroke:#e9f7ef,stroke-width:3px,fill:#1d8348,color:#e9f7ef
      classDef U3_ stroke:#eaf2f8,stroke-width:2px,fill:#2471a3,color:#eaf2f8
    end

    %% Layout
    Start:::G1_ -- "&nbsp;OptionObject and&nbsp;\n&nbsp;Script Parameter&nbsp;" --> TingenWebService:::E4_ -- "&nbsp;Modified OptionObject&nbsp;\n&nbsp;and Script Parameter&nbsp;" --> End:::G1_
    classDef B0_ stroke:#FFFFFF,stroke-width:3px,fill:#000000,color:#FFFFFF
    classDef G1_ stroke:#e9f7ef,stroke-width:3px,fill:#a9dfbf,color:#145a32
    classDef E4_ stroke:#fdf2e9,stroke-width:3px,fill:#784212,color:#fdf2e9
    %% Links
    %% None.  
```

## An *even closer* look

Oh boy! This is going to be a doozy! Let's break down the Tingen Web Service's internal workings in excruciating detail.

```mermaid
flowchart TD
    %% Components
    Start@{shape: circle, label: "Avatar"}
    End@{shape: circle, label: "Avatar"}

    subgraph TingenWebService ["Tingen Web Service"]
      direction TB
      %% Components
      SubStart@{shape: sm-circ, label: ""}
      RuntimeConfig_Load@{shape: fr-rect, label: "RuntimeConfig.Load()"}
      CriticalFailureOccurred@{shape: rounded, label: "CriticalFailureOccurred()"}
      ReturnOriginalOptionObject@{shape: dbl-circ, label: "Return original\nOptionObject"}
      Mode@{shape: diam, label: "Mode"}
      Instance.Start@{shape: fr-rect, label: "Instance.Start()"}
      ParseParameter@{shape: fr-rect, label: "AvatarScriptParameter.ParseParameter()"}
      DoWork@{shape: fr-rect, label: "Do work"}
      LogSession@{shape: fr-rect, label: "LogEvent.Session()"}
      SubEnd@{shape: sm-circ, label: ""}
      %% Layout
      SubStart --> RuntimeConfig_Load:::E3_ --> CriticalFailureOccurred:::E3_
      CriticalFailureOccurred -- True --> ReturnOriginalOptionObject:::E3_
      CriticalFailureOccurred -- False --> Mode:::E3_
      Mode -- enabled --> Instance.Start:::U3_
      Mode -- passthrough --> Instance.Start
      Mode -- other --> ReturnOriginalOptionObject
      Instance.Start --> ParseParameter:::U3_
      ParseParameter --> DoWork:::G3_
      DoWork --> LogSession:::U3_
      LogSession --> SubEnd:::U3_   
      %% Styles
      classDef E3_ stroke:#fdf2e9,stroke-width:2px,fill:#ca6f1e,color:#fdf2e9
      classDef G3_ stroke:#e9f7ef,stroke-width:3px,fill:#1d8348,color:#e9f7ef
      classDef U3_ stroke:#eaf2f8,stroke-width:2px,fill:#2471a3,color:#eaf2f8
      %% Links
      %%click RuntimeConfig_Load "https://github.com/spectrum-health-systems/tingen-documentation-project/blob/main/static/diagrams/TingenWebService.Configuration.md#tingenwebserviceconfigurationruntimeconfigcs"
      %%click CriticalFailureOccurred "https://github.com/spectrum-health-systems/tingen-documentation-project/blob/main/static/diagrams/TingenWebService.md#criticalfailureoccurred"
    end

    %% Layout
    Start:::G1_ -- "OptionObject and&nbsp;\n&nbsp;Script Parameter&nbsp;" --> TingenWebService:::E4_ -- "&nbsp;Modified OptionObject&nbsp;\n&nbsp;and Script Parameter&nbsp;" --> End:::G1_
    classDef B0_ stroke:#FFFFFF,stroke-width:3px,fill:#000000,color:#FFFFFF
    classDef G1_ stroke:#e9f7ef,stroke-width:3px,fill:#a9dfbf,color:#145a32
    classDef E4_ stroke:#fdf2e9,stroke-width:3px,fill:#784212,color:#fdf2e9
    %% Links
    %% None.  
```

NEXT: [Requirements](Requirements.md)

<br/>

***

[Manual](README.md) ❭ About the Tingen Web Service

<sub>Last updated: 260618</sub>
