import { Car } from './car';

export interface Tenant {
    tenantId: string;
    email: string;
    gender: string;
    firstName: string;
    lastName: string;
    addressId: string;
    roomId: number;
    car: Car;
    startDate: Date;
    endDate: Date;
    langName: string;
}