# Testing - After publishing a new release

## Step 1 of X

- [ ] Delete the contents of `Tingen_Data\WebService\UAT\*`
- [ ] Execute the Tingen Web Service

### Verify

- [ ] No errors are thrown
- [ ] The following folders are created in `Tingen_Data\WebService\UAT\`:
    * Blueprints
    * Config
    * Export
    * Import
    * Session
    * SysLog
    * TranslationTables
- [ ] The following files are created in `Tingen_Data\WebService\UAT\Blueprints\`:
    * CriticalErrorLog.blueprint
    * ErrorLog.blueprint
    * SessionLog.blueprint
- [ ] The following files are created in `Tingen_Data\WebService\UAT\Config\`:
    * TngnWsvc.config
- [ ] The `Tingen_Data\WebService\UAT\Session\YYMMDD\AvatarUserName\HHMMSS\` folder is created in
- [ ] A valid `AvatarUserName.session` file is created in `Tingen_Data\WebService\UAT\Session\YYMMDD\AvatarUserName\HHMMSS\`
- [ ] A valid `YYMMDD.start` file is created in `Tingen_Data\WebService\UAT\SysLog\`

## Step 2 of X

Wait a few minutes after completing Step 1.

- [ ] Execute the Tingen Web Service

### Verify

- [ ] No errors are thrown
- [ ] `Tingen_Data\WebService\UAT\` has not been modified
- [ ] `Tingen_Data\WebService\UAT\Blueprints\` has not been modified
- [ ] `Tingen_Data\WebService\UAT\Config\` has not been modified
- [ ] The `Tingen_Data\WebService\UAT\Session\YYMMDD\AvatarUserName\HHMMSS\` folder is created in
- [ ] A valid `AvatarUserName.session` file is created in `Tingen_Data\WebService\UAT\Session\YYMMDD\AvatarUserName\HHMMSS\`
- [ ] `Tingen_Data\WebService\UAT\SysLog\` has not been modified

## Step 3

Wait a few minutes after completing Step 2.

- [ ] Delete the `Tingen_Data\WebService\UAT\SysLog\YYMMDD.start` file
- [ ] Execute the Tingen Web Service

### Verify

- [ ] No errors are thrown
- [ ] A valid `YYMMDD.start` file is created in `Tingen_Data\WebService\UAT\SysLog\`

## Step 4

Wait a few minutes after completing Step 3.

- [ ] Delete a blueprint
- [ ] Delete a config file
- [ ] Execute the Tingen Web Service
- [ ] Confirm that the blueprint **is not** recreated (that's only when the system is rebuilt)
- [ ] Confirm that the config file **is** recreated (should happen whenever it's missing)
