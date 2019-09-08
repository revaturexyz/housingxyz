import { MsAdalAngular6Service } from 'microsoft-adal-angular6';
import { Component } from '@angular/core';
import * as Msal from 'msal';

@Component({
  selector: 'dev-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'housingxyz';
  //   config = {
  //     auth: {
  //         clientId: "",
  //         redirectUri: "https://localhost:4200",
  //     },

  // }

  //   applicationId = ""; // B2C application Id
  //   tenant = ""; // Azure tenant ID
  //   signUpSignInPolicy = "B2C_1_revapp"; // Name of user flow

  //   // name of scope, taken from the portal
  //   scope = [""];

  //   // the creation of this was taken from the ref above.
  //   authority = "https://login.microsoftonline.com/tfp/" + this.tenant + "/" +
  //     this.signUpSignInPolicy;

  //   clientApplication = new Msal.UserAgentApplication(this.config);

  //   login() {

  //     var _this = this; // JS this :(

  //     let loginRequest = {
  //       scopes: ["user.read", "user.write"],
  //       prompt: "select_account",
  //   }

  //   let accessTokenRequest = {
  //       scopes: ["user.read", "user.write"]
  //   }

  //   _this.clientApplication.loginPopup(loginRequest).then(function (loginResponse) {
  //       return _this.clientApplication.acquireTokenSilent(accessTokenRequest);
  //   }).then(function (accessTokenResponse) {
  //       const token = accessTokenResponse.accessToken;
  //       console.log(token)
  //   }).catch(function (error) {
  //       //handle error
  //   });

  //   }
}
