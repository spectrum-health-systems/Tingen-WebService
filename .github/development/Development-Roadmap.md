<div align="center">

  <h1>Tingen Web Service: Development - Roadmap</h1>

</div>

| CONTENTS |
| [Code](#code) |
| [Features](#features) |

***

- [ ] Rename `Dependencies/`
- [ ] Migrate to `Du`

## Code

- [ ] Add error handling to where needed (e.g., "try...catch")
- [ ] Review web.config and remove unnecessary settings
- [ ] As many internals/private methods as possible
- [ ] Fix "Value cannot be null" error when starting fresh
- [ ] SQL query user name/password changes
- [ ] Preprocessor directives?
- [ ] Cleanup `.csproj` file to remove unnecessary references and files.
- [ ] Re-org web.config setting order (use the table in Configuration.md)
- [ ] Have one Netsmart Query web service, instead of two (one for UAT, one for LIVE).

## Features

- [ ] Open Incident report functionality complete
  - [ ] Open Incident report thing - Pt name, involved, etc.
- [ ] Logging
  - [ ] Better logging functionality in regards to preset messages
  - [ ] Better time formatting for log files

## Framework

- [ ] Move the debug stuff from .development\Debug\

### Testing

- [ ] Testing functionality complete
- [ ] Verify error message codes match
- [ ] Test upper/lowercase script parameter parse
- [ ] Verify `throw` works in StartApp()
- [ ] Check AvatarOptioObject.Export() to see if the example looks right
- [ ] Verify that text session logs are created, then disable. (?)

## Documentation

### Development documentation

- [ ] Note about how Primeval logs only use the `returned[1]` value.
- [ ] How to reset everything using the daily file
- [ ] How to comment/uncomment primeval logs in source code files
- [ ] Trace log levels
    ```
    0 - No trace logs
    1 - Method entry
    2 - Method exit
    3 - Block entry
    4 - Block sub-entry
    5 - Block exit
    6 - Special case
    7 - Not used
    8 - Not used
    9 - Debugging (all trace logs)
    ```

### Project documentation


### Source code documentation

- [ ] XML documentation
  - [ ] Review XML documentation and modify/remove `<example>` blocks as needed.
  - [ ] Add links to Class documentation/Appendix in the XML documentation where relevant.

### Testing documentation

- [ ] Setup tests for TingenWebService.asmx.CriticalErrorOccurred() to make sure it's catching everything/functioning correctly.

## Other

### GitHub

- [ ] Is there a way to ignore non-API documentation with Github pages?
