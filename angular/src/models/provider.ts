import { Trainingcenter } from './trainingcenter';

export class Provider {
    constructor(
        public CompanyName: string,
        public StreetAddress: string,
        public City: string,
        public State: string,
        public ZipCode: string,
        public ContactNumber: string,
        public TrainingCenter: Trainingcenter
        ) { }
}
