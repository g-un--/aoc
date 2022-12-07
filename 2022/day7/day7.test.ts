import { part1, part2 } from './day7';
import { getInput } from '../utils'

describe("2022/day7/part1", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part1(input)).toBe(95437);
  });


  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input)).toBe(100000);
  });

});


describe("2022/day2/part2", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part2(input)).toBe(24933642);
  });


  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part2(input)).toBe(2500);
  });

});
