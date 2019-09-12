export class ProviderLocation {

    // Primary Key
    LocationID : number;
    Address : string;
    City : string;
    State : string;
    Zip : string;
    TraningCenter : string;

    // Foreign Key
    ProviderID : number;
}
