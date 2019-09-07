export class ProviderLocation {

    //  Primary Key
    locationID: number;
    address: string;
    city: string;
    state: string;
    zip: string;
    traningCenter: string;

    // Foreign Key
    providerID: number;
}
