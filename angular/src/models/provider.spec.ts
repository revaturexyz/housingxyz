import { Provider } from './provider';

describe('Provider', () => {
  it('should create an instance', () => {
    expect(new Provider('', '', '', '', '', '', null)).toBeTruthy();
  });
});
