export class Address {
    AddressId: number;
    StreetAddress: string;
    City: string;
    State: string;
    ZipCode: string;
    constructor(id: number, addr: string, city: string, state: string, zip: string) {
        this.AddressId = id;
        this.StreetAddress = addr;
        this.City = city;
        this.State = state;
        this.ZipCode = zip;
    }
}
