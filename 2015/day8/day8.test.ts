import { part1 } from './day8';
import { getInput } from '../utils'

describe("2015/day8/part1", () => {

  test('example', () => {
    const result =
        part1([
            "\"\"", 
            "\"abc\"",
            "\"aaa\\\"aaa\"",
            "\"\\x27\""]);
    
    expect(result).toBe(12);
  });

  
  test('input.txt', () => {
    const input = getInput(__dirname); 
    expect(part1(input)).toBe(1342);
  });
  
});