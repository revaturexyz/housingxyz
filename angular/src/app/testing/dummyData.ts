import { ProviderLocation } from 'src/models/location';

import { Room } from 'src/models/room';



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

    address= '123 Main Street';

    city= 'Queens'; 

    state= 'NY';

    zipCode= '11111';

    trainingCenter= 'Queens College';

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



const location1 = new MockProvider();

const location2 = {

    LocationID: 4,

    address:'65 Kissena',

    city: 'New York',

    state: 'NY',

    zipCode: '10000',

    trainingCenter: 'Queens College',

    ProviderID: 1

}

export const mockLocationList = [{location1}, {location2}]



const room1 = new MockRoom();

const room2= {

     //Primary Key

     RoomID : 2,

     Type : 'dorm',

     MaxOccupancy : 4,

     RoomNumber : '102',

     Gender : 'female',

     StartDate : new Date('2019/08/10'),

     EndDate :  new Date('2019/11/11'),

     CurrentOccupancy : 2,

     IsActive : true,

     Description : 'this is a mock room',

 

     //Foreign Key

     LocationID : 1

}

export const mockRoomList = [{room1}, {room2}]



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
