import {getTransformedLength} from './day10';

describe("2015/day10/part1", () => {

    test('example', () => {
      expect(getTransformedLength("1", 5)).toBe(6);
    });

    
    test('part1', () => {
        expect(getTransformedLength("1113122113", 40)).toBe(360154);
    });
    
   
    test('part2', () => {
        expect(getTransformedLength("1113122113", 50)).toBe(5103798);
    });
    
});