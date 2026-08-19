
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




  <img src="./.github/logo/Tingen-WebService-512x346.png" alt="Tingen Web Service">
  
  <img src="https://github.com/spectrum-health-systems/Tingen-WebService/blob/development/.github/logo/Tingen-WebService-512x346.png" alt="Tingen Web Service">