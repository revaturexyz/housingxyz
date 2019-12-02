import { PostCar } from './postCar';
import { PostTenantAddress } from './PostTenAddress';
import { PostBatch } from './postBatch';


export interface PostTenant {
    id: null,
    email: string;
    gender: string;
    firstName: string;
    lastName: string;
    apiCar: PostCar;
    apiAddress: PostTenantAddress;
    trainingCenter: string;
    apiBatch: PostBatch;
}
