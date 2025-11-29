import { part1, part2 } from './day11';
import { getInput } from '../utils'
import { describe, test, expect } from 'bun:test'

describe("2022/day11/part1", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part1(input)).toBe(10605);
  });

  
  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input)).toBe(66802);
  });

});


describe("2022/day11/part2", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part2(input)).toBe(2713310158);
  });


  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part2(input)).toBe(21800916620);
  });

});
