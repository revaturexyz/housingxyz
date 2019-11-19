import { Car } from './car';
import { Batch } from './batch';
import { TenantAddress } from './tenantAddress';

export interface Tenant {
    tenantId: string;
    email: string;
    gender: string;
    firstName: string;
    lastName: string;
    tenantAddress: TenantAddress;
    car: Car;
    batch: Batch;
}