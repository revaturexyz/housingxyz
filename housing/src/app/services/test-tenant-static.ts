import { PostTenant } from '../../interfaces/postTenant';
import { PostTenantAddress } from '../../interfaces/postTenAddress';
import { Batch } from '../../interfaces/batch';
import { PostCar } from '../../interfaces/postCar';
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
    };

    static dummyBatch: Batch = {
        batchId: 1,
        batchCurriculum: 'C#',
        startDate: new Date(),
        endDate: new Date(),
        trainingCenter: 'fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d'
    };

    static dummyPostBatch: PostBatch = {
        batchCurriculum: 'C#',
        startDate: new Date(),
        endDate: new Date(),
        trainingCenter: 'fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d'
    };

    static dummyTenant: PostTenant = {
        id: null,
        email: 'abc@abc.com',
        gender: 'male',
        firstName: 'Bob',
        lastName: 'Hope',
        apiAddress: TestTenantData.dummyAddress,
        apiCar: TestTenantData.dummyCar,
        apiBatch: TestTenantData.dummyPostBatch,
        trainingCenter: 'fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d'
    };
}

export class TenantServiceStub {
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
        });
    }

    PostTenant() {
        return of( {
            data: [
                {
                    id: 'Abc-1234-asd',
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
        });
    }
}
