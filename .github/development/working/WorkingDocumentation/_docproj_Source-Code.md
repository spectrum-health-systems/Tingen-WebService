

# Logging

## Debuggler statement

Debuggler statements should be used sparingly, and only at the top of a `method()` where debugging is needed and a Trace Log is not possible. Debuggler statements should also be separated from the rest of the method by a blank line.

```csharp
public static void MethodOne(AbSession abSession)
{
    Debuggler.DebugLog(Settings.Default.DebugglerMode, Assembly.GetExecutingAssembly().GetName().Name);

    var numberOne = 1;
    var numberTwo = 2;
    var total = numberOne + numberTwo;
}
```

## Trace Logs

Trace logs should be used:

- in all methods
- in all if...then...else blocks
- in all switch case blocks
- to debug specific code

### Methods

Trace log statements should be at the top of each `method()`, separated from the rest of the method by a blank line.

```csharp
public static void DoThing(AbSession abSession)
{
    LogEvent.Trace("trace", abSession, AssemblyName);

    var numberOne = 1;
    var numberTwo = 2;
    var total = numberOne + numberTwo;
}
```



## Inline code

Inline Trace Log statements should be used sparingly, when you need to debug a specific line/block of code.

When a Trace Log statement is added to code that you need to debug, the statement should follow the line of code directly, and should not be followed by a blank line.

```csharp
public static void ParseCommand(AbSession abSession)
{
    LogEvent.Trace("trace", abSession, AssemblyName);

    var numberOne = 1;
    LogEvent.Trace("trace", abSession, AssemblyName);
    var numberTwo = 2;
    LogEvent.Trace("trace", abSession, AssemblyName);
    var total = numberOne + numberTwo;
    LogEvent.Trace("trace", abSession, AssemblyName);
}
```





# Return statements

## Methods

Return statements at the end of a `method()` should be separated from that block by a blank line.

```csharp
public static string DoThing(AbSession abSession)
{
    LogEvent.Trace("trace", abSession, AssemblyName);

    var numberOne = 1;
    var numberTwo = 2;
    var total = numberOne + numberTwo;

    return total;
}
```


# Catalog information

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


<br>

***

<div align="center">

  This document is part of the [Abatab Documentation Project](../Abatab%20Documentation%20Project.md).

  <h5>
    Last updated: May 30, 2023
  </h5>

</div>
