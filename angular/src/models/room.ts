import { Address } from './address';
import { Amenity } from './amenity';

export class Room {
    RoomID?: number;
    Address: Address;
    RoomNumber: string;
    NumberOfBeds: number;
    RoomType: string;
    IsOccupied: boolean;
    Amenities: Amenity;
    StartDate: Date;
    EndDate: Date;
    ComplexID: number;
    constructor(
        id: number,
        addy: Address,
        roomNum: string,
        numBeds: number,
        roomType: string,
        isOcc: boolean,
        am: Amenity,
        startD: Date,
        endD: Date,
        compId: number
    ) {
        this.RoomID = id;
        this.Address = addy;
        this.RoomNumber = roomNum;
        this.NumberOfBeds = numBeds;
        this.RoomType = roomType;
        this.IsOccupied = isOcc;
        this.Amenities = am;
        this.StartDate = startD;
        this.EndDate = endD;
        this.ComplexID = compId;
    }
}
