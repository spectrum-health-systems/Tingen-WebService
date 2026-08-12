<div align="center">

  <h1>Tingen Web Service: Development - Roadmap</h1>

</div>

***

## R26.8

- Add error handling to where needed
- Review web.config and remove unnecessary settings
- All XML documentation complete
- Testing functionality complete
- Testing documentation complete
- Open Incident report functionality complete
- As many internals/private methods as possible
- Verify error message codes match
- Test upper/lowercase script parameter parse
- Fix "Value cannot be null" error when starting fresh


## Next

* Better logging functionality in regards to preset messages
* better time formatting for log files
* SQL query user name/password
* Open Incident report thing - Pt name, involved, etc.
* Preprocessor directives?
* Move the debug stuff from .development\Debug\
* Setup tests for TingenWebService.asmx.CriticalErrorOccurred() to make sure it's catching everything/functioning correctly.
* Review XML documentation and modify/remove `<example>` blocks as needed.
* Cleanup `.csproj` file to remove unnecessary references and files.
* Re-org web.config setting order (use the table in Configuration.md)
* Have one Netsmart Query web service, instead of two (one for UAT, one for LIVE).