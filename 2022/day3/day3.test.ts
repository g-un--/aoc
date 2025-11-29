import { part1, part2 } from './day3';
import { getInput } from '../utils'
import { describe, test, expect } from 'bun:test'

const example = [
  "vJrwpWtwJgWrhcsFMMfFFhFp",
  "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
  "PmmdzqPrVvPwwTWBwg",
  "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
  "ttgJtRGJQctTZtZT",
  "CrZsJsPPZsGzwwsLwLmpwMDw"
];

describe("2022/day3/part1", () => {

  test('example', () => {
    expect(part1(example)).toBe(157);
  });

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input)).toBe(7850);
  });

});

describe("2022/day3/part2", () => {
  test('example', () => {
    expect(part2(example)).toBe(70);
  });

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part2(input)).toBe(2581);
  });

});
