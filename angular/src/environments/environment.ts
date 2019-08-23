export const environment = {
  msalConfig: {
    authority: 'https://identityxyz.b2clogin.com/7cd0af2d-1ead-4ab0-bbbc-9913da89ce5a/B2C_1_signup_signin',
    clientID: '398b1b9c-8b43-4f29-bac5-b4b0daf30e04',
    redirectUri: 'http://localhost:10080',
    validateAuthority: false
  },
  production: false
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
