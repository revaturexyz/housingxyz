import { TenantInRoom } from "../../interfaces/tenant-in-room";


export const TENANTS: TenantInRoom[] = [
    {
        tenantId: "f55db185-205e-4669-baf3-1872e87b9bcc",
        firstName: "Shanaya",
        lastName: "Twayne",
        gender: "Female",
        car: null,
        batch: {
            batchId: 1,
            language: "c#",
            startDate: new Date(2012,3,1),
            endDate: new Date(2012,3,1)

        }
    },
    {
        tenantId: "s12db185-205e-4663-baf3-1872e87b9bcc",
        firstName: "Cran",
        lastName: "Barnie",
        gender: "Male",
        car: null,
        batch: {
            batchId: 1,
            language: "c#",
            startDate: new Date(2012,3,1),
            endDate: new Date(2012,3,1)

        }
    }
]