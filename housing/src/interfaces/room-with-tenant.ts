import { TenantInRoom } from "./tenant-in-room";

export interface RoomWithTenants{
    id: string;
    roomNumber: string;
    totalBeds: number;
    tenants:TenantInRoom[];
}