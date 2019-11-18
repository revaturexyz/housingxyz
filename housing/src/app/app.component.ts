import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'dev-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'housing';

  constructor() {}

  ngOnInit() {
  }
}
