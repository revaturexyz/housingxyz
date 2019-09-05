export class Complex {
    ComplexId: number;
    StreetAddress: string;
    City: string;
    State: string;
    ZipCode: string;
    ComplexName: string;
    ContactNumber: string;

    constructor(id: number, addr: string, city: string, state: string, zip: string, name: string, num: string) {
        this.ComplexId = id;
        this.StreetAddress = addr;
        this.City = city;
        this.State = state;
        this.ZipCode = zip;
        this.ComplexName = name;
        this.ContactNumber = num;
    }
}
