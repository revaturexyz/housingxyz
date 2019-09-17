import { TrainingCenter } from './trainingcenter';
import { Address } from './address';

export interface Provider {
    providerId: number;
    companyName: string;
    address: Address;
    contactNumber: string;
    apiTrainingCenter: TrainingCenter;
}
