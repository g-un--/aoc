import { part1, part2 } from './day7';
import { getInput } from '../utils'
import { describe, test, expect } from 'bun:test'

describe("2022/day7/part1", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part1(input)).toBe(95437);
  });


  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input)).toBe(1581595);
  });

});


describe("2022/day7/part2", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part2(input)).toBe(24933642);
  });


  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part2(input)).toBe(1544176);
  });

});
