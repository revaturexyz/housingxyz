// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  tenant: '',
  clientId: '',
  identity: {
    authority: 'https://identityxyz.b2clogin.com/identity.revature.xyz/b2c_1_signup_signin',
    clientID: 'fac95bb7-bd11-4ecf-b30b-fdd6ddd13bcd',
    validateAuthority: false
  },
  extraQueryParameter: 'nux=1', // This is optional
  googleMapsKey: 'AIzaSyCxYMcmEjlHQ2r2CywMgyK7YEplxurqW2A',
  endpoints: {
    providerXYZ: 'http://192.168.99.100:10080/' // Note, this is an object, you could add several things here
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
