import { TrainingCenter } from './trainingcenter';

export interface Provider {
    providerId: number;
    companyName: string;
    streetAddress: string;
    city: string;
    state: string;
    zipCode: string;
    contactNumber: string;
    providerTrainingCenter: TrainingCenter;
}
