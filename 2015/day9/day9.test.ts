import { part1, part2 } from './day9';
import { getInput } from '../utils'

const example = [
    "London to Dublin = 464",
    "London to Belfast = 518",
    "Dublin to Belfast = 141"
];

describe("2015/day9/part1", () => {

  test('example', () => {
    expect(part1(example)).toBe(605);
  });

  test('input.txt', () => {
    const input = getInput(__dirname); 
    expect(part1(input)).toBe(117);
  });
  
});

describe("2015/day9/part1", () => {

    test('example', () => {
      expect(part2(example)).toBe(982);
    });
  
    test('input.txt', () => {
      const input = getInput(__dirname); 
      expect(part2(input)).toBe(909);
    });
    
});
