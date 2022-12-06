import { part1, part2 } from './day6';
import { getInput } from '../utils'

describe("2022/day6/part1", () => {

  test('example', () => {
    expect(part1("mjqjpqmgbljsphdztnvjfqwrcgsmlb").position).toBe(7);
  })

  test('input.txt', () => {
    const input = getInput(__dirname)[0];
    expect(part1(input).position).toBe(1892);
  });

});


describe("2022/day2/part2", () => {

  test('exaple.txt', () => {
    expect(part2("mjqjpqmgbljsphdztnvjfqwrcgsmlb").position).toBe(19);
  });

  test('input.txt', () => {
    const input = getInput(__dirname)[0];
    expect(part2(input).position).toBe(2500);
  });

});
