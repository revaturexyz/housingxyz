import { RoomWithTenants } from "../../interfaces/room-with-tenant";
import { TENANTS } from "./mock-tenants";

export const ROOMS: RoomWithTenants[] = [
    {
        id: "1",
        roomNumber: "2048",
        totalBeds: 4,
        tenants: [
            TENANTS[0]
        ]
    },

    {
        id: "2",
        roomNumber: "20B",
        totalBeds: 2,
        tenants: [
            TENANTS[1],
        ]
    }
]