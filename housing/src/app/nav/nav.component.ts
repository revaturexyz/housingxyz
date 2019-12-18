import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'dev-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  public role: string;

  constructor(private router: Router, public auth: AuthService, public user: UserService) {
    user.Roles$.subscribe(res => this.role = res[0]);
  }

  ngOnInit() { }
}
