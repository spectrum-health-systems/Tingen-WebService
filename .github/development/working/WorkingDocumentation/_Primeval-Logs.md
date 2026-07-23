# Logger

## Types of logs

### Primeval

Primeval logs

- Are very basic logs
- Are intended for development only
- Can be created with very little, if any, information
- Are used where other types of logs aren't available
- Are always written to "C:\AbatabData\Primeval\"

Primeval logs can be used before Abatab does any setup, and don't require anything other than the existance of "C:\Abatab\Primeval\", so they are useful when developing Abatab.

For the most part, primeval log syntax can be removed once they have been used to debug something, but you will find a few primeval log commands in the source code, just commented out.

Primeval logs use the following naming convention:

&nbsp;&nbsp;&nbsp;&nbsp;**yyyMMdd-HHmmssfffffff.primeval_log**

For example:

&nbsp;&nbsp;&nbsp;&nbsp;**240516-1021341553252.primeval_log**

#### Examples

* `LogEvent.Primeval()`  
  This will create a primeval log with the contents of "Abatab primeval log".

* `LogEvent.Primeval("[START]")`  
  This will create a primeval log with the contents of "[START]".
