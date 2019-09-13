import { Address } from './address';
import { Amenity } from './amenity';
import { Complex } from './complex';

export interface Room {
    roomId: number;
    roomAddress: Address;
    roomNumber: string;
    numberOfBeds: number;
    roomType: string;
    isOccupied: boolean;
    amenities: Amenity[];
    startDate: Date;
    endDate: Date;
    complex: Complex;
}
