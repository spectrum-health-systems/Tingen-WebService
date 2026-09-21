[The Tingen Web Service Manual](../README.md) ❭ [MDAC documentation](README.md) ❭ Create a Custom Web Service

<div align="center">

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="../../../.github/logo/Tingen-WebService-Manual-Logo-Dark-256x256.png">
    <source media="(prefers-color-scheme: light)" srcset="../../../.github/logo/Tingen-WebService-Manual-Logo-Light-256x256.png">
    <img alt="Fallback image description" src="../../../.github/logo/Tingen-WebService-Manual-Logo-Light-256x256.png">
  </picture>

  <h1>Create a custom web service for AvatarNX™</h1>

</div>

| CONTENTS&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; |
|:---------------------------------------------------------------------------------------------------------------------------------------------|
| [Requirements](#requirements)                                                                       |
| [Setup Visual Studio](#setup-visual-studio)                                                         |
| [Create a new ASP.NET Web Application project](#create-a-new-aspnet-web-application-project)        |
| [Setup required project components](#setup-required-project-components)                             |
| &nbsp;&nbsp;&nbsp;&nbsp;[The ASMX Web Service component](#the-asmx-web-service-component)           |
| &nbsp;&nbsp;&nbsp;&nbsp;[The ScriptLinkStandard component](#the-scriptlinkstandard-component)       |
| [Create required methods](#create-required-methods)                                                 |
| &nbsp;&nbsp;&nbsp;&nbsp;[The `HelloWorld()` method](#the-helloworld-method)                         |
| &nbsp;&nbsp;&nbsp;&nbsp;[The `GetVersion()` method](#the-getversion-method)                         |
| &nbsp;&nbsp;&nbsp;&nbsp;[The `RunScript()` method](#the-runscript-method)                           |
| &nbsp;&nbsp;&nbsp;&nbsp;[The `MethodName()` method](#the-methodname-method)                         |
| [Add the NTST.ScriptLinkService.Objects namespace](#add-the-ntstscriptlinkserviceobjects-namespace) |
| [Completed code](#completed-code)                                                                   |

***

## Requirements

* [**Visual Studio Community 2022**](https://visualstudio.microsoft.com/vs/)<br>
Technically you can use any IDE/text editor to write custom web services, but the Visual Studio IDE makes some of the necessary setup really easy.

* [**.NET Framework 4.8.x**](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)<br>
Unfortunately .NET 6 - the current version of the .NET platform - does not support web services built on the SOAP messaging protocol that myAvatar™ uses. Microsoft's .NET Framework does, however, so we will be using .NET Framework 4.8 to build our custom web service.

* **An understanding of the C# programming language**<br>
I write my web services in C#, but you can use whichever language you would like to write your custom 
web service (assuming it supports the SOAP messaging protocol).

* **A place to host your web service that your myAvatar™ environments have access to** (via HTTPS)

## Setup Visual Studio

> [!NOTE]
> This documentation assumes you are using Visual Studio Community 2022. Newer versions of Visual Studio may also work, but have not been tested.

If Visual Studio Community 2022 isn't installed on your system, you'll need to download the [Visual Studio installer](https://visualstudio.microsoft.com/vs/), then launch the Visual Studio installer and install the required workloads and components

* Workloads:

  * ASP.NET and web development
  * Azure development
  * .NET desktop development

* Components:

  * .NET Framework 4.8 SDK
  * .NET Framework 4.8 targeting pack
  * .NET Framework project and item templates

## Create a new ASP.NET Web Application project

When Visual Studio 2022 launches, you should see the "Get Started" page.

1. Click **Create a new project**.

More specifically, we are going to create an *ASP.NET Web Application (.NET Framework)* project.

The easiest way to do this is to:

2. Leave the language dropdown as `All languages`
3. Leave the platform dropdown as `All platforms`
4. Leave the project type dropdown as `All project types`
5. In the search bar type `"ASP.NET Web Application (.NET Framework)"`

The first option should be `C# ASP.NET Web Application (.NET Framework)`

Then:

6. Click `C# ASP.NET Web Application (.NET Framework)`, and you will notice it becomes highlighted
7. Click **Next**

Which will bring up the configuration page, where you should:

8. Change the Project name to the name of your project (e.g., "CustomAvatarWebService")
9. Verify that the Location where you want your sourcecode is correct
10. Verify that the framework is `.NET Framework 4.8`
11. Click **Create**
12. On the Create a new ASP.NET Web Application window, choose **Empty**
13.Click **Create**.

Visual Studio will take a few minutes to create your new ASP.NET Web Application, and when it complete you will be presented with this beaut of a screen:

## Setup required project components

### The ASMX Web Service component

1. Right-click on your *project* (e.g., "CustomAvatarWebService")
2. Choose `Add > New item`.
3. Go to `Visual C# > Web`
4. Choose **Web Service (ASMX)**
5. Name the web service (e.g. "CustomAvatarWebService.asmx")
6. Click **Add**.
7. Right-click on the ASMX Web Service you added (e.g, "CustomAvatarWebService.asmx")
8. Set the ASMX (Web Service) as the start page for your project.
9. Click **Set as Start Page**

### The ScriptLinkStandard component

Your project also needs the [ScriptLinkStandard](https://rcskids.github.io/ScriptLinkStandard/) project to function.

To do this:

1. Create a new Solution Folder for third party components
2. Create a new folder called "ThirdParty/" in your project, then copy the ScriptLinkStandard project to ThirdParty/
3. Right click the ThirdParty/ Solution Folder and click "Add > Existing Project", then add the ScriptLinkStandard.csproj file.
4. Right click on your *project* , choose `Add > Reference`, and choose ScriptLinkStandard.

## Create required methods

Custom web services that interface with myAvatar™ ***require*** the following two methods to be present:

* `GetVersion()`
* `RunScript()`

***If these methods aren't present, your web service won't work!***

For the purposes of this documentation, I'm going to assume you named your project "CustomAvatarWebService".

Your shiny, brand new *CustomAvatarWebService.asmx.cs* file should look like this:

```csharp
using System.Web.Services;

namespace CustomAvatarWebService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CustomAvatarWebService : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
```

### The `HelloWorld()` method

We don't need the `HelloWorld()` method, so you can remove it.

### The `GetVersion()` method

The `GetVersion()` method simply returns the version of your custom web service, and it looks like this:

```csharp
[WebMethod]
public string GetVersion()
{
    return "VERSION 1.0";
}
```

Just copy that code and paste it where the `HelloWorld()` method used to be.

Now *CustomAvatarWebService.asmx.cs* should look like this:

```csharp
using System.Web.Services;

namespace CustomAvatarWebService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CustomAvatarWebService : System.Web.Services.WebService
    {
        [WebMethod]
        public string GetVersion()
        {
            return "VERSION 1.0";
        }
    }
}
```

### The `RunScript()` method

The `RunScript()` method is what myAvatar™ calls via a ScriptLink event in a form, and looks like this:

```csharp
[WebMethod]
public OptionObject2015 RunScript(OptionObject2015 sentOptionObject, string action)
{
    switch(action)
    {
        case "doSomething":
            return MethodName(sentOptionObject);
        
        default:
            break;
    }
    
    return sentOptionObject;
}
```

This method:

* Accepts an OptionObject2015 sent from myAvatar™
* Does something with the data within that object*
* Returns a modified object* to myAvatar™.

### The `MethodName()` method

When myAvatar™ reaches out to your custom web service, it's going to ask it to perform some kind of *action*.

You'll notice in the `RunScript()` code above there is an `action` parameter that is passed. That is the *action* myAvatar™ is requesting (via a ScriptLink *paramater* in myAvatar™).

For this tutorial, let's pretend that the action myAvatar™ is requesting is to "doSomething".

You'll see that the switch statement has a case for "DoSomething", and that case calls the `MethodName()` method. So when myAvatar™ requests that our web services "DoSomething", the code in `MethodName()` will run.

A more real-world example would be myAvatar™ requesting a "CheckDate" action be performed, and our web service would then execute the code in a method named `CheckTheDatePlease()`.

The `RunScript()` method  in that example would look like this:

```csharp
[WebMethod]
public OptionObject2015 RunScript(OptionObject2015 sentOptionObject, string action)
{
    switch(action)
    {
        case "CheckDate":
            return CheckTheDatePlease(sentOptionObject);

        default:
            break;
    }

    return sentOptionObject;
}
```

And then we would have a method called `CheckTheDatePlease()` that would do whatever date checking you need, via code that you write.

For this tutorial, we are just going to create a method called `MethodName()`, which looks like this:

```csharp
private static OptionObject2015 MethodName(OptionObject2015 sentOptionObject)
{
    // You'll write the logic you need here.
    return new OptionObject2015();
}
```

Copy the `MethodName()` method code above, and paste it below the `RunScript()` method.

## Add the NTST.ScriptLinkService.Objects namespace

At this point, you probably have some warnings in your code in the form of red underlines. Most likely you are getting these warnings under the text for `OptionObject2015` and `MethodName`. You are getting these warnings becuase your project doesn't know what `OptionObject2015` and `MethodName` are. 

To fix this, find the this line at the very top of your asmx file:

```csharp
using System.Web.Services;
```

Then add the following line below that:

```csharp
using NTST.ScriptLinkService.Objects;
```

## Completed code

Now the *CustomAvatarWebService.asmx.cs* file is **complete**, and should look like this:

```csharp
using System.Web.Services;
using NTST.ScriptLinkService.Objects;

namespace CustomAvatarWebService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CustomAvatarWebService : System.Web.Services.WebService
    {
        [WebMethod]
        public string GetVersion()
        {
            return "VERSION 1.0";
        }

        [WebMethod]
        public OptionObject2015 RunScript(OptionObject2015 sentOptionObject, string action)
        {
            switch(action)
            {
                case "DoSomething":
                    return MethodName(sentOptionObject);
                
                default:
                    break;
            }
            
            return sentOptionObject;
        }

        private static OptionObject2015 MethodName(OptionObject2015 sentOptionObject)
        {
            return new OptionObject2015();
        }
    }
}
```

<br/>

***

[The Tingen Web Service Manual](../README.md) ❭ [MDAC documentation](README.md) ❭ Create a Custom Web Service

<sub>Last updated: 260610</sub>
