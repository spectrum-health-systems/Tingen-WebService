[Appendix](README.md) ❭ Avatar Script Parameter

<div align="center">

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="../../.github/logo/dark/256x173/Appendix.png">
    <source media="(prefers-color-scheme: light)" srcset="../../.github/logo/light/256x173/Appendix.png">
    <img alt="Fallback image description" src="../../.github/logo/light/256x173/Appendix.png">
  </picture>

  <h1>The Avatar Script Parameter</h1>

</div>

| CONTENTS |
|:---------|
| [What is the Avatar Script Parameter?](#what-is-the-avatar-script-parameter) |
| [Types](#types) |
| [Syntax](#syntax) |

## What is the Avatar Script Parameter?

The Avatar Script Parameter is a **required** string that is passed from Avatar, and tells the Tingen Web Service what action(s) to take.

## Types

There are two types of Script Parameters:

1. _FormSpecific
2. General

### Form-specific

If a Script Parameter starts with an underscore (`_`), the Tingen Web Service will route the request to a specific form.

The syntax for a form-specific Script Parameter is as follows:

```text
_ExactFormName-WhatYouWantToDo
```

For example, if the Script Parameter is `_OpenIncident-DoSomething`, the Tingen Web Service will route the request to `Module.OpenIncident` for further processing.

### General

If a Script Parameter does not start with an underscore (`_`), the Tingen Web Service will treat the Script Parameter as a command to be processed.

The syntax for a general Script Parameter is as follows:

```text
WhatYouWantToDo
```

For example, if the Script Parameter is `DoSomething`, the Tingen Web Service will route that command to the appropriate code for processing.

## Syntax

Here are the syntax rules for the Script Parameter:

* The Script Parameter cannot be null or empty.
* The Script Parameter cannot exceed 50 characters in length.
* The Script Parameter can only contain letters, numbers, underscores (`_`), and hyphens (`-`).
* Form- specific Script Parameters must start with an underscore (`_`)

Aside from that, the Script Parameter can be structured in any way that makes sense.

For example, the following are all valid Script Parameters:

* `_FormName-DoSomething`
* `_FormName-DoSomething-Else`
* `DoSomething`
* `DoSomething-Else-Please`

***

[Appendix](README.md) ❭ Avatar Script Parameter

<sub>Last updated: 260709</sub>
