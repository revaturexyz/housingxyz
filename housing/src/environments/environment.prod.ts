export const environment = {
  production: true,
  domain: 'dev-fyo32d99.auth0.com', // Absent trailing slash matters here
  clientID: '4z5nyCX4Jg9L7TDAUvMP0zApLQY3N4dx',
  audience: '/account',
  claimsDomain: 'https://revature.com/', // Trailing slash matters here
  googleMapsKey: 'AIzaSyCxYMcmEjlHQ2r2CywMgyK7YEplxurqW2A',
  endpoints: {
    account: 'https://account-aspnet-dev.azurewebsites.net/',
    complex: 'https://complex-aspnet-dev.azurewebsites.net/',
    tenant: 'https://tenant-aspnet-dev.azurewebsites.net/',
    provider: 'http://localhost:10080/'
  }
};
