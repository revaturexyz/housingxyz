import { MapsGeometry } from './maps-geometry';
// nested results of the maps Google API result object.
// in correct address search, there will be just one MapsDetails element return.
// formatted_address returns the "formal" address matching the search.
// formatted_address may be of use later to verify that user meant to enter that address.
// geometry object contains latitude and longitude of the corrected address.
export interface MapsDetails {
    address_components: object[];
    formatted_address: string;
    geometry: MapsGeometry;
    partial_match: boolean;
    place_id: object;
    types: object[];
}
