import { part1, part2 } from './day5';
import { getInput } from '../utils'

describe("2015/day5/part1", () => {

  test('ugknbfddgicrmopn', () => {
    expect(part1(["ugknbfddgicrmopn"])).toBe("1");
  });

  test('aaa', () => {
    expect(part1(["aaa"])).toBe("1");
  });

  test('jchzalrnumimnmhp', () => {
    expect(part1(["jchzalrnumimnmhp"])).toBe("0");
  });

  test('haegwjzuvuyypxyu', () => {
    expect(part1(["haegwjzuvuyypxyu"])).toBe("0");
  });

  test('dvszwmarrgswjxmb', () => {
    expect(part1(["dvszwmarrgswjxmb"])).toBe("0");
  });

  test('input.txt', () => {
    const input = getInput(__dirname); 
    expect(part1(input)).toBe("258");
  });

});

describe("2015/day5/part2", () => {

  test('qjhvhtzxzqqjkmpb', () => {
    expect(part2(["qjhvhtzxzqqjkmpb"])).toBe("1");
  });

  test('xxyxx', () => {
    expect(part2(["xxyxx"])).toBe("1");
  });

  test('uurcxstgmygtbstg', () => {
    expect(part2(["uurcxstgmygtbstg"])).toBe("0");
  });

  test('ieodomkazucvgmuy', () => {
    expect(part2(["ieodomkazucvgmuy"])).toBe("0");
  });

  test('input.txt', () => {
    const input = getInput(__dirname); 
    expect(part2(input)).toBe("53");
  });

});
