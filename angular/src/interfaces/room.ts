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
    apiAmenity: Amenity[];
    startDate: Date;
    endDate: Date;
    apiComplex: Complex;
}
