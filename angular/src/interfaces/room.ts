import { Address } from './address';
import { Amenity } from './amenity';
import { Complex } from './complex';
import { RoomType } from './room-type';

export interface Room {
  roomId: number;
  apiAddress: Address;
  roomNumber: string;
  numberOfBeds: number;
  apiRoomType: RoomType;
  isOccupied: boolean;
  apiAmenity: Amenity[];
  startDate: Date;
  endDate: Date;
  apiComplex: Complex;
}
