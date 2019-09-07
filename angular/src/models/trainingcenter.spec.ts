import { Trainingcenter } from './trainingcenter';

describe('Trainingcenter', () => {
  it('should create an instance', () => {
    expect(new Trainingcenter(
      1,
      'UTA',
      '1001 s. Center St',
      'Arlington',
      'Texas',
      '76010',
      'UTA',
      '1231231234'))
      .toBeTruthy();
  });
});
