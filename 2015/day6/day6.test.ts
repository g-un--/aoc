import { part1, part2 } from './day6';
import { getInput } from '../utils'

describe("2015/day6/part1", () => {

  test('turn on 0,0 through 999,999', () => {
    expect(part1(["turn on 0,0 through 999,999"], 1000, 1000)).toBe("1000000");
  });

  test('toggle 0,0 through 999,0', () => {
    expect(part1(["toggle 0,0 through 999,0"], 1000, 1000)).toBe("1000");
  });

  test('turn off 499,499 through 500,500', () => {
    expect(part1([
        "turn on 0,0 through 999,999", 
        "turn off 499,499 through 500,500"
    ], 1000, 1000)).toBe("999996");
  });
  
  test('input.txt', () => {
    const input = getInput(__dirname); 
    expect(part1(input, 1000, 1000)).toBe("377891");
  });

});

describe("2015/day6/part2", () => {

    test('turn on 0,0 through 0,0', () => {
      expect(part2(["turn on 0,0 through 0,0"], 1000, 1000)).toBe("1");
    });
  
    test('toggle 0,0 through 999,999', () => {
        expect(part2(["toggle 0,0 through 999,999"], 1000, 1000)).toBe("2000000");
    });
    
    test('input.txt', () => {
        const input = getInput(__dirname); 
        expect(part2(input, 1000, 1000)).toBe("14110788");
    });
});