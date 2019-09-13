import { MapsGeometry } from './maps-geometry';

export interface MapsDetails {
    address_components: object[];
    formatted_address: string;
    geometry: MapsGeometry;
    partial_match: boolean;
    place_id: object;
    types: object[];
}
