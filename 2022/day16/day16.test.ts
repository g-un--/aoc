import { part1, part2, cache } from './day16';
import { getInput } from '../utils'
import { describe, test, expect, beforeEach } from 'bun:test'

describe("2022/day16/part1", () => {

  beforeEach(() => {
    cache.clear();
  });

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part1(input)).toBe(1651);
  });


  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input)).toBe(1701);
  });

});


describe("2022/day16/part2", () => {

  beforeEach(() => {
    cache.clear();
  });

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part2(input)).toBe(1707);
  });


  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part2(input)).toBe(2455);
  }, {
    timeout: 100000 // milliseconds
  });

});

