import { part1, part2 } from './day18';
import { getInput } from '../utils'
import { describe, test, expect } from 'bun:test'

describe("2022/day17/part1", () => {

  test('example 1', () => {
    const cubes = ["1,1,1", "2,1,1"];
    expect(part1(cubes)).toBe(10);
  });

  test('example.txt', () => {
    const input = getInput(__dirname, "example.txt");
    expect(part1(input)).toBe(64);
  });

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input)).toBe(4604);
  });

});


describe("2022/day17/part2", () => {
  
  test('example.txt', () => {
    const input = getInput(__dirname, "example.txt");
    expect(part2(input)).toBe(58);
  });

  
  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part2(input)).toBe(2604);
  });
  
});