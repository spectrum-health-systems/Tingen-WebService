⦁	Configuration files
Tingen is designed to be very configurable by end users, and achieves this through configuration files.
The C:\TingenData\%SystemCode%\Config\Tingen.config file contains the configuration settings for the Tingen web service.
Each module will have it’s own configuration file. For example, the Open Incident Module’s configuration file is C:\TingenData\%SystemCode%\Config\ModOpenIncident.config
3.1 Modes
The Tingen web service, and most Tingen Modules, have a “Mode” setting that determines if a specific module – or the entire web service – is enabled or disabled.
3.1.1. Enabling the Tingen web service
When you disable Tingen, the web service will not do any work, and will return any data Avatar has sent back to Avatar unmodified.
This is the equivalent of disabling all ScriptLink events on all forms that call Tingen, effectively turning the web service off.
To disable Tingen:
⦁	Open the “TingenData\LIVE\Tingen.config” file.

⦁	Find the following line:

“TingenMode”: “enabled”

⦁	Change “enabled” to “disabled”

⦁	Save the file.

3.1.2 Enabling the Tingen web service
When Tingen is enabled, the web service function normally.
To enable Tingen:
⦁	Open the “TingenData\LIVE\Tingen.config” file.

⦁	Find the following line:

“TingenMode”: “disabled”

⦁	Change “disabled” to “enabled”

⦁	Save the file.


3.1.3 Disabling a module
When you disable a module, that specific module will not do any work, and will return any data Avatar has sent back to Avatar unmodified.
Tingen, and other enabled modules, will function normally.
This is the equivalent of disabling all ScriptLink events on all forms that call the specific module that is being disabled, effectively turning that specific module off.
To disable a Tingen Module:
⦁	Open the “TingenData\LIVE\Tingen.config” file.

⦁	Find the module’s mode line:

Each Tingen Module will have a %ModuleName%Mode entry in Tingen.config.

For example, to disable the Open Incident Module, you would look for the following line:

"ModOpenIncidentMode": "enabled",

⦁	Change “enabled” to “disabled”

⦁	Save the file.

3.1.4 Enabling a module
When a module is enabled, that specific module will function normally.
To enable a Tingen Module:
⦁	Open the “TingenData\LIVE\Tingen.config” file.

⦁	Find the module’s mode line:

Each Tingen Module will have a %ModuleName%Mode entry in Tingen.config.

For example, to disable the Open Incident Module, you would look for the following line:

"ModOpenIncidentMode": "disabled",

⦁	Change “disabled” to “enabled”

⦁	Save the file.
