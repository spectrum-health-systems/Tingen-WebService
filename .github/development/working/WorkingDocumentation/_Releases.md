<!-- 240430 -->

<div align="center">

![](../../.github/resources/images/logos/abatab-documentation-project-logo.png)

</div>

# Abatab releases

> Please follow the [Abatab versioning guide](../Versioning/Versioning.md).

There are four primary types of Abatab releases:

```mermaid
%%{init: {'theme':'dark'}}%%
flowchart LR
  development(Development) --> releaseCandidate(Release Candidate) --> stable(Stable) --> community(Community) 
```

## Development releases

An Abatab development release can be found in the [development branch](https://github.com/spectrum-health-systems/Abatab/tree/development) of the Abatab repository, and:

- May contain new and/or updated functionality
- May contain fixes for known issues
- May be partially - or completely! broken
- Is not intended for use in production environments

### Creating a new development release

When starting work on a new development release of Abatab:

1. Archive the previous development release  
   When starting work on development branch `24.04`, create a new branched named `24.03`, from the development branch.

2. Update `MAJOR.MINOR` version numbers  
   You will need to update the version numbers in the following locations:
   - Abatab.GetVersion() method  
   - Abatab settings file  
   - Abatab/README.md  
   - All Project AssemblyInfo.cs files

3. Update the `Abatab.asmx.cs` file header.

### Committing development releases

When committing a development branch to GitHub, you can just use the `BUILD` information.

ex.: `b240410`

## Release candidates

Abatab release candidates can be found in the [development branch](https://github.com/spectrum-health-systems/Abatab/tree/development) of the Abatab repository, and:

- Are "final" versions of the development branch
- Where regression testing takes place
- Are not intended for use in production environments

### Creating a new release candidate

Since release candidates are actually just a finalized development release, all you need to do is:

1. Update the `Abatab.asmx.cs` file header to include the full `MAJOR.MINOR.PATCH+TYPE-BUILD` version information with `rc#` as the type, where `#` increments by 1 for each release.

ex.: `24.04.0+rc1+b240410`

### Committing release candidate

When committing a development branch to GitHub, use the full `MAJOR.MINOR.PATCH+TYPE-BUILD` version information.  
ex.: `24.04.0+rc1+b240410`

## Stable releases

Abatab stable releases can be found in the [main branch](https://github.com/spectrum-health-systems/Abatab) of the Abatab repository, and:

- Have been fully tested
- Are not intended for use in production environments
- Have updated API documentation
- Are the foundation for Abatab [community releases](https://github.com/spectrum-health-systems/Abatab-Community-Release)

### Creating a stable release

Once you have confirmed a release candidate is stable: 

1. Update the `Abatab.asmx.cs` file header to include the full `MAJOR.MINOR.PATCH+TYPE-BUILD` version information with `stable` as the type.  
ex.: `24.04.0+stable+b240410`

2. Clean/rebuild Abatab

3. Copy `DocFX/site_` to `Abatab/docs`

4. Merge the development branch with the main branch.

## Stable releases

Abatab community releases can be found at the [Abatab Community Release](https://github.com/spectrum-health-systems/Abatab-Community-Release) repository of the Abatab repository, and:

- Have been fully tested
- Are not intended for use in production environments
- Have updated API documentation
- Are the foundation for Abatab [community releases](https://github.com/spectrum-health-systems/Abatab-Community-Release)

### Creating a community release

Abatab

1. Update the `Abatab.asmx.cs` file header to include the full `MAJOR.MINOR.PATCH+TYPE-BUILD` version information with `stable` as the type.  
ex.: `24.04.0+stable+b240410`

2. Clean/rebuild Abatab

3. Copy `DocFX/site_` to `Abatab/docs`

4. Merge the development branch with the main branch.


<br>
<br>

***

<!-- Abatab Documentation Project Footer -->

<br>

***

<div align="center">

This document is part of the
<b>[Abatab Documentation Project](https://github.com/spectrum-health-systems/Abatab-Documentation-Project)</b><br>

</div>


# Releasing a new Community Release

- Modify Abatab/README.md details
	- Change version
	- Remove Community Release note
- Modify Abatab/Abatab.asmx.cs header details
	- Change version
	- Remove Community Release note
- Modify AppData/Version.json (TODO)
