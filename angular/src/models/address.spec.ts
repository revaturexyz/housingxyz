import { Address } from './address';

describe('Address', () => {
  it('should create an instance', () => {
    expect(new Address(
      1,
      '132 HasAdd',
      'Arlington',
      'Texas',
      '77089'))
    .toBeTruthy();
  });
});
