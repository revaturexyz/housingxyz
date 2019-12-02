import { Batch } from "./batch";
import { Car } from "./car";

export interface TenantInRoom{
    batch:Batch;
    tenantId: string;
    gender: string;
    firstName: string;
    lastName: string;
    car: Car;
}