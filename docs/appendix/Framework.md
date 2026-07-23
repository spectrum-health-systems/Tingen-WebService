[Appendix](README.md) ❭ The Tingen Web Service Framework

<div align="center">

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="../../.github/logo/dark/256x173/Appendix.png">
    <source media="(prefers-color-scheme: light)" srcset="../../.github/logo/light/256x173/Appendix.png">
    <img alt="Fallback image description" src="../../.github/logo/light/256x173/Appendix.png">
  </picture>

  <h1>The Tingen Web Service Framework</h1>

</div>

| CONTENTS |
|:---------|
| [What is the Tingen Web Service Framework?](#what-is-the-tingen-web-service-framework) |
| [Tingen_Data\\](#tingen_data) |
| [Tingen_www\\](#tingen_www) |

***

## What is the Tingen Web Service Framework?

The Tingen Web Service Framework is a set of files and folders that are required for the Tingen Web Service to function properly.

The framework consists of the following folders:

* `Tingen_Data\`
* `Tingen_www\`

## Tingen_Data\

The `Tingen_Data\` folder contains *data* for the Tingen Web Service, including (but not limited to!):

* Log files
* Debug information
* Templates
* Configuration files
* Avatar generated data

> [!IMPORTANT]
> Currently, this folder structure must be created manually, prior to deploying the Tingen Web Service. Eventually this will be automated.

The structure of the `Tingen_Data\` folder is as follows:

| {host}        |   |
|:--------------|---|
| ├─ index.html | Informational purposes only |
| ├─ README.md | Informational purposes only |
| ├─ .development\ | Tingen project development data/resources |
| ├─ .resources\ | Resources for index.html and README.md |
| └─ WebService\ | Tingen Web Service **data** |
| &nbsp;&nbsp;&nbsp;&nbsp;├─ AvatarGeneratedData\ | Tingen Avatar Generated Data (not covered in this documentation) |
| &nbsp;&nbsp;&nbsp;&nbsp;└─ LIVE\ | Tingen Web Service LIVE environment **data** |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;├─ AppData\ | LIVE instance **data** |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;│&nbsp;&nbsp;&nbsp;&nbsp;├─ Blueprint\ | Blueprints |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;│&nbsp;&nbsp;&nbsp;&nbsp;├─ Config\ | Configuration files |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;│&nbsp;&nbsp;&nbsp;&nbsp;├─ Export\ | Exported data |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;│&nbsp;&nbsp;&nbsp;&nbsp;├─ History\ | Historical data |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;│&nbsp;&nbsp;&nbsp;&nbsp;├─ Import\ | Imported data |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;│&nbsp;&nbsp;&nbsp;&nbsp;├─ Log\ | Logs |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;│&nbsp;&nbsp;&nbsp;&nbsp;├─ OptObjErrorMessage\ | OptionObject error message templates |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;│&nbsp;&nbsp;&nbsp;&nbsp;├─ TranslationTable\ | Translation tables |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;│&nbsp;&nbsp;&nbsp;&nbsp;└─ www\ | ? |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;├─ Archive\ | Archived data |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;├─ Session\ | Session data |

> [!TIP]
> For additional details about the `Tingen_Data\` folder, see the [API documentation](https://spectrum-health-systems.github.io/Tingen-WebService/api/html/d2e2e77f-4c48-0ffb-63f1-6c1b60c112c6.htm).

## Tingen_www\

The `Tingen_www\` folder contains the actual **Tingen Web Service instance**.

This folder is automatically created when you deploy (or if you are developing, when you publish) the Tingen Web Service.

> [!WARNING]`
> With the exception of the `web.config` file, **do not modify** any files or folders within the `Tingen_www\` directory!

The structure of the `Tingen_www\` folder is as follows:

| {host}        |   |
|:--------------|---|
| ├─ index.html | Informational purposes only |
| ├─ README.md | Informational purposes only |
| ├─ web.config | Tingen project development data/resources |
| ├─ .resources\ | Resources for index.html and README.md |
| &nbsp;&nbsp;&nbsp;&nbsp;└─ LIVE\ | Tingen Web Service LIVE **instance** |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;├─ TingenWebService.asmx | The Tingen Web Service endpoint |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;├─ web.config | The Tingen Web Service configuration file |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;├─ AppData\ | LIVE instance resources (?) |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;├─ bin\ | Tingen Web Service binary files |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;│&nbsp;&nbsp;&nbsp;&nbsp;├─ AppData\ | LIVE instance resources (?) |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;│&nbsp;&nbsp;&nbsp;&nbsp;│&nbsp;&nbsp;&nbsp;&nbsp;├─ Blueprints\ | Blueprints |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;│&nbsp;&nbsp;&nbsp;&nbsp;│&nbsp;&nbsp;&nbsp;&nbsp;├─ OptObjErrorMessage\ | OptionObject error message templates |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;│&nbsp;&nbsp;&nbsp;&nbsp;│&nbsp;&nbsp;&nbsp;&nbsp;└─ TranslationTable\ | Translation tables |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;│&nbsp;&nbsp;&nbsp;&nbsp;├─ roslyn\ | Roslyn compiler files |
| &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;├─ Web References\ | TBD |

> [!TIP]
> For additional details about the `Tingen_www\` folder, see the [API documentation](https://spectrum-health-systems.github.io/Tingen-WebService/api/html/63930f63-1fd5-3280-5289-230a969d0433.htm).

<br/>

***

[Appendix](README.md) ❭ The Tingen Web Service Framework

<sub>Last updated: 260709</sub>
