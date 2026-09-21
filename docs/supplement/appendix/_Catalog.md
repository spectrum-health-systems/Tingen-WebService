<!--
This documentation is a work in progress.
The goal is to have this completed for R26.7
--->

[Development Manual](README.md) ❭ Catalogs

<div align="center">

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="../../.github/logo/dark/256x173/DevMan.png">
    <source media="(prefers-color-scheme: light)" srcset="../../.github/logo/Devlight/256x173/Man.png">
    <img alt="Fallback image description" src="../../.github/logo/Devlight/256x173/Man.png">
  </picture>

  <h1>Catalogs</h1>

</div>

***

> [!CAUTION]
> **This documentation is not complete!**

***

| CONTENTS |
|----------|
| |

***

Catalog information is pre-defined text for logging/data export.

Catalog information methods should use the expression body style, and string interpolation.

The strings that these methods return use Markdown syntax, which creates a carriage return when a line ends with two blank characters:

```csharp
$"**Mode:** {abSession.ModProgressNote.Mode}  {Environment.NewLine}"
                                            ^^
```

Removing the blank characters will break the Markdown output.

Example:

```csharp
public static string CatalogInformation() =>
    $"## Title{Environment.NewLine}" +
    $"**First thing:** {firstThing}  {Environment.NewLine}" +
    $"**Second thing:** {secondThing}  {Environment.NewLine}" +
    $"**Third thing:** {thirdThing.ValidOrderTypes}  {Environment.NewLine}";
}
```

<br/>

***

[Development Manual](README.md) ❭ Catalogs

<sub>Last updated: 260629</sub>
