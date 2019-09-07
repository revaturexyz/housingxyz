import { TrainingCenter } from './trainingcenter';

export interface Provider {
    companyName: string;
    streetAddress: string;
    city: string;
    state: string;
    zipCode: string;
    contactNumber: string;
    providerTrainingCenter: TrainingCenter;
}
