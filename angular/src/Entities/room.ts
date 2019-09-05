export class Room {
    
    //Primary Key
    RoomID : number;
    Type : string;
    MaxOccupancy : number;
    RoomNumber : string;
    Gender : string;
    StartDate : Date;
    EndDate : Date;
    CurrentOccupancy : number;
    IsActive : boolean;
    Description : string;

    //Foreign Key
    LocationID : number;
}
