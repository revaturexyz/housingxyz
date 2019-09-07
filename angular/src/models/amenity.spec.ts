import { Amenity } from './amenity';

describe('Amenity', () => {
  it('should create an instance', () => {
    expect(new Amenity(1, 'A/C')).toBeTruthy();
  });
});
