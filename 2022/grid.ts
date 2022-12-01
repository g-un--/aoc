// https://stackoverflow.com/questions/50114294/create-a-grid-array-in-javascript

export const rotate = (grid: string[][]) => 
  grid[0].map(
    (_,y)=>grid.map(
      (_,x)=>[y,x]
    )
  ).map(
    row=>row.map(([x,y])=>grid[y][x])
  );

export const format = <T>(grid: string[][]) => grid.map(row=>row.join(" ")).join("\n");
