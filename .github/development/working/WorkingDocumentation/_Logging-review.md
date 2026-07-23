# DEVELOPMENT NOTES - Logging

> Last updated 1-27-23

## Logging

* How to enable/disable log event types
* Public methods have debug logs at the beginning
* Public methods have trace logs
* Private methods have trace logs
* Logging is done a little differently in AbatabLogging.BuildContent()


### Debug logs

* Located:
    - At the top of public methods.
    - Immediately following the statement you want to trace (not separated by a blank line)
* Debug logs with "[DEBUG]" are part of the source code, and should not be removed.
* Debug logs without "[DEBUG]" temporary.
* Debug logs have a 100ms delay, and will have a noticeable affect on performance.
* DebuggingDebug is only for debugging/development/troubleshooting log functionality(?)
* DebuggingDebug logs have a 1000ms delay, and will severly impact performance.
* Debug logs should not be enabled in production
* Cases where paramaters are hardcoded

### Trace logs

* Located:
    - First statement in a private method, second statement in a public method (behind debug statement)
    - Last statement in public/private methods (or last statement before return)
    - At the top of case statements
    - At the top of if...else statements
    - After assignments that aren't generated via a method
* Trace statements generally do not follow methods that have a trace event at the top, unless there is no trace event at the top (see: maintenance)
* Trace statement generally do not have any unique messages, and look like this:
```
LogEvent.Trace(abatabSession, Assembly.GetExecutingAssembly().GetName().Name);
```
* Trailing trace logs have a logMsg that desribes why they are there/what they are doing.

* Intended to show the flow of code, usually used during development
* Should not be enabled in production
* Trace logs are generally found at the beg/end of methods, and not in sub-methods
* Cases where paramaters are hardcoded