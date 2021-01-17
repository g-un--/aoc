import { part1, part2 } from './day4';

describe("2015/day4/part1", () => {

  test('abcdef', () => {
    expect(part1("abcdef")).toBe("609043");
  });

  test('pqrstuv<', () => {
    expect(part1("pqrstuv")).toBe("1048970");
  });

  test('ckczppom', () => {
    expect(part1("ckczppom")).toBe("117946");
  });
  
});

describe("2015/day4/part2", () => {

    test('ckczppom', () => {
      expect(part2("ckczppom")).toBe("3938038");
    });
    
});
