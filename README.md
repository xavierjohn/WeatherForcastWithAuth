# Weather Forecast Example with Authorization Unit Test

This sample demonstrates how to use a custom test authentication handler to ensure unit tests for the Weather Forecast API can be executed without disabling authorization. The example also highlights how to verify that authorization mechanisms are implemented correctly.

### Implementation Details

1. **Custom Test Authentication Handler**
   A lightweight authentication handler is created to simulate authenticated users during unit tests, bypassing complex external authentication mechanisms.

2. **Unit Test Validation**
   Tests ensure authorization requirements are enforced by verifying protected endpoints reject unauthorized access while allowing authorized users with the correct claims or roles.


To get a token use:

```http
https://login.microsoftonline.com/{Your Tenant}/oauth2/v2.0/authorize?response_type=token&redirect_uri=https://jwt.ms&client_id={Your Client}&scope=api://{Your Client}/access_as_user
```