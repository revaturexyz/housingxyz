import { Address } from './address';
import { Amenity } from './amenity';

export interface Room {
    roomId: number;
    roomAddress: Address;
    roomNumber: string;
    numberOfBeds: number;
    roomType: string;
    isOccupied: boolean;
    amenities: Amenity;
    startDate: Date;
    endDate: Date;
    complexId: number;
}
