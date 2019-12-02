import CarSelected from './car-selected';
import BatchSelected from './batch-selected';
import AddressSelected from './address-selected';

export default interface TenantSelected {
    id: string;
    email: string;
    gender: string;
    firstName: string;
    lastName: string;
    addressId: string;
    roomId: string | null;
    carId: number | null;
    batchId: number | null;
    trainingCenter: string;
    apiCar: CarSelected | null;
    apiBatch: BatchSelected | null;
    apiAddress: AddressSelected | null;
}
