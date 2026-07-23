<!--
    260527
    Tingen_www
-->

***

<div align="center">

# SHS-AZU-NSWS-01

</div>

`SHS-AZU-NSWS-01` is a Spectrum Health Systems server that hosts the following:

* [Tingen Web Service](https://github.com/spectrum-health-systems/tingen-webservice)
* [Tingen Transmorger](https://github.com/spectrum-health-systems/Tingen-Transmorger)

There are two shares on this server:

* [Tingen_Data](#shs-azu-nsws-01tingen_data) - Data for all hosted Tingen projects
* [Tingen_www](#shs-azu-nsws-01tingen_www) - LIVE and UAT instances of the Tingen Web Service  **⇦ YOU ARE HERE**

## SHS-AZU-NSWS-01/Tingen_www/

This share contains the [Tingen Web Service](https://github.com/spectrum-health-systems/Tingen-WebService) instances.

### [`./`](./)

* [`./web.config`](web.config) - The configuration file for the server. This file should not be modified without proper authorization.

### [`./Resources/`](Resources)

* [`./Resources/Logo/`](Resources/Logo) - Logos for documentation.
* [`./Resources/Workspaces/`](Resources/Workspaces) - Visual Studio workspaces.
* [`./Resources/WorkingDocuments/`](Resources/WorkingDocuments) - Working documents for the Tingen Web Service.

### [`./WebService/`](WebService)

Contains the following instances of the Tingen Web Service:

#### [`./WebService/LIVE/`](WebService/LIVE) ([WSDL](https://shs-azu-nsws-01.spectrumhealthsystems.org/WebService/LIVE/TingenWebService.asmx?WSDL))

* [`./WebService/LIVE/bin/`](WebService/LIVE/bin) - Dependencies for the *LIVE* instance of the Tingen Web Service.
* [`./WebService/LIVE/TingenWebService.asmx`](WebService/LIVE/TingenWebService.asmx) - The API endpoint for the *LIVE* instance of the Tingen Web Service.
* [`./WebService/LIVE/Web.config`](WebService/LIVE/Web.config) - The configuration file for the *LIVE* instance of the Tingen Web Service.

#### [`WebService/UAT/`](WebService/UAT) ([WSDL](https://shs-azu-nsws-01.spectrumhealthsystems.org/WebService/UAT/TingenWebService.asmx?WSDL))

* [`./WebService/UAT/`](WebService/UAT) - The *UAT* instance of the Tingen Web Service.
* [`./WebService/UAT/bin/`](WebService/UAT/bin) - Dependencies for the *UAT* instance of the Tingen Web Service.
* [`./WebService/UAT/TingenWebService.asmx`](WebService/UAT/TingenWebService.asmx) - The API endpoint for the *UAT* instance of the Tingen Web Service.
* [`./ebService/UAT/Web.config`](WebService/UAT/Web.config) - The configuration file for the *UAT* instance of the Tingen Web Service.
