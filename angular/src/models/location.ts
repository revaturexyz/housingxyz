export class ProviderLocation {

    //  Primary Key
    locationId: number;
    address: string;
    city: string;
    state: string;
    zip: string;
    traningCenter: string;

    // Foreign Key
    providerId: number;
}
