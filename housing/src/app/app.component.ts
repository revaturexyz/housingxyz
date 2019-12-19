import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'dev-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'housing';

  constructor(private auth: AuthService) { }

  ngOnInit() {
    this.auth.localAuthSetup();
    this.auth.handleAuthCallback();
  }
}
