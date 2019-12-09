import { PostCar } from './post-car';
import { PostTenantAddress } from './post-tenant-address';
import { PostBatch } from './post-batch';

export interface PostTenant {
  id: null;
  email: string;
  gender: string;
  firstName: string;
  lastName: string;
  apiCar: PostCar;
  apiAddress: PostTenantAddress;
  trainingCenter: string;
  apiBatch: PostBatch;
}
