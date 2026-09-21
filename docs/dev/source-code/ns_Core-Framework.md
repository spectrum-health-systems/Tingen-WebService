[Source Code Documentation](README.md) ❭ ns:TingenWebService.Core.Framework

<div align="center">

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="../../../.github/logo/dark/256x173/SrcDoc.png">
    <source media="(prefers-color-scheme: light)" srcset="../../../.github/logo/light/256x173/SrcDoc.png">
    <img alt="Fallback image description" src="../../../.github/logo/light/256x173/SrcDoc.png">
  </picture>

  <h1>ns:TingenWebService.Core.Framework</h1>

</div>

***

> [!NOTE]
> See the API documentation for the source-level reference for this namespace.

***

## Overview

The `TingenWebService.Core.Framework` namespace contains the types that build and verify the Tingen Web Service runtime framework.

Its primary responsibility is to resolve the folder structure used during processing, including the server data tree and the server WWW tree, and to ensure required data folders exist before request handling continues.

These types are used early in the session lifecycle so downstream code can rely on consistent, session-scoped paths.

## Types in this namespace

| Type | Purpose |
|---|---|
| `TngnWsvcDataFolders` | Builds the full data-folder layout for a session, including `AppData`, `Blueprint`, `Config`, `Log`, `History`, `Session`, and related paths. |
| `TngnWsvcFramework` | Combines data and WWW folder structures and verifies the runtime folder state. |
| `TngnWsvcWwwFolders` | Builds the WWW-folder layout for a session, including the root, `bin\AppData`, and translation/blueprint locations. |
| `NamespaceDoc` | Documentation-only type used to describe the namespace in generated help output. |

## Key responsibilities

### `TngnWsvcDataFolders`

`TngnWsvcDataFolders` represents the server-side data paths used by the service.

It creates a predictable directory structure rooted at the configured server data path and scoped to the current Avatar system and session.

The structure supports:

- generated avatar data
- application data
- blueprint files
- configuration files
- exports and imports
- history and log files
- session-specific artifacts

### `TngnWsvcWwwFolders`

`TngnWsvcWwwFolders` represents the WWW-side folder structure for a session.

It resolves the web root for the current Avatar system and points to the `bin\AppData` location used by the web application for shared assets such as blueprints and translation tables.

### `TngnWsvcFramework`

`TngnWsvcFramework` coordinates the folder structures used by the service.

It:

- loads the data-folder layout
- loads the WWW-folder layout
- verifies the data folder structure
- creates missing directories when needed

This keeps the rest of the application from having to calculate or validate paths repeatedly.

## Framework flow

```mermaid
    flowchart TD
    A["Web.config values and session inputs"] --> B["TngnWsvcFramework.Load(...)"]
    B --> C["TngnWsvcDataFolders.Load(...)"] B --> D["TngnWsvcWwwFolders.Load(...)"]
    C --> E["Session data-folder layout"] D --> F["Session WWW-folder layout"]
    E --> G["TngnWsvcFramework.Verify(...)"]
    G --> H["Create missing data folders"] H --> I["Request processing can continue"]
```

## How the namespace is used

A typical framework flow is:

1. Runtime configuration provides the server paths and Avatar system.
2. `TngnWsvcSession` calls `TngnWsvcFramework.Load(...)`.
3. The framework loads both folder structures for the current session.
4. `TngnWsvcFramework.Verify(...)` ensures required data folders exist.
5. Later components use the resolved paths for logging, translation, blueprints, exports, and session files.

## Why this namespace matters

The framework namespace keeps path resolution centralized.

That helps the service:

- avoid hard-coded paths
- keep session data isolated by Avatar system and user
- create required folders consistently
- support maintenance and request processing with predictable storage locations

## Related namespaces

- `TingenWebService.Core.Configuration` — runtime configuration loading
- `TingenWebService.Core.TingenWsvcSession` — session bootstrap and state
- `TingenWebService.Core.Logger` — shared logging infrastructure

<br/>

***

[Source Code Documentation](README.md) ❭ ns:TingenWebService.Core.Framework

<sub>Last updated: 260709</sub>
