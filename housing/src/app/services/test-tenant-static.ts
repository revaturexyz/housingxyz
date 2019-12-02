import { PostTenant } from '../../interfaces/PostTenant';
import { PostTenantAddress } from '../../interfaces/postTenAddress';
import { Batch } from '../../interfaces/batch';
import { PostCar } from '../../interfaces/PostCar';
import { PostBatch } from '../../interfaces/postBatch';
import { of } from 'rxjs';

export class TestTenantData {
    static trainingCenterId = 'fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d';

    static dummyAddress: PostTenantAddress = {
        street: '123 Sesame St',
        city: 'Arlington',
        state: 'TX',
        country: 'United States',
        zipCode: '12345'
    };

    static dummyCar: PostCar = {
        licensePlate: 'I(╯°□°）╯︵ ┻━┻',
        make: 'Honda',
        model: 'Accord',
        color: 'Blue',
        year: '1990',
        state: 'TX'
    }

    static dummyBatch: Batch = {
        batchId: 1,
        batchCurriculum: 'C#',
        startDate: new Date(),
        endDate: new Date(),
        trainingCenter: 'fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d'
    }

    static dummyPostBatch: PostBatch = {
        batchCurriculum: 'C#',
        startDate: new Date(),
        endDate: new Date(),
        trainingCenter: 'fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d'
    }

    static dummyTenant: PostTenant = {
        email: 'abc@abc.com',
        gender: 'male',
        firstName: 'Bob',
        lastName: 'Hope',
        apiAddress: TestTenantData.dummyAddress,
        apiCar: TestTenantData.dummyCar,
        apiBatch: TestTenantData.dummyPostBatch,
        trainingCenter: 'fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d'
    }
}

export class CoordinatorServiceStub {
    GetBatchByTrainingCenterId() {
        return of( {
            data: [
                {
                    batchId: 1,
                    batchCurriculum: 'C#',
                    startDate: new Date(),
                    endDate: new Date(),
                    trainingCenter: 'fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d'
                }]
        })
    }

    PostTenant() {
        return of( {
            data: [
                {
                    email: 'abc@abc.com',
                    gender: 'male',
                    firstName: 'Bob',
                    lastName: 'Hope',
                    apiAddress: TestTenantData.dummyAddress,
                    apiCar: TestTenantData.dummyCar,
                    apiBatch: TestTenantData.dummyPostBatch,
                    trainingCenter: 'fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d'
                }
            ]
        })
    }
}