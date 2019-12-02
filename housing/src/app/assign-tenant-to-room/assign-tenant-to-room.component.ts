import { Component, OnInit } from '@angular/core';
import { RoomWithTenants } from '../../interfaces/room-with-tenant';
import { TenantInRoom } from '../../interfaces/tenant-in-room';
import { TenantAssignService } from '../services/tenant-assign-service';

@Component({
  selector: 'dev-assign-tenant-to-room',
  templateUrl: './assign-tenant-to-room.component.html',
  styleUrls: ['./assign-tenant-to-room.component.scss']
})
export class AssignTenantToRoomComponent implements OnInit {

  // Initial array of rooms pre-prioritized
  roomArray:[RoomWithTenants,number][];

  // Displayed array of rooms, prioritized
  prioritizedRooms:[RoomWithTenants,number][];

  // All rooms available for a selected tenant
  availableRooms:RoomWithTenants[]; 

  // All tenants that don't have a room
  assignableTenants:TenantInRoom[];

  // Currently selected tenant to be assigned a room
  currentTenant:TenantInRoom;

  // Currently selected room for a tenant
  currentRoomId: string;

  filterCar:boolean = false;
  filterLang:boolean = false;
  filterBatch:boolean = false;

  // Determines how strongly to prioritize specific filters
  batchPriorityWeight:number = 0.6;
  langPriorityWeight:number = 0.6;

  constructor(
    private tenantAssignService: TenantAssignService
  ) { }

  ngOnInit() {
    this.tenantAssignService.getTenantsNotAssignedRoom().then(
      result => this.assignableTenants = result
    );
  }

  selectTenant(tenant: TenantInRoom){
    this.currentTenant = tenant;
  }

  selectRoom(roomId: string){
    this.currentRoomId = roomId;
  }

  // Filter Checkbox Methods; sorts list whenever a checkbox is clicked
  toggleCar(){
    this.filterCar = !this.filterCar;
    this.prioritizedRooms = this.prioritizeWithFilters(this.roomArray);
  }
  toggleLang(){
    this.filterLang = !this.filterLang;
    this.prioritizedRooms = this.prioritizeWithFilters(this.roomArray);
  }
  toggleBatch(){
    this.filterBatch = !this.filterBatch;
    this.prioritizedRooms = this.prioritizeWithFilters(this.roomArray);
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

  prioritizeWithFilters(arr:[RoomWithTenants, number][]) : [RoomWithTenants, number][]{
    let result = JSON.parse(JSON.stringify(arr));
    if (this.filterCar){
      this.prioritizeRoomsByCar(result);
    }
    if (this.filterLang){
      this.prioritizeRoomsByLang(result);
    }
    if (this.filterBatch){
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
