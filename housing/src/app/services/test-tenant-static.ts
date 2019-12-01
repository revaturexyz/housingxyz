import { Tenant } from '../../interfaces/tenant';
import { TenantAddress } from '../../interfaces/tenantAddress';
import { Batch } from '../../interfaces/batch';
import { Car } from '../../interfaces/car';

export class TestTenantData {
    static trainingCenterId = 'fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d';

    static dummyAddress: TenantAddress = {
        addressId: 'qwer',
        street: '123 Sesame St',
        city: 'Arlington',
        state: 'TX',
        country: 'United States',
        zipCode: '12345'
    };

    static dummyCar: Car = {
        carId: 12345,
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
        trainingCenter: 'fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d',
    };

    static dummyTenant: Tenant = {
        id: '1',
        email: 'abc@abc.com',
        gender: 'male',
        firstName: 'Bob',
        lastName: 'Hope',
        addressId: '',
        roomId: '',
        carId: '',
        batchId: 1,
        tenantAddress: TestTenantData.dummyAddress,
        car: TestTenantData.dummyCar,
        batch: TestTenantData.dummyBatch,
        trainingCenter: 'fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d'
    };
}
