# Testing

1. Publish the Tingen Web Service

## The Web Services Testing form

- [ ] Open the form
- [ ] Click the "Admin" tab
- [ ] Check "Deploy"
    - [ ] Verify the framework was created
    - [ ] Verify the session folder was created
    - [ ] Verify that the AppData folder contains blueprints
    - [ ] Verify that regression tests completed
- [ ] Delete AppData folder
- [ ] Check "Refresh", verify
    - [ ] Verify that the AppData folder contains blueprints
- [ ] Delete the regression test data
- [ ] Check "test"
    - [ ] Verify that regression tests completed



## OpenIncident

* [ ] Delete the OpenIncident.config file and verify it is re-created.

* [ ] Set the Open Incident Module Mode to "disabled"
  * [ ] Open an existing incident that was created by another staff member
    * [ ] Verify messages do not appear

* [ ] Set the Open Incident Module Mode to "enabled"
  * [ ] Open a new incident
    * [ ] Verify the form can be submitted without issue
    * [ ] Verify the same incident can be re-opened and submitted

  * [ ] Open an existing incident that was created by another staff member
    * [ ] Verify an information message appears when the form opens
    * [ ] Verify a hard stop appears when submitting the form