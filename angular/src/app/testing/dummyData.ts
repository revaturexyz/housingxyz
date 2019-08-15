import { ProviderLocation } from 'src/Entities/location';
import { Room } from 'src/Entities/room';

export const LocationData = {
    Address: "abc ",
    State: "NY",
    City: "city",
    ZipCode: "12345",
    //need change, might not need this properties
    TrainingCenter: "Queen"
}

export const LocationObject={
    locationID: 3, 
    address: '123 Main Street',
    city: 'Queens', 
    state: 'New York',
    zip: '11111',
    trainingCenter: 'Queens College',
    providerID: 1,
}

export const RoomData = {
  
    Type:"apartment",
    MaxOccupancy:2 ,
    RoomNumber:"1",
    Gender:"F",
    StartDate: new Date('2019/08/10'),
    EndDate: undefined,
    Description : ''

}

export const UpdateRoomData = {
  
    Type:"apartment",
    MaxOccupancy:2 ,
    CurrentOccupancy:2,
    RoomNumber:"1",
    Gender:"F",
    StartDate: new Date('2019/08/10'),
    EndDate: undefined,
    Description : '',
    LocationData:2

}

export const LocationErrorList = {
    ErrorList: ["Address is required",
        "State is required",
        "City is required",
        "Zip Code is required",
        "Training Center is required"
    ]
}

export const RoomErrorList={
    ErrorList:["Room Type is required",
        "Number of Beds is required",
        "Room Number is required",
        "Gender is required",
        "StartDate is required"
        
       
      
    ]
}


export const UpdateRoomErrorList={
    ErrorList:["Room Type is required",
        "Number of Beds is required",
        "Number of Beds is required",
        "Room Number is required",
        "Gender is required",
        "StartDate is required",
        "EndDate is required"
        
       
      
    ]
}

export class MockProvider extends ProviderLocation{
    LocationID= 3; 
    Address= '123 Main Street';
    City= 'Queens'; 
    State= 'New York';
    ZipCode= '11111';
    TrainingCenter= 'Queens College';
    ProviderID= 1;
}
export class MockRoom extends Room{
     

    //Primary Key
    RoomID = 1;
    Type = 'dorm';
    MaxOccupancy = 4;
    RoomNumber = '101';
    Gender = 'male';
    StartDate = new Date('2019/08/10');
    EndDate =  new Date('2019/11/11');
    CurrentOccupancy = 2;
    IsActive = true;
    Description = 'this is a mock room';

    //Foreign Key
    LocationID = 1;
}

export const RoomObject={

     

    //Primary Key
    roomID : 1,
    type : 'dorm',
    maxOccupancy : 4,
    roomNumber : '101',
    gender : 'male',
    startDate : new Date('2019/08/10'),
    endDate :  new Date('2019/11/11'),
    currentOccupancy : 2,
    isActive : true,
    description : 'this is a mock room',

    //Foreign Key
    locationID : 1

}
