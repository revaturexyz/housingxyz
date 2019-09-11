import { Component, OnInit, ɵConsole } from '@angular/core';
import { Router } from '@angular/router';
import { MapsService } from '../services/maps.service';

@Component({
  selector: 'dev-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  locationList: object;
  roomList: object;

  constructor(private router: Router, private mapsService: MapsService) { }

  updateRoom(id: number) {
    this.router.navigate(['update-room', id]);
  }

  ngOnInit() {
    this.mapsService.checkDistance();
  }
}
