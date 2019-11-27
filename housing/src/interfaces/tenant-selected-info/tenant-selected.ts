import CarSelected from './car-selected';
import BatchSelected from './batch-selected';

export default interface TenantSelected {
    id: string;
    email: string;
    gender: string;
    firstName: string;
    lastName: string;
    addressId: string;
    roomId: string | null;
    carId: number | null;
    car: CarSelected | null;
    batchId: number | null;
    batch: BatchSelected | null;
    trainingCenter: string;
}
