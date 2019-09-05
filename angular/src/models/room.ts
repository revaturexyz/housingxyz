import { Complex } from './complex';
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
}
