import {sumNumbers} from './day12';
import {sumNumbersWithoutRedObjects} from './day12';
import {getInput} from '../utils'

describe("2015/day12/part1", () => {

    test('[1,2,3]', () => {
      expect(sumNumbers("[1,2,3]")).toBe(6);
    });

    test('{"a":2,"b":4}', () => {
        expect(sumNumbers('{"a":2,"b":4}')).toBe(6);
    });

    test('[[[3]]]', () => {
        expect(sumNumbers('[[[3]]]')).toBe(3);
    });

    test('{"a":{"b":4},"c":-1}', () => {
        expect(sumNumbers('[[[3]]]')).toBe(3);
    });

    test('{"a":[-1,1]}', () => {
        expect(sumNumbers('{"a":[-1,1]}')).toBe(0);
    });
    
    test('[-1,{"a":1}]', () => {
        expect(sumNumbers('[-1,{"a":1}]')).toBe(0);
    });
    
    test('[]', () => {
        expect(sumNumbers('[]')).toBe(0);
    });

    test('{}', () => {
        expect(sumNumbers('{}')).toBe(0);
    });
    
    test('input.txt', () => {
        const input = getInput(__dirname)[0];
        expect(sumNumbers(input)).toBe(111754);
    }); 
});

describe("2015/day12/part2", () => {

    test('[1,2,3]', () => {
      expect(sumNumbersWithoutRedObjects("[1,2,3]")).toBe(6);
    });

    test('[1,{"c":"red","b":2},3]', () => {
        expect(sumNumbersWithoutRedObjects('[1,{"c":"red","b":2},3]')).toBe(4);
    });

    test('{"d":"red","e":[1,2,3,4],"f":5}', () => {
        expect(sumNumbersWithoutRedObjects('{"d":"red","e":[1,2,3,4],"f":5}')).toBe(0);
    });

    test('[1,"red",5]', () => {
        expect(sumNumbersWithoutRedObjects('[1,"red",5]')).toBe(6);
    });
    
    test('input.txt', () => {
        const input = getInput(__dirname)[0];
        expect(sumNumbersWithoutRedObjects(input)).toBe(111754);
    }); 
});