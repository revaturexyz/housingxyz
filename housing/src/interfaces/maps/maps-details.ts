import { MapsGeometry } from './maps-geometry';

// An object matching one of results in the array of results
// inside a Maps object.
//
// formatted_address: contains the "formal" address string matching the search
//                    - formatted_address may be of use later to verify that user meant to enter that address.
// geometry: contains latitude and longitude of the corrected address
export interface MapsDetails {
  address_components: object[];
  formatted_address: string;
  geometry: MapsGeometry;
  partial_match: boolean;
  place_id: object;
  types: object[];
}
