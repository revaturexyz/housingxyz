export class TrainingCenter {
    CenterId: number;
    CompanyName: string;
    StreetAddress: string;
    City: string;
    State: string;
    ZipCode: string;
    CenterName: string;
    ContactNumber: string;
    constructor(
        Id: number,
        Name: string,
        Address: string,
        Cities: string,
        States: string,
        ZipCodes: string,
        CName: string,
        CNumber: string
    ) {
        this.CenterId = Id;
        this.CompanyName = Name;
        this.StreetAddress = Address;
        this.City = Cities;
        this.State = States;
        this.ZipCode = ZipCodes;
        this.CenterName = CName;
        this.ContactNumber = CNumber;
    }
}

