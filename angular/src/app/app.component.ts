import { Component } from '@angular/core';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'xyz-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'housingxyz';

  constructor(private authService: MsalService) {}

  signOut() {
    this.authService.logout();
  }
}
