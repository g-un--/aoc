import { part1, part2 } from './day5';
import { getInput } from '../utils'

describe("2022/day5/part1", () => {

  test('exaple.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part1(input)).toBe("CMZ");
  });

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input)).toBe("GRTSWNJHH");
  });

});


describe("2022/day2/part2", () => {

  test('exaple.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part2(input)).toBe("MCD");
  });

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part2(input)).toBe("QLFQDBBHM");
  });

});
