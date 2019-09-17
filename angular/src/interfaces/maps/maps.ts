import { MapsDetails } from './maps-details';
//Google API returns a complex object, most of which we do not use.
//We map the returned object to this nested interface.
//status returns OK if at least one result found, ZERO_RESULTS if no match is found.
//results contains a list of matched results; we are using the first result.
//if address was entered correctly; results should contain exactly one match.
//format is: https://maps.googleapis.com/maps/api/geocode/json?address= <address, separated by + or spaces or no separators> &key=AIzaSyCxYMcmEjlHQ2r2CywMgyK7YEplxurqW2A
export interface Maps {
    results: MapsDetails[];
    status: string;
}
