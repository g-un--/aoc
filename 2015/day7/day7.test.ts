import { part1 } from './day7';
import { getInput } from '../utils'

describe("2015/day7/part1", () => {

  test('example', () => {
    const variables =
        part1([
            "123 -> x", 
            "456 -> y",
            "x AND y -> d",
            "x OR y -> e",
            "x LSHIFT 2 -> f",
            "y RSHIFT 2 -> g",
            "NOT x -> h",
            "NOT y -> i"]);
    
    expect(variables.get("x")).toBe(123);
    expect(variables.get("y")).toBe(456);
    expect(variables.get("d")).toBe(72);
    expect(variables.get("e")).toBe(507);
    expect(variables.get("f")).toBe(492);
    expect(variables.get("g")).toBe(114);
    expect(variables.get("h")).toBe(65412);
    expect(variables.get("i")).toBe(65079);
  });

  
  test('input.txt', () => {
    const input = getInput(__dirname); 
    expect(part1(input).get("a")?.toString()).toBe("956");
  });
  
});