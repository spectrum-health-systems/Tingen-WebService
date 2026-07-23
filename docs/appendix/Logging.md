[Appendix](README.md) ❭ Logging

<div align="center">

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="../../.github/logo/dark/256x173/Appendix.png">
    <source media="(prefers-color-scheme: light)" srcset="../../.github/logo/light/256x173/Appendix.png">
    <img alt="Fallback image description" src="../../.github/logo/light/256x173/Appendix.png">
  </picture>

  <h1>Logging</h1>

</div>

| CONTENTS |
|:---------|
| [What is Tingen Web Service logging?](#what-is-tingen-web-service-logging) |
| [Types of logs](#types-of-logs) |
| [Trace logs](#trace-logs) |


***

## What is Tingen Web Service logging?

Tingen Web Service logging is a mechanism used to record information about the operation of the web service. It helps in monitoring, debugging, and troubleshooting by capturing events, errors, and other significant activities.

## Types of logs

Tingen Web Service generates the following types of logs:

* Trace logs
* Error logs
* Debug logs

## Trace logs

Trace logs record detailed information about the execution flow of the web service, which is useful for debugging and troubleshooting.

### Where to use trace logs

Trace logs should be used:

* in all methods (at the top)
* in all if...then...else blocks
* in all switch case blocks
* to debug specific code

### Trace log sytax

TBD

### Trace log limit

Description here.

### Trace log call example

```csharp
// Example of a trace log call
```

***

## Error logs


***

## Debug logs

Debug logs are written to:

`Tingen_Data\.development\debug\`

To write a debug simple debug log:

`LogEvent.Debug();`

To write a debug log with a message:

`LogEvent.Debug("This is a debug message.");`

<br/>

***

[Appendix](README.md) ❭ Logging

<sub>Last updated: 260709</sub>

<!--
* Trace log limit

-->