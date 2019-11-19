import { Car } from './car';
import { Batch } from './batch';

export interface Tenant {
    tenantId: string;
    email: string;
    gender: string;
    firstName: string;
    lastName: string;
    addressId: string;
    roomId: number;
    car: Car;
    batch: Batch;
}