import { TrainingCenter } from './trainingcenter';

describe('TrainingCenter', () => {
  it('should create an instance', () => {
    expect(new TrainingCenter(
      1,
      "UTA",
      "1001 s. Center St",
      "Arlington",
      "Texas",
      "76010",
      "UTA",
      "1231231234"
    )).toBeTruthy();
  });
});
