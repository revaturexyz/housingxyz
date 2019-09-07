import { TrainingCenter } from "src/models/trainingcenter";
import { Address } from 'src/models/address';
import { Complex } from 'src/models/complex';
import { Provider } from 'src/models/provider';
import { Amenity } from 'src/models/amenity';
import { Room } from 'src/models/room';

export class TestServiceData {
    static dummyTrainingCenter: TrainingCenter = new TrainingCenter(
        1,
        "UIC",
        "123 s. Chicago Ave",
        "Chicago",
        "Illinois",
        "60645",
        "UIC",
        "3213213214"
    );
      
    static dummyAddress: Address = new Address(
        1,
        '123 Address St',
        'Arlington',
        'TX',
        '12345'
    );

    static dummyComplex: Complex = new Complex(
        1,
        '123 Complex St',
        'Arlington',
        'TX',
        '12345',
        'Liv+ Appartments',
        '123-123-1234'
    );

    static dummyComplex2: Complex = new Complex(
        2,
        '234 Complex St',
        'Arlington',
        'TX',
        '23456',
        'Liv- Appartments',
        '123-123-1234'
    );

    static dummyProv: Provider = new Provider(
        'Liv+',
        '123 Address St',
        'Arlington',
        'TX',
        '12345',
        '123-123-1234',
        TestServiceData.dummyTrainingCenter
    );

    static dummyGender: string[] = ['male', 'female', 'undefined'];

    static dummyAmenity1: Amenity = new Amenity(1, 'washer/dryer');

    static dummyAmenity2: Amenity = new Amenity(2, 'smart TV');

    static dummmyList: Amenity[] = [TestServiceData.dummyAmenity1, TestServiceData.dummyAmenity2];

    static room: Room = new Room(
        null,
        new Address(1, '1001 S Center St', 'Arlington', 'TX', '76010'),
        '',
        2,
        '',
        false,
        TestServiceData.dummyAmenity1,
        new Date(),
        new Date(),
        1
    );

    static room2: Room = new Room(
        null,
        new Address(2, '701 S Nedderman Dr', 'Arlington', 'TX', '76019'),
        '323',
        9001,
        'Dorm',
        true,
        new Amenity(2, 'Washer/Dryer'),
        new Date(),
        new Date(),
        2
    );

    static postToRoom: Room;
    static postAddress: Address;

    static trainingcenter: TrainingCenter = new TrainingCenter(
        1,
        "UTA",
        "1001 s. Center St",
        "Arlington",
        "Texas",
        "76010",
        "UTA",
        "1231231234"
    );
    
    static trainingcenter2: TrainingCenter = new TrainingCenter(
        2,
        "UIC",
        "123 s. Chicago Ave",
        "Chicago",
        "Illinois",
        "60645",
        "UIC",
        "3213213214"
    );
}