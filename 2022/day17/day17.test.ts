import { part1, part2 } from './day17';
import { getInput } from '../utils'


describe("2022/day17/part1", () => {

  test('example.txt', () => {
    const moves = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";
    expect(part1(moves)).toBe(3068);
  });

  test('input.txt', () => {
    const input = getInput(__dirname)[0];
    expect(part1(input)).toBe(3209);
  });

});


describe("2022/day17/part2", () => {
  
  test('example.txt', () => {
    const moves = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";
    expect(part2(moves)).toBe(1514285714288);
  });

  
  test('input.txt', () => {
    const input = getInput(__dirname)[0];
    expect(part2(input)).toBe(0);
  });
  
});