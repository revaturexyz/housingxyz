import { MapsGeoLocation } from './maps-geo-location';

// A nested object in the Maps.mapDetails property
//
// location: object contains the actual coordinates of the result
export interface MapsGeometry {
  bounds: object[];
  location: MapsGeoLocation;
  location_type: string;
  viewport: object[];
}
