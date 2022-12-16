import { part1, part2 } from './day16';
import { getInput } from '../utils'


describe("2022/day16/part1", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part1(input)).toBe(1651);
  });

  
  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input)).toBe(1701);
  });
  
});


describe("2022/day15/part2", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part2(input)).toBe(56000011);
  });

  
  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part2(input)).toBe(11482462818989);
  });
  
});

