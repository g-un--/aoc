function dfs(json: any, filterObjects: (object: any) => boolean): number {
     if (Array.isArray(json)) {
        return json.reduce((sum, current) => sum + dfs(current, filterObjects), 0);
     } else if (typeof(json) === "object") {
         let sum = 0;
         if (filterObjects(json)) {
            for (const value of Object.values(json)) {
                sum += dfs(value, filterObjects);
            }
         }
         return sum;
     } else if (typeof (json) === "number") {
         return json;
     } else {
         return 0;
     }
}

export function sumNumbers(input: string): number {
    const json = JSON.parse(input);
    return dfs(json, (obj) => true);
}

export function sumNumbersWithoutRedObjects(input: string): number {
    const json = JSON.parse(input);
    return dfs(json, (obj) => {  
        const values = Object.values(obj);
        return !values.some((value) => value === "red"); 
    });
}