[The Tingen Web Service Manual](README.md) ❭ Testing

<div align="center">

  <picture>
    <source media="(prefers-color-scheme: dark)" srcset="../../../.github/logo/Tingen-WebService-Development-Manual-Logo-Dark-256x256.png">
    <source media="(prefers-color-scheme: light)" srcset="../../../.github/logo/Tingen-WebService-Development-Manual-Logo-Light-256x256.png">
    <img alt="Fallback image description" src="../../../.github/logo/Tingen-WebService-Development-Manual-Logo-Light-256x256.png">
  </picture>

  <h1>Testing</h1>

</div>

---

### CONTENTS

* [Open Incident Report](#openincidentreport)<br>


---

## Open Incident Report

### Preparation - Avatar users

You will need two users to perform these tests:

**TESTWSVCA**  
User ID: TESTWSVCA
User Description: Test WebserviceA
User Role: SuperUsers

**UserB**
User ID: TESTWSVCB
User Description: Test Web'service-B
User Role: NXOPNURSE

### Preparation - Translation table

You will also need to add the following to the beginning of "USERID_UserDescription_Active.translation":

```text
TESTWSVCA^Test WebserviceA
TESTWSVCB^Test Web'service-B
```

### Testing the ***Program of Incident*** field

Staff should not be able to submit a new incident without choosing a value for the **Program of Incident** field.

Currently the title of the field is red, but is not technically required.

#### Expected outcome

The web service should display a message informing the user they cannot submit the form until the *Program of Incident* field is completed.

#### Testing procedure

1. Login to Avatar with `TESTWSVCB`

2. Open the `Open Incident` form

3. Type "40" in the search bar

4. Click the `New Incident` button

A new, blank Open Incident form should open

5. Complete the following required fields:

* Brief Incident Description: "web-service-test-YYMMDD-HHMMSS"
* Date Incident Occured
* Date Incident Reported
* Incident Description: \[YYMMDD-HHMMSS\] Test.
* Time Completed
 
6. Click the `Submit` button

Since you did not complete the "Program of Incident" field, the "Program of Incident is not valid" message should appear.

Clicking "OK" should bring you back to the form.

> A log file should have been written noting that:
> ?

7. Complete the *Program of Incident* field

8. Click the `Submit` button

The incident was should submit, and you should be back at the homepage.

> A log file should have been written noting that:
> 1. Invalid Program of Incident
> 2. Original author/current user match in the translation table

### The original author opens an incident

Staff should be able to open/submit any incidents they are the original creators of.

#### Expected outcome

Nothing should happen!

#### Testing procedure

Using the TESTWSVCB user:

1. Open the `Open Incident` form

2. Type "40" in the search bar

3. Search for the incident you created

4. Open the incident

5. Add text to the "Incident Description" field.

6. Click the Submit button

The incident was submitted, and the form closed.

### Opening an incident created by another user

Only staff that are members of the authorized list of Avatar User Roles should be able to open incident reports they have not created, but cannot save any changes they make.

Staff that are not members of the authorized list of Avatar User Roles cannot open incidents they did not create.

#### Expected outcomes

When opening an incident report they did not create, authorized staff members should receive a message letting them know any changes they make will not be saved. If the user attempts to submit changes,they will receive a message letting them know they are not able to submit the form.

Unauthorized staff members should not be able to open any incident report they did not create.

#### Testing procedures

1. Login to Avatar as a user with the "SuperUser" role

2. Open the incident

Since you are a member of the "SuperUser" role, you should get the "You are not the original author" message. Clicking "OK" should open the form.

5. Add text to the "Incident Description" field.

6. Click the Submit button

Since you are not the original author of this incident, you should get the "You cannot submit" message. Click OK, and you should be returned to the form. Click "Discard", and you should be returned to the homepage.

2. Verified the TESTUSER account does not have an authorized user role.

2. Opened an Open Incident report created by another user.

Received the "You are not authorized to view" message, and sent to the home screen.

3. Add the TESTUSER user role to the OpenIncident.config file

4. Opened an Open Incident report created by another user.

Received the "Not original author" message, and the form opens.

5. Click the Submit button

Received the "Can't submit" message, and returned to the form.

### Verify the `_VerifyOriginalAuthorIsOpening` command

- [ ] Login as USER1
- [ ] Create a new Open Incident "USER1_testing"
- [ ] Submit "USER1_testing"
- [ ] Open "USER1_testing"
- [ ] Add a note to the Incident Description field
- [ ] Submit the form
- [ ] Logout of USER1

### Verify the translation table is working

- [ ] Copy "USERID_User Description_{AvatarSystem}.txt" to "USERID_User Description_{AvatarSystem}_backup.txt")
- [ ] Open ""USERID_User Description_{AvatarSystem}.txt"
- [ ] Remove USER2 from the file
- [ ] Save the modified file
- [ ] Login as USER2
- [ ] Attempt to open "USER1_testing" - you should receive the "Your use cannot be found" message, and the form should close
- [ ] Delete the ""USERID_User Description_{AvatarSystem}.txt" file
- [ ] Rename "USERID_User Description_{AvatarSystem}_backup.txt" to "USERID_User Description_{AvatarSystem}.txt"

### Verify the `_VerifyOriginalAuthorIsSubmitting` command

- [ ] Attempt to open "USER1_testing" - you should receive the "You are not the original author" message, and the form should open
- [ ] Verify you can view the incident
- [ ] Add a note to the Incident Description field
- [ ] Attempt to submit - you should receive the "You can't submit" message
- [ ] Discard the form form
- [ ] Logout of USER2

### Verify USER2 changes were not submitted

- [ ] Log in "USER1"
- [ ] Open "USER1_testing"
- [ ] Confirm the modification by USER2 was not saved

<br/>

***

[The Tingen Web Service Manual](README.md) ❭ Testing

<sub>Last updated: 260608</sub>
