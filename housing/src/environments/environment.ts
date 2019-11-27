// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  domain: 'dbnd.auth0.com',
  clientID: 'kdZAr42aRguFCVbUJ5C7xozmM0n31KYn',
  audience: '/housing',
  googleMapsKey: 'AIzaSyCxYMcmEjlHQ2r2CywMgyK7YEplxurqW2A',
  endpoints: {
    // provider: 'http://192.168.99.100:10080/'
    provider: 'http://localhost:10080/',
    coordinator: 'http://localhost:5000/'
    // provider: 'http://localhost:5000/'
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
