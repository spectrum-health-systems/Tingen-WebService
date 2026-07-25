# Testing Initialization

## Step 1

1. Delete the contents of `Tingen_Data\WebService\UAT\*`

2. Execute the Tingen Web Service  
The following should be created:
- The framework
- The Blueprints
- The config files
- The history file

## Step 2

Wait a few minutes after completing Step 1.

1. Execute the Tingen Web Service 
2. Verify that the above information has not been re-created or modified unexpectedly

## Step 3

Wait a few minutes after completing Step 2.

1. Delete the history file
2. Execute the Tingen Web Service
3. Confirm only the history file has been re-created

## Step 4

Wait a few minutes after completing Step 3.

1. Delete a blueprint
2. Delete a config file
2. Execute the Tingen Web Service
3. Confirm that the blueprint **is not** recreated (that's only when the system is rebuilt)
4. Confirm that the config file **is** recreated (should happen whenever it's missing)
