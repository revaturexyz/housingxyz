import { Address } from './address';
import { Amenity } from './amenity';

export class Room {
    constructor(
        public RoomID: number,
        public RoomAddress: Address,
        public RoomNumber: string,
        public NumberOfBeds: number,
        public RoomType: string,
        public IsOccupied: boolean,
        public Amenities: Amenity,
        public StartDate: Date,
        public EndDate: Date,
        public ComplexID: number
    ) { }
}
