export const environment = {
  production: true,
  tenant: '',
  clientId: '',
  extraQueryParameter: 'nux=1', // This is optional
  googleMapsKey: 'AIzaSyCxYMcmEjlHQ2r2CywMgyK7YEplxurqW2A',
  identity: {
    authority: 'https://identityxyz.b2clogin.com/identity.revature.xyz/b2c_1_signup_signin',
    clientID: 'fac95bb7-bd11-4ecf-b30b-fdd6ddd13bcd',
    validateAuthority: false
  },
  endpoints: {
    providerXYZ: 'https://providerpre.revature.xyz/',
    'http://localhost:4200': '' // Note, this is an object, you could add several things here
  }
};
