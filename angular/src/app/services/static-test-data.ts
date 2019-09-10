import { TrainingCenter } from 'src/interfaces/trainingcenter';
import { Address } from 'src/interfaces/address';
import { Complex } from 'src/interfaces/complex';
import { Provider } from 'src/interfaces/provider';
import { Amenity } from 'src/interfaces/amenity';
import { Room } from 'src/interfaces/room';

export class TestServiceData {
    static dummyTrainingCenter: TrainingCenter = {
        centerId: 1,
        streetAddress: '123 S. Chicago Ave',
        city: 'Chicago',
        state: 'Illinois',
        zipCode: '60645',
        centerName: 'UT Arlington',
        contactNumber: '3213213214'
    };

    static dummyAddress: Address = {
        addressId: 1,
        streetAddress: '123 Address St',
        city: 'Arlington',
        state: 'TX',
        zipCode: '12345'
    };

    static livPlusAddress: Address = {
        addressId: 2,
        streetAddress: '1001 S Center St',
        city: 'Arlington',
        state: 'TX',
        zipCode: '76010'
    };

    static UTA: Address = {
        addressId: 3,
        streetAddress: '749 S Cooper St',
        city: 'Arlington',
        state: 'TX',
        zipCode: '76010'
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

    static dummyGender: string[] = ['male', 'female', 'undefined'];

    static dummyAmenity1: Amenity = {
        amenityId: 1,
        amenityString:  'Washer/Dryer',
        isSelected: true
    };
    static dummyAmenity2: Amenity = {
        amenityId: 2,
        amenityString: 'Smart TV',
        isSelected: true
    };
    static dummyAmenity3: Amenity = {
        amenityId: 3,
        amenityString: 'Patio',
        isSelected: true
    };
    static dummyAmenity4: Amenity = {
        amenityId: 4,
        amenityString: 'Fully Furnished',
        isSelected: true
    };
    static dummyAmenity5: Amenity = {
        amenityId: 5,
        amenityString: 'Full Kitchen',
        isSelected: true
    };
    static dummyAmenity6: Amenity = {
        amenityId: 6,
        amenityString: 'Individual Bathrooms',
        isSelected: true
    };

    static dummmyList: Amenity[] = [
        TestServiceData.dummyAmenity1,
        TestServiceData.dummyAmenity2,
        TestServiceData.dummyAmenity3,
        TestServiceData.dummyAmenity4,
        TestServiceData.dummyAmenity5,
        TestServiceData.dummyAmenity6
    ];

    static room: Room = {
        roomId: 0,
        roomAddress: TestServiceData.dummyAddress,
        roomNumber: '',
        numberOfBeds: 2,
        roomType: '',
        isOccupied: false,
        amenities: TestServiceData.dummmyList,
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
        amenities: [{
            amenityId: 2,
            amenityString: 'Washer/Dryer',
            isSelected: true
        }],
        startDate: new Date(),
        endDate: new Date(),
        complexId: 2
    };

    static postToRoom: Room;
    static postAddress: Address;

    static trainingcenter: TrainingCenter = {
        centerId: 1,
        streetAddress: '701 S Nedderman Dr',
        city: 'Arlington',
        state: 'Texas',
        zipCode: '76019',
        centerName: 'UT Arlington - Preston Hall',
        contactNumber: '(703) 570-8181'
    };

    static trainingcenter2: TrainingCenter = {
        centerId: 2,
        streetAddress: '123 s. Chicago Ave',
        city: 'Chicago',
        state: 'Illinois',
        zipCode: '60645',
        centerName: 'UIC',
        contactNumber: '3213213214'
    };

    static dummyProvider: Provider = {
        providerId: 1,
        companyName: 'Liv+',
        streetAddress: '123 Address St',
        city: 'Arlington',
        state: 'TX',
        zipCode: '12345',
        contactNumber: '123-123-1234',
        providerTrainingCenter: TestServiceData.trainingcenter
    };
}
