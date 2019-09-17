import { Address } from './address';
import { Provider } from './provider';

export interface Complex {
    complexId: number;
    apiAddress: Address;
    apiProvider: Provider;
    complexName: string;
    contactNumber: string;
}
