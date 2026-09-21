[Appendix](README.md) ❭ Avatar OptionObjects

<div align="center">

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="../../.github/logo/dark/256x173/Appendix.png">
    <source media="(prefers-color-scheme: light)" srcset="../../.github/logo/light/256x173/Appendix.png">
    <img alt="Fallback image description" src="../../.github/logo/light/256x173/Appendix.png">
  </picture>

  <h1>The Avatar OptionObjects</h1>

</div>

| CONTENTS |
|:---------|
| [What is an Avatar OptionObject?](#what-is-an-avatar-optionobject) |
| [ScriptLink Standard](#scriptlink-standard) |
| [OptionObject instances](#optionobject-instances) |
| [Error codes](#error-codes) |

***

## What is an Avatar OptionObject?

An Avatar `OptionObject` defines the web service contract for use with AvatarNX.

You can think of it as a box that contains all of the information that is in a form. Avatar sends that box to the Tingen Web Service, and the Tingen Web Service:

1. Opens the box
2. Looks through the box for specific information
3. Uses that information to do something
4. Potentially modifies the contents
5. Makes sure everything is in order to be returned to Avatar
6. Closes the box



### ScriptLink Standard

The Tingen Web Service uses the [ScriptLink Standard](https://rcskids.github.io/ScriptLinkStandard/) class library to define the `OptionObject` and its properties.

You can read more about OptionObjects [here](https://rcskids.github.io/ScriptLinkStandard/api/scriptlinkstandard.objects/), and the specific `OptionObject2015` implementation in [here](https://rcskids.github.io/ScriptLinkStandard/api/scriptlinkstandard.objects/optionobject2015.html).

## OptionObject instances

 The Tingen Web Services uses three different OptionObjects:

1. **Original**  
The original OptionObject that is passed from myAvatar, and is *never* modified

2. **Worker**  
The working copy of the OptionObject that is used for processing, and *may* be modified

3. **Completed**  
The completed OptionObject that is returned to myAvatar

## Error Codes

When an OptionObject is returned to Avatar, it requires one of the following single-digit integer Error Codes:

| Error Code | Description                                                                                                 |
|:----------:|-------------------------------------------------------------------------------------------------------------|
| 0          | No action taken                                                                                             |
| 1          | Returns an Error Message with an **OK** button, and stops further processing of scripts                       |
| 2          | Returns an Error Message with **OK** and **CANCEL** buttons, and stops further scripts the user clicks "Cancel" |
| 3          | Returns an Error Message with an **OK** button                                                                   |
| 4          | Returns an Error Message with **YES** and **NO** buttons and stops further scripts the user clicks "NO"         |
| 5          | Returns a URL to be opened in a new browser                                                                 |
| 6          | Returns a form to be opened in Avatar                                                                       |

### Common Error Codes

The most common error codes are:

| Error Code | Use                                                                 |
|:----------:|---------------------------------------------------------------------|
| 1          | Stop a user from doing something                                    |
| 3          | Notify the user of something                                        |
| 4          | Warn the user about something, but give them the option to continue |

For example:

* If you wanted to stop a user from submitting an specific form without filling out a specific field, you would return **Error Code 1**, and a message that says "You must fill out the XYZ field before submitting this form."
* If you wanted to notify a user that they have successfully submitted a form, you would return **Error Code 3**, and a message that says "Your form has been successfully submitted."
* If you wanted to warn a user that they are about to delete an important record, you would return **Error Code 4**, and a message that says "Are you sure you want to delete this record? This action cannot be undone."

<br/>

***

[Appendix](README.md) ❭ Avatar OptionObjects

<sub>Last updated: 260709</sub>
