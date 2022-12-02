function getCalories(input: string[]): number[] {
  const calories: number[] = [];
  let currentElfCalories = 0;
  for (const item of input) {
    if (item === '') {
      calories.push(currentElfCalories);
      currentElfCalories = 0;
    } else {
      currentElfCalories += Number.parseInt(item, 10);
    }
  }
  return calories;
}

export function part1(input: string[]): number {
  const calories = getCalories(input);
  const maxItem = Math.max(...calories);
  return maxItem;
}

export function part2(input: string[]): number {
  const calories = getCalories(input);
  calories.sort((a, b) => b - a);
  const top3Sum = calories.slice(0, 3).reduce((sum, current) => sum + current, 0);
  return top3Sum;
}
