// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  tenant: "9d8d0698-40b7-4f4b-a29a-050698a88de6",
clientId: "335f1779-7ca4-4235-bd75-b6793ea8fd2f",
extraQueryParameter: 'nux=1', // This is optional
endpoints: {
  "http://localhost:4200": "335f1779-7ca4-4235-bd75-b6793ea8fd2f" // Note, this is an object, you could add several things here
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
