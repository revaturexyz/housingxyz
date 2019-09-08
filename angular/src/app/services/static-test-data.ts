import { TrainingCenter } from 'src/interfaces/trainingcenter';
import { Address } from 'src/interfaces/address';
import { Complex } from 'src/interfaces/complex';
import { Provider } from 'src/interfaces/provider';
import { Amenity } from 'src/interfaces/amenity';
import { Room } from 'src/interfaces/room';

export class TestServiceData {
    static dummyTrainingCenter: TrainingCenter = {
        centerId: 1,
        companyName: 'UIC',
        streetAddress: '123 S. Chicago Ave',
        city: 'Chicago',
        state: 'Illinois',
        zipCode: '60645',
        centerName: 'UIC',
        contactNumber: '3213213214'
    };

    static dummyAddress: Address = {
        addressId: 1,
        streetAddress: '123 Address St',
        city: 'Arlington',
        state: 'TX',
        zipCode: '12345'
    };

    static dummyComplex: Complex = {
        complexId: 1,
        streetAddress: '123 Complex St',
        city: 'Arlington',
        state: 'TX',
        zipCode: '12345',
        complexName: 'Liv+ Appartments',
        contactNumber: '123-123-1234'
    };

    static dummyComplex2: Complex = {
        complexId: 2,
        streetAddress: '234 Complex St',
        city: 'Arlington',
        state: 'TX',
        zipCode: '23456',
        complexName: 'Liv- Appartments',
        contactNumber: '123-123-1234'
    };

    static dummyProv: Provider = {
        companyName: 'Liv+',
        streetAddress: '123 Address St',
        city: 'Arlington',
        state: 'TX',
        zipCode: '12345',
        contactNumber: '123-123-1234',
        providerTrainingCenter: TestServiceData.dummyTrainingCenter
    };

    static dummyGender: string[] = ['male', 'female', 'undefined'];

    static dummyAmenity1: Amenity = {
        amenityId: 1,
        amenityString:  'washer/dryer'
    };

    static dummyAmenity2: Amenity = {
        amenityId: 2,
        amenityString: 'smart TV'
    };

    static dummmyList: Amenity[] = [TestServiceData.dummyAmenity1, TestServiceData.dummyAmenity2];

    static room: Room = {
        roomId: 0,
        roomAddress: TestServiceData.dummyAddress,
        roomNumber: '',
        numberOfBeds: 2,
        roomType: '',
        isOccupied: false,
        amenities: TestServiceData.dummyAmenity1,
        startDate: new Date(),
        endDate: new Date(),
        complexId: 1
    };

    static room2: Room = {
        roomId: 0,
        roomAddress: {
            addressId: 2,
            streetAddress: '701 S Nedderman Dr',
            city: 'Arlington',
            state: 'TX',
            zipCode: '76019'
        },
        roomNumber: '323',
        numberOfBeds: 9001,
        roomType: 'Dorm',
        isOccupied: true,
        amenities: {
            amenityId: 2,
            amenityString: 'Washer/Dryer'
        },
        startDate: new Date(),
        endDate: new Date(),
        complexId: 2
    };

    static postToRoom: Room;
    static postAddress: Address;

    static trainingcenter: TrainingCenter = {
        centerId: 1,
        companyName: 'UTA',
        streetAddress: '1001 S. Center St',
        city: 'Arlington',
        state: 'Texas',
        zipCode: '70610',
        centerName: 'UTA',
        contactNumber: '1231231234'
    };

    static trainingcenter2: TrainingCenter = {
        centerId: 2,
        companyName: 'UIC',
        streetAddress: '123 s. Chicago Ave',
        city: 'Chicago',
        state: 'Illinois',
        zipCode: '60645',
        centerName: 'UIC',
        contactNumber: '3213213214'
    };
}
