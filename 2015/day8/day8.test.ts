import { part1, part2 } from './day8';
import { getInput } from '../utils'

const example = [
    "\"\"", 
    "\"abc\"",
    "\"aaa\\\"aaa\"",
    "\"\\x27\""
];

describe("2015/day8/part1", () => {

  test('example', () => {
    expect(part1(example)).toBe(12);
  });

  
  test('input.txt', () => {
    const input = getInput(__dirname); 
    expect(part1(input)).toBe(1342);
  });
  
});

describe("2015/day8/part2", () => {

    test('example', () => {
      expect(part2(example)).toBe(19);
    });
  

    test('input.txt', () => {
      const input = getInput(__dirname); 
      expect(part2(input)).toBe(2074);
    });
    
});