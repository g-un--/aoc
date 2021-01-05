import { part1 } from './day2'
import fs from 'fs';
import path from 'path';

describe("2015/day1/part1", () => {

  test('2x3x4', () => {
    expect(part1(["2x3x4"])).toBe("58");
  });

  test('1x1x10', () => {
    expect(part1(["1x1x10"])).toBe("43");
  });
  
  test('input.txt', () => {
    const input = fs.readFileSync(path.resolve(__dirname, "./input.txt"), "utf-8").split("\n");
    expect(part1(input)).toBe("1586300");
  });

});