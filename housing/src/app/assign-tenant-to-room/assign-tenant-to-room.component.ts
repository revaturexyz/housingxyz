import { Component, OnInit } from '@angular/core';
import { RoomWithTenants } from '../../interfaces/room-with-tenant';
import { TenantInRoom } from '../../interfaces/tenant-in-room';

@Component({
  selector: 'dev-assign-tenant-to-room',
  templateUrl: './assign-tenant-to-room.component.html',
  styleUrls: ['./assign-tenant-to-room.component.scss']
})
export class AssignTenantToRoomComponent implements OnInit {

  roomArray:[RoomWithTenants,number][];
  changingArray:[RoomWithTenants,number][];

  availableRooms:RoomWithTenants[];
  assignableTenants:TenantInRoom[];
  currentTenant:TenantInRoom;

  filterCar:boolean = false;
  filterLang:boolean = false;
  filterBatch:boolean = false;

  // Determines how strongly to prioritize specific filters
  batchPriorityWeight:number = 0.6;
  langPriorityWeight:number = 0.6;


  constructor() { }

  ngOnInit() {
    // Populate
  }

  // Filter Checkbox Methods; sorts list whenever a checkbox is clicked
  toggleCar(){
    this.filterCar = !this.filterCar;
    this.changingArray = this.prioritizeWithFilters(this.roomArray,this.filterCar,this.filterLang,this.filterBatch);
  }
  toggleLang(){
    this.filterLang = !this.filterLang;
    this.changingArray = this.prioritizeWithFilters(this.roomArray,this.filterCar,this.filterLang,this.filterBatch);
  }
  toggleBatch(){
    this.filterBatch = !this.filterBatch;
    this.changingArray = this.prioritizeWithFilters(this.roomArray,this.filterCar,this.filterLang,this.filterBatch);
  }

  prioritizeRoomsByCar(arr:[RoomWithTenants,number][]) {
    arr.forEach(roomTuple => {
      let currentOccupancy = roomTuple[0].totalBeds - roomTuple[0].tenants.length;
      let totalCars = 0;
      roomTuple[0].tenants.forEach(t=>{
        if(t.car){
          totalCars++;
        }
      });
      let priority;
      if (this.currentTenant.car){
        priority = (currentOccupancy - totalCars) / currentOccupancy;
      } else {
        priority = (totalCars - currentOccupancy) / currentOccupancy;
      }
      roomTuple[1] = Math.round((roomTuple[1] + priority)*100)/100;
    });
    
  }

  prioritizeRoomsByLang(arr:[RoomWithTenants, number][]){
    arr.forEach(roomTuple => {
      roomTuple[0].tenants.forEach(tenant => {
        if (tenant.batch.language == this.currentTenant.batch.language){
          roomTuple[1] += this.langPriorityWeight;
        }
      });
      roomTuple[1] = Math.round(roomTuple[1]*100)/100;
    });
  }

  prioritizeRoomsByBatch(arr:[RoomWithTenants, number][]){
    arr.forEach(roomTuple => {
      roomTuple[0].tenants.forEach(tenant => {
        if (tenant.batch.batchId == this.currentTenant.batch.batchId){
          roomTuple[1] += this.batchPriorityWeight;
        }
      });
      roomTuple[1] = Math.round(roomTuple[1]*100)/100;
    });
  }

  prioritizeWithFilters(arr:[RoomWithTenants, number][], car:boolean,lang:boolean, batch:boolean) : [RoomWithTenants, number][]{
    let result = JSON.parse(JSON.stringify(arr));
    if (car){
      this.prioritizeRoomsByCar(result);
    }
    if (lang){
      this.prioritizeRoomsByLang(result);
    }
    if (batch){
      this.prioritizeRoomsByBatch(result);
    }
    this.sortRoomsByPriority(result);
    return result;
  }

  // Sorts list by the 2nd item in each tuple element of the array
  sortRoomsByPriority(someArray:[string,number][]){
    someArray.sort((elem1,elem2) => elem1[1] > elem2[1] ? -1 : elem1[1] < elem2[1] ? 1 : 0);
  }

}
