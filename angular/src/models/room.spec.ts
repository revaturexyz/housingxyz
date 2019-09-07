import { Room } from './room';
import { Amenity } from './amenity';
import { Address } from './address';
describe('Room', () => {
  it('should create an instance', () => {
    const date = new Date();
    expect(new Room(
      1,
      new Address(
        1,
        '1100 Center',
        'Arlington',
        'TX',
        '8787'
      ),
      '1121 A',
      1,
      'Apt',
      true,
      new Amenity(1, 'toster'),
      date,
      date,
      1
      ))
    .toBeTruthy();
  });
});
