import { MapsDetails } from './maps-details';

// The Google Mapps Geocode API returns a complex object, most of which we do not use.
// We map the returned object to this nested interface.
//
// status: is set to 'OK' if at least one result found
//         is set to 'ZERO_RESULTS' if no match found
//
// results: an array of matched results
//          - we are using the first result as it is most closely matches
//            the query
//
// Exact matches for an address result in one result.
export interface Maps {
  results: MapsDetails[];
  status: string;
}
