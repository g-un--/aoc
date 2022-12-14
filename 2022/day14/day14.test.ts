import { part1, part2 } from './day14';
import { getInput } from '../utils'


describe("2022/day14/part1", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part1(input)).toBe(24);
  });

  
  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input)).toBe(979);
  });
  
});

/*
describe("2022/day14/part2", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part2(input)).toBe(140);
  });


  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part2(input)).toBe(21614);
  });

});
*/
