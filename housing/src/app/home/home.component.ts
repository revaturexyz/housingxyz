import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Provider } from 'src/interfaces/account/provider';
import { Complex } from 'src/interfaces/complex';

@Component({
  selector: 'dev-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  locationList: object;
  roomList: object;
  provider: Provider;
  complexes: Complex[];

  constructor(private router: Router) { 

  }

  ngOnInit() {
  }
}
