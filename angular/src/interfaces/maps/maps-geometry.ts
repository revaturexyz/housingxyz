import { MapsGeoLocation } from './maps-geo-location';

export interface MapsGeometry {
    bounds: object[];
    location: MapsGeoLocation;
    location_type: string;
    viewport: object[];
}
