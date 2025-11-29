import { part1, part2 } from './day15';
import { getInput } from '../utils'
import { describe, test, expect } from 'bun:test'

describe("2022/day15/part1", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part1(input, 10)).toBe(26);
  });


  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input, 2000000)).toBe(4737443);
  });

});


describe("2022/day15/part2", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part2(input, 20)).toBe(56000011);
  });


  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part2(input, 4000000)).toBe(11482462818989);
  });

});

