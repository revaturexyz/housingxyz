import { Complex } from './complex';

describe('Complex', () => {
  it('should create an instance', () => {
    expect(new Complex(
      1,
      '123 HasAdd',
      'Arlinton',
      'Texas',
      '77089',
      'Liv+',
      '123-456-789')
    ).toBeTruthy();
  });
});
