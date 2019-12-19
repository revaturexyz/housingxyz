import { Car } from './car';
import { Batch } from './batch';
import { TenantAddress } from './tenant-address';

export interface Tenant {
  id: string;
  email: string;
  gender: string;
  firstName: string;
  lastName: string;
  addressId: string;
  roomId: string;
  carId: string;
  batchId: number;
  tenantAddress: TenantAddress;
  car: Car;
  batch: Batch;
  trainingCenter: string;
}
