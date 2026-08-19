
### A note about branches

There are three main branches in the Tingen Web Service repository: 

<div align="center">

```mermaid
flowchart LR
  %% Components
  Development@{shape: rounded, label: "Development"}
  Testing@{shape: rounded, label: "Testing"}
  Stable@{shape: rounded, label: "Stable"}
  Release@{shape: rounded, label: "Release"}
  %% Layout
  Development:::G0_ --> Testing:::G1_ --> Stable:::G2_ --> Release:::G3_
  %% Styles
  %% Styles
  classDef G0_ stroke:#e9f7ef,stroke-width:3px,fill:#eaf2f8,color:#154360
  classDef G1_ stroke:#e9f7ef,stroke-width:3px,fill:#a9dfbf,color:#145a32
  classDef G2_ stroke:#e9f7ef,stroke-width:3px,fill:#52be80,color:#e9f7ef
  classDef G3_ stroke:#e9f7ef,stroke-width:3px,fill:#1d8348,color:#e9f7ef
  
```

</div>

* **Development**  
The main development branch where new features and updates are actively worked on. Changes in this branch are considered experimental and may not be stable.

* **Testing**  
Once the new features and updates in the Development branch have been tested and are deemed stable, they are merged into the Testing branch for further evaluation before being promoted to the Stable branch.

* **Stable**  
The Stable branch contains the code that is considered stable and ready for release. Changes in this branch are thoroughly tested and vetted before being promoted to the Release branch.

* **Release**  
The Release branch contains the officially released versions of the Tingen Web Service. Changes in this branch are minimal and typically only include critical bug fixes or updates that are necessary for the official release.

There may be other branches that are used for specific features, experiments, or hotfixes. These branches are typically temporary and may be merged into one of the main branches (Development, Testing, Stable, Release) once their purpose has been fulfilled.


## HOW IT WORKS

A very high level overview of how the Tingen Web Service works:

1. Avatar sends an `OptionObject` and a `ScriptParameter` to the Tingen Web Service
2. The Tingen Web Service processes the request and returns a modified `OptionObject` back to Avatar

```mermaid
flowchart TB
  %% Components
  Start@{shape: circle, label: "Avatar"}
  TingenWebService@{shape: rounded, label: "Tingen Web Service"}
  %% Layout
  Start:::U3_ -- 1. Request --> TingenWebService:::E4_ -- 2. Response --> Start
  %% Styles
  classDef U3_ stroke:#eaf2f8,stroke-width:3px,fill:#2471a3,color:#eaf2f8
  classDef E4_ stroke:#fdf2e9,stroke-width:3px,fill:#784212,color:#fdf2e9
```



  <img src="./.github/logo/Tingen-WebService-512x346.png" alt="Tingen Web Service">

  <img src="https://github.com/spectrum-health-systems/Tingen-WebService/blob/development/.github/logo/Tingen-WebService-512x346.png" alt="Tingen Web Service">