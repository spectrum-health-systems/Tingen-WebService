# DEVELOPMENT NOTES - Logging

> Last updated 2-23-23

# Primeval logs

## What is a primeval log

Primeval logs:

* Are used in development/testing only, and should not be used in production environments.
* Have a hardcoded path (currently *c:\AbatabData\Testing*).
* When a primeval log is created, it overwrites existing primeval logs with the same name.
* Have a **.primeval** extention.
* May not have content, since all necessary information is in the filename.

## When to create a primeval log

Primeval logs should be created:

* When standard logging functionality won't work.

## Primeval log syntax

To create a primeval log *without* content, use the following syntax:

```
if (webConfigContents["DebugMode"] == "enabled")
{
    LogEvent.Primeval(@"C:\AbatabData\Testing\", Assembly.GetExecutingAssembly().GetName().Name);
}
```

To create a primeval log *with* content, use the following syntax:

```
if (webConfigContents["DebugMode"] == "enabled")
{
    LogEvent.Primeval(@"C:\AbatabData\Testing\", Assembly.GetExecutingAssembly().GetName().Name, "Content goes here");
}
```

You can also create a one-off primeval log using, with or without content, using the following syntax (but be sure to comment this out in production environments):

```
LogEvent.Primeval(@"C:\AbatabData\Testing\", Assembly.GetExecutingAssembly().GetName().Name);
LogEvent.Primeval(@"C:\AbatabData\Testing\", Assembly.GetExecutingAssembly().GetName().Name, "Content goes here");
```

## Enabling/disabling primeval logs

To **enable** primeval logs, set the "DebugMode" to `enabled`.  
To **disable** primeval logs, set the "DebugMode" to `disabled`.

Please note that one-off primeval logs are not affected by the DebugMode setting, and are disabled by commenting them out.

# Trace logs

## What is a trace log

Trace logs:

* Are used to deterimine programatic flow
* Are generally used in testing/development, but can be used in production (but probably shouldn't).
* Have a **.trace** extention.
* Do not have content since all necessary information is in the filename.

## When to create a trace log

Trace logs should be created:

* At the start of a method
* At the start of a case statement
* At the start of if...then...elseif...else statements

## Trace log syntax

To create a trace log, use the following syntax:

```
LogEvent.Trace(sessionDetail, Assembly.GetExecutingAssembly().GetName().Name);
```

## Enabling/disabling trace logs

To **enable** trace logs, set the "LoggerMode" to `all` or include `-trace-`.  
To **disable** trace logs, set the "LoggerMode" to `none`.