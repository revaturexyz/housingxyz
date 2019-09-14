import { TrainingCenter } from './trainingcenter';
import { Address } from './address';

export interface Provider {
    providerId: number;
    companyName: string;
    streetAddress: Address;
    contactNumber: string;
    providerTrainingCenter: TrainingCenter;
}
