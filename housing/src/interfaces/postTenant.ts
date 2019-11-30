import { PostCar } from './postCar';
import { PostTenantAddress } from './PostTenAddress';
import { PostBatch } from './postBatch';


export interface PostTenant {
    email: string;
    gender: string;
    firstName: string;
    lastName: string;
    car: PostCar;
    tenAddress: PostTenantAddress;
    trainingCenter: string;
    batch: PostBatch;
}