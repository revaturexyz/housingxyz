import { TenantInRoom } from "./tenant-in-room";

export interface RoomWithTenants{
    id: number;
    roomNumber: string;
    totalBeds: number;
    tenants:TenantInRoom[];
}