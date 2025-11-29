import { part1, part2 } from './day9';
import { getInput } from '../utils'
import { describe, test, expect } from 'bun:test'

describe("2022/day9/part1", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part1(input)).toBe(13);
  });

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input)).toBe(6090);
  });

});


describe("2022/day9/part2", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part2(input)).toBe(1);
  });

  test('example2.txt', () => {
    const input = getInput(__dirname, "./example2.txt");
    expect(part2(input)).toBe(36);
  });

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part2(input)).toBe(2566);
  });

});
