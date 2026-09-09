[Manual](README.md) ❭ Deploying the Tingen Web Service

<div align="center">

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="../../.github/logo/dark/256x173/Man.png">
    <source media="(prefers-color-scheme: light)" srcset="../../.github/logo/light/256x173/Man.png">
    <img alt="Fallback image description" src="../../.github/logo/light/256x173/Man.png">
  </picture>

  ![RELEASE](https://img.shields.io/badge/Release-26.9-teal)

<h1>Deploying the Tingen Web Service</h1>

</div>

| CONTENTS |
|:---------|
| [The Deployment Process](#the-deployment-process) |
| [Configuring the Tingen Web Service](#configuring-the-tingen-web-service) |

***

To deploy the Tingen Web Service, you'll need:

* A host machine with the necessary setup completed (web server, .NET SDK, folder structure)
* The latest Community Release of the Tingen Web Service

## The Deployment Process

Deploying the Tingen Web Service is fairly straightforward:

1. Download the latest [Community Release](NEED-LINK) of the Tingen Web Service
2. Extract the contents of the downloaded release to the `{HOST}:\Tingen_www\WebService\LIVE\` folder on the host machine.

You should now have the following folder structure on your host machine:

```text
{HOST}
|---Tingen_www\
    +---WebService\
        +---LIVE\
            ¦   TingenWebService.asmx
            ¦   Web.config
            +---AppData
                +---Blueprints
                +---OptObjErrorMessage
                +---TranslationTable
                +---XmlDocumentation
            +---bin
              +---AppData
                  +---Blueprints
                  +---OptObjErrorMessage
                  +---TranslationTable
              +---roslyn
            +---WebReferences
```

NEXT: [Configuring the Tingen Web Service](Configuring.md)

<br/>

***

[Manual](README.md) ❭ Deploying the Tingen Web Service

<sub>Last updated: 260709</sub>
