import { MapsGeoLocation } from './maps-geo-location';
//nested results of maps -> maps-details Google API result object
//location object contains the actual coordinates of the result.
export interface MapsGeometry {
    bounds: object[];
    location: MapsGeoLocation;
    location_type: string;
    viewport: object[];
}
