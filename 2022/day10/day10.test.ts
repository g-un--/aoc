import { part1, part2 } from './day10';
import { getInput } from '../utils'
import { describe, test, expect } from 'bun:test'

describe("2022/day10/part1", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part1(input)).toBe(13140);
  });


  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input)).toBe(14920);
  });

});


describe("2022/day10/part2", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    part2(input)
  });


  test('input.txt', () => {
    const input = getInput(__dirname);
    part2(input)
    //you should see some letters in terminal
    expect("BUCACBUZ");
  });

});
