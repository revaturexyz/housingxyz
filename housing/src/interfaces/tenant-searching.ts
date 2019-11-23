export default interface TenantSearching {
    id :string;
    email: string;
    gender: string;
    firstName: string;
    lastName: string;
    addressId: string;
    roomId: string | null;
    carId: number | null;
    batchId: number | null;
    trainingCenter: string;
}
