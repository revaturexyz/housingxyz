import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { UserService } from './services/user.service';

@Component({
  selector: 'dev-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'housing';

  constructor(private auth: AuthService, private user: UserService) {}

  ngOnInit() {
    this.auth.localAuthSetup();
    this.auth.handleAuthCallback();

    this.user.Roles$.subscribe(res => console.log(res));
  }
}
