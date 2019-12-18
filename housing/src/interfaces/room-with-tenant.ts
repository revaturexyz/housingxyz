import { TenantInRoom } from './tenant-in-room';

export interface RoomWithTenants {
    id: string;
    roomNumber: string;
    numberOfBeds: number;
    tenants: TenantInRoom[];
}
