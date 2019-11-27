import { Batch } from "./batch";
import { Car } from "./car";

export interface TenantInRoom{
    batch:Batch;
    tenantId: string;
    email: string;
    gender: string;
    firstName: string;
    lastName: string;
    addressId: string;
    roomId: number;
    car: Car;
    startDate: Date;
    endDate: Date;
}