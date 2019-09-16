import { TrainingCenter } from 'src/interfaces/trainingcenter';
import { Address } from 'src/interfaces/address';
import { Complex } from 'src/interfaces/complex';
import { Provider } from 'src/interfaces/provider';
import { Amenity } from 'src/interfaces/amenity';
import { Room } from 'src/interfaces/room';

export class TestServiceData {
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

    static dummyTrainingCenter: TrainingCenter = {
        centerId: 1,
        address: TestServiceData.livPlusAddress,
        centerName: 'UT Arlington',
        contactNumber: '3213213214'
    };

    static UTA: Address = {
        addressId: 3,
        streetAddress: '749 South Cooper St',
        city: 'Arlington',
        state: 'TX',
        zipCode: '76010'
    };

    static complexAddress1: Address = {
        addressId: 9,
        streetAddress: '123 Complex St',
        city: 'Arlington',
        state: 'TX',
        zipCode: '12345'
    };

    static complexAddress2: Address = {
        addressId: 10,
        streetAddress: '234 Complex St',
        city: 'Arlington',
        state: 'TX',
        zipCode: '23456',
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

    static postToRoom: Room;
    static postAddress: Address;

    static prestionHall: Address = {
        addressId: 6,
        streetAddress: '701 S Nedderman Dr',
        city: 'Arlington',
        state: 'Texas',
        zipCode: '76019',
    };

    static trainingcenter: TrainingCenter = {
        centerId: 1,
        address: TestServiceData.prestionHall,
        centerName: 'UT Arlington - Preston Hall',
        contactNumber: '(703) 570-8181'
    };

    static uic: Address = {
        addressId: 7,
        streetAddress: '123 s. Chicago Ave',
        city: 'Chicago',
        state: 'Illinois',
        zipCode: '60645',
    };

    static trainingcenter2: TrainingCenter = {
        centerId: 2,
        address: TestServiceData.uic,
        centerName: 'UIC',
        contactNumber: '3213213214'
    };

    static liv: Address = {
        addressId: 8,
        streetAddress: '123 Address St',
        city: 'Arlington',
        state: 'TX',
        zipCode: '12345',
    };

    static dummyProvider: Provider = {
        providerId: 2,
        companyName: 'Liv+',
        address: TestServiceData.liv,
        contactNumber: '123-123-1234',
        providerTrainingCenter: TestServiceData.trainingcenter
    };

    static dummyComplex: Complex = {
        complexId: 1,
        apiProvider: TestServiceData.dummyProvider,
        apiAddress: TestServiceData.livPlusAddress,
        complexName: 'Liv+ Appartments',
        contactNumber: '123-123-1234'
    };

    static room: Room = {
        roomId: 1,
        roomAddress: TestServiceData.dummyAddress,
        roomNumber: '322',
        numberOfBeds: 2,
        roomType: 'Apartment',
        isOccupied: false,
        amenities: TestServiceData.dummmyList,
        startDate: new Date(),
        endDate: new Date(),
        complex: TestServiceData.dummyComplex
    };

    static dummyComplex2: Complex = {
        complexId: 2,
        apiProvider: TestServiceData.dummyProvider,
        apiAddress: TestServiceData.complexAddress2,
        complexName: 'Liv- Appartments',
        contactNumber: '123-123-1234'
    };

    static room2: Room = {
        roomId: 2,
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
            isSelected: false
        }],
        startDate: new Date(),
        endDate: new Date(),
        complex: TestServiceData.dummyComplex2
    };

    static testTrainingCenter2Address: Address = {
        addressId: 8,
        streetAddress: 'One UTSA Circle',
        city: 'San Antonio',
        state: 'TX',
        zipCode: '78249'
    };

    static testTrainingCenter2: TrainingCenter = {
        centerId: 5,
        address: TestServiceData.testTrainingCenter2Address,
        centerName: 'UTSA Training Center',
        contactNumber: '(482) 482-4824'
    };

    static testProvider2Address: Address = {
        addressId: 9,
        streetAddress: '7114 UTSA Boulevard',
        city: 'San Antonio',
        state: 'TX',
        zipCode: '78249'
    };

    static testProvider2: Provider = {
        providerId: 1,
        companyName: 'Prado Student Living',
        address: TestServiceData.livPlusAddress,
        contactNumber: '(203) 232-2847',
        providerTrainingCenter: TestServiceData.trainingcenter
    };

    static testProviders: Provider[] = [
        TestServiceData.dummyProvider,
        TestServiceData.testProvider2
    ];
}
